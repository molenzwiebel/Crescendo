# Crescendo

![Crescendo Logo](https://i.imgur.com/8oYy8td.png)

[![Discord](https://discordapp.com/api/guilds/249481856687407104/widget.png?style=shield)](https://discord.gg/bfxdsRC)

## Table of Contents
- [Crescendo](#crescendo)
- [Setup](#setup)
- [Development](#development)
    - [License](#license)
    - [How To Contribute](#how-to-contribute)
    - [Project Structure](#project-structure)
    - [Changelog](#changelog)
- [FAQ](#faq)
- [Support and Feedback](#support-and-feedback)

## :mute: Crescendo <a name="crescendo"></a>
Annoyed by the PHUUOOOOOOOOOOOM sound the League client makes after you've accepted a queue? Wish it'd just shut up until you loaded into champion select? **Crescendo** runs in the background and will automatically mute your League when you've accepted or declined the ready check.

## Setup
### Requirements
- This is an application for [League of Legends](https://www.leagueoflegends.com/en-us/), so ensure you have the League Client installed first.

### Installation
1. To download, click the [Releases](https://github.com/molenzwiebel/Crescendo/releases) link on the right to download the latest version.
    - If you are a Windows User, a message may show up that prevents Crescendo from running. To bypass this, click on More Info > Allow anyway
3. To launch League with Crescendo, simply open League how you normally would. Crescendo will run in the background.

## Development

### License
This project operates under the [GPL 3](https://opensource.org/licenses/GPL-3.0) license. Pull requests are always welcome!

### How To Contribute
For development support, please visit the [Discord](https://discord.gg/bfxdsRC).

To begin development on your own fork, click the "Fork" button at the top right of the project page.

To clone the repository to start developing, click on the green "Code" button at the top right of the page.
- Copy the displayed URL
- In your terminal, enter the command `git clone https://github.com/YOUR-USERNAME/YOUR-REPOSITORY`

### Project Structure
The following includes a summary of the major files and directories in this project
- `Crescendo.sln`: The Microsoft Visual Studio Solution file for the Crescendo project, defining project structure, configurations, and dependencies within the Visual Studio development environment.  
- `Crescendo` directory: Contains the application's main files
  - `Properties` directory: Configuration files and settings for the project
  - `App.config`: XML-based configuration settings
  - `Crescendo.cs`: Main class managing system tray functionality, menu items, and League of Legends interactions, including dynamic sound control based on in-game states.
  - `LeagueConnection.cs`: Manages a WebSocket connection with the League of Legends client, providing methods to connect, disconnect, handle events, and make requests to the League Client Update API.
  - `LeagueUtils.cs`: Provides static methods to query and interact with the League of Legends client process.
  - `Program.cs`: Serves as the main entry point for the application, initializing and running the Crescendo class.
  - `SimpleJson.cs`: Provides functionality for handling JSON serialization and deserialization.
  - `VolumeMixer.cs`: Uses COM interfaces to interact with Windows audio controls, specifically muting and unmuting sessions.

### Changelog
#### v1.1.0 (Latest)
- Resolved issues where Crescendo would fail to mute League of Legends with certain configurations.
- If Crescendo fails to detect the League client (indicated by a grayscale icon), please try running it as administrator.

#### v1.0.0
- Initial release!

## FAQ

**Is this approved by Riot?**  
Cresendo conforms to both the [general Riot Developer policies](https://developer.riotgames.com/policies.html) and the [League Client development policies](https://developer.riotgames.com/league-client-apis.html), and it is registered on Riot's developer portal. While this doesn't mean that Riot explicitly approves the application or supports it in any way, it makes it increasingly unlikely that using this application will risk your account. [This comment by RiotSargonas](https://www.reddit.com/r/leagueoflegends/comments/80d4r0/runebook_the_ultimate_rune_pages_manager_that_you/duv2r22/) explains more about Riot's approach to applications like this one.

**How do I uninstall?**  
Since Crescendo doesn't actually install anything, removing it is as simple as deleting the file from your Downloads folder!

**Wow this is amazing! Do you have any other cool programs?**  
_shameless plug start_   
As a matter of fact I do! I created [Mimic](http://mimic.molenzwiebel.xyz/desktop), which is a client on your mobile phone. I also created [Deceive](https://github.com/molenzwiebel/Deceive), which allows you to appear offline to all your friends. Finally, I created [Sentinel](https://github.com/molenzwiebel/Sentinel), which gives you proper desktop notifications when you receive a new message or invite on League.  
_shameless plug end_

## Support and Feedback
- Need help, have a feature suggestion, or just want to talk? Join the [Discord](https://discord.gg/bfxdsRC)!
