using System;
using System.Diagnostics;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Crescendo
{
    class Crescendo : ApplicationContext
    {
        public static string APP_NAME = "Crescendo"; // For boot identification
        public static string VERSION = "1.1.0";
        private RegistryKey bootKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

        private NotifyIcon trayIcon;
        private LeagueConnection league;
        private bool wasInitiallyMuted;
        private bool isMuted;

        private bool startsOnBoot => bootKey.GetValue(APP_NAME) != null;

        public Crescendo()
        {
            league = new LeagueConnection();
            league.OnConnected += SetupMenuItems; // refresh menu state on connect
            league.OnDisconnected += SetupMenuItems; // and on disconnect
            league.Observe("/lol-matchmaking/v1/ready-check", HandleReadyCheckState);

            trayIcon = new NotifyIcon
            {
                Icon = Properties.Resources.crescendo,
                Visible = true
            };
            SetupMenuItems();

            // If this is our first launch, ask for startup and show a bubble.
            if (!Properties.Settings.Default.launchedBefore)
            {
                Properties.Settings.Default.launchedBefore = true;
                Properties.Settings.Default.Save();

                var result = MessageBox.Show(
                    "Would you like Crescendo to automatically launch when you start your computer? You can enable or disable this at any time.",
                    "Crescendo",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button1
                );

                if (result == DialogResult.Yes) bootKey.SetValue(APP_NAME, Application.ExecutablePath);

                trayIcon.BalloonTipText =
                    "Crescendo will run in the background. Right-click the tray icon for more information.";
                trayIcon.ShowBalloonTip(3000);
            }
        }

        private void SetupMenuItems()
        {
            var aboutMenuItem = new MenuItem("Crescendo v" + VERSION + " - " + (league.IsConnected ? "Active" : "League Not Running"));
            aboutMenuItem.Enabled = false;

            var sourceMenuItem = new MenuItem("Source Code", (a, e) =>
            {
                Process.Start("https://github.com/molenzwiebel/crescendo");
            });

            MenuItem startOnBootMenuItem = null;
            startOnBootMenuItem = new MenuItem("Start with Windows", (a, e) =>
            {
                // Toggle start on boot.
                if (!startsOnBoot) bootKey.SetValue(APP_NAME, Application.ExecutablePath);
                else bootKey.DeleteValue(APP_NAME, false);

                // Rerender menu items.
                startOnBootMenuItem.Checked = startsOnBoot;
            });
            startOnBootMenuItem.Checked = startsOnBoot;

            var quitMenuItem = new MenuItem("Quit", (a, b) =>
            {
                Application.Exit();
            });

            trayIcon.Icon = league.IsConnected
                ? Properties.Resources.crescendo
                : Properties.Resources.crescendo_grayscale;
            trayIcon.ContextMenu = new ContextMenu(new MenuItem[] { aboutMenuItem, sourceMenuItem, startOnBootMenuItem, quitMenuItem });
        }

        private void HandleReadyCheckState(dynamic state)
        {
            // If there's no ready check, or the user can is able to accept, make sure
            // that League's sound is on. If there _is_ a ready check, and the user can't accept,
            // mute League.
            if (state == null || state.state != "InProgress" || state.playerResponse == "None")
            {
                UnmuteLeague();
            }
            else
            {
                MuteLeague();
            }
        }

        private void MuteLeague()
        {
            if (!league.IsConnected) return;
            if (isMuted) return;

            Console.WriteLine("Muting League");

            wasInitiallyMuted = VolumeMixer.GetLeagueMuted().Value;
            VolumeMixer.SetLeagueMuted(true);
            isMuted = true;

            SetupMenuItems();
        }

        private void UnmuteLeague()
        {
            if (!league.IsConnected) return;
            if (!isMuted) return;

            Console.WriteLine("Unmuting League");

            VolumeMixer.SetLeagueMuted(wasInitiallyMuted);
            isMuted = false;

            SetupMenuItems();
        }
    }
}
