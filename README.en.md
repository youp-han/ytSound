# ytSound

[![C#](https://img.shields.io/badge/C%23-blueviolet)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![.NET Framework](https://img.shields.io/badge/.NET-512BD4?logo=dotnet)](https://dotnet.microsoft.com/)
[![Windows Forms](https://img.shields.io/badge/Windows_Forms-lightgrey)](https://docs.microsoft.com/en-us/dotnet/desktop/winforms/)

[한글 README](README.md)

## 📖 Introduction

`ytSound` is a simple Windows desktop application that streams and plays audio from YouTube video URLs. It provides an easy way to enjoy your favorite music without ads.

![image](https://github.com/user-attachments/assets/d6197fef-4242-402b-9296-5eb9403447f8)

## ✨ Key Features

*   **YouTube Audio Playback**: Streams and plays audio directly from a YouTube video URL.
*   **Playback Controls**: Supports basic playback controls like Play, Pause, Resume, and Stop.
*   **Playlist Management**: Save your favorite tracks to a playlist and load them back anytime.
*   **Video Information Display**: Shows video details such as title, description, channel, and thumbnail in the UI.
*   **YouTube Shortcut**: Opens the YouTube website with a single button click.

## 🛠️ Key Technologies & Libraries Used

*   **C# (.NET Framework)**: The primary development language for the application.
*   **Windows Forms**: For building the desktop UI.
*   **[Google APIs Client Library for .NET](https://github.com/googleapis/google-api-dotnet-client)**: Used to fetch video metadata via the YouTube Data API v3.
*   **[YoutubeExplode](https://github.com/Tyrrrz/YoutubeExplode)**: Extracts audio streams from YouTube.
*   **[NAudio](https://github.com/naudio/NAudio)**: Plays the extracted audio streams.

## 🚀 How to Build and Run

1.  **Clone the Repository**:
    ```bash
    git clone <repository-url>
    ```
2.  **Open in Visual Studio**:
    Open the `ytSound.sln` solution file using Visual Studio.

3.  **Set Up API Key**:
    *   Create a copy of the `App.Template.config` file and rename it to `App.config`.
    *   Open the `App.config` file and enter your own YouTube Data API v3 key in the `YouTubeApiKey` value.
    ```xml
    <appSettings>
        <add key="YouTubeApiKey" value="YOUR_API_KEY_HERE" />
        <add key="ApplicationName" value="ytSound" />
    </appSettings>
    ```

4.  **Build and Run**:
    Build the project (Ctrl+Shift+B) and run it (F5) from Visual Studio.

## 📂 Source Code Structure

*   `ytSoundfrm.cs`: The core file containing the main UI form and user event handling logic.
*   `service/ytService.cs`: A service class that communicates with the YouTube Data API to fetch video information.
*   `utility/YouTubeAudioPlayer.cs`: Encapsulates the logic for fetching audio streams with `YoutubeExplode` and playing them with `NAudio`.
*   `utility/StorageUtility.cs`: Handles saving and loading playlists in `JSON` format to/from the local file system.
*   `utility/ListViewWithButtons.cs`: Customizes the UI's list view control.
*   `dto/PlayListDto.cs`: A Data Transfer Object (DTO) that defines the data structure for a playlist.
