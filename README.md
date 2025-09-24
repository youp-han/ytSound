# ytSound

[![C#](https://img.shields.io/badge/C%23-blueviolet)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![.NET Framework](https://img.shields.io/badge/.NET-512BD4?logo=dotnet)](https://dotnet.microsoft.com/)
[![Windows Forms](https://img.shields.io/badge/Windows_Forms-lightgrey)](https://docs.microsoft.com/en-us/dotnet/desktop/winforms/)

[English README](README.en.md)

## 📖 소개

`ytSound`는 YouTube 동영상 URL을 사용하여 오디오를 스트리밍하고 재생하는 간단한 Windows 데스크톱 애플리케이션입니다. 좋아하는 음악을 광고 없이 간편하게 즐길 수 있습니다.

![image](https://github.com/user-attachments/assets/d6197fef-4242-402b-9296-5eb9403447f8)

## ✨ 주요 기능

*   **YouTube 오디오 재생**: YouTube 동영상 URL만 있으면 오디오를 바로 스트리밍하여 재생할 수 있습니다.
*   **재생 컨트롤**: 재생, 일시정지, 이어하기, 정지 등 기본적인 재생 컨트롤을 지원합니다.
*   **재생 목록 관리**: 원하는 곡들을 재생 목록으로 저장하고, 언제든지 다시 불러올 수 있습니다.
*   **동영상 정보 표시**: 비디오의 제목, 설명, 채널, 썸네일 등 상세 정보를 UI에서 확인할 수 있습니다.
*   **YouTube 바로가기**: 버튼 클릭으로 YouTube 웹사이트를 바로 열 수 있습니다.

## 🛠️ 사용된 주요 기술 및 라이브러리

*   **C# (.NET Framework)**: 애플리케이션의 주요 개발 언어입니다.
*   **Windows Forms**: 데스크톱 UI를 구성합니다.
*   **[Google APIs Client Library for .NET](https://github.com/googleapis/google-api-dotnet-client)**: YouTube Data API v3를 사용하여 동영상 메타데이터를 가져옵니다.
*   **[YoutubeExplode](https://github.com/Tyrrrz/YoutubeExplode)**: YouTube로부터 오디오 스트림을 추출합니다.
*   **[NAudio](https://github.com/naudio/NAudio)**: 추출된 오디오 스트림을 재생합니다.

## 🚀 빌드 및 실행 방법

1.  **저장소 복제**:
    ```bash
    git clone <repository-url>
    ```
2.  **Visual Studio에서 열기**:
    Visual Studio를 사용하여 `ytSound.sln` 솔루션 파일을 엽니다.

3.  **API 키 설정**:
    *   `App.Template.config` 파일의 복사본을 만들어 `App.config`로 이름을 변경합니다.
    *   `App.config` 파일을 열고 `YouTubeApiKey` 값에 자신의 YouTube Data API v3 키를 입력합니다.
    ```xml
    <appSettings>
        <add key="YouTubeApiKey" value="YOUR_API_KEY_HERE" />
        <add key="ApplicationName" value="ytSound" />
    </appSettings>
    ```

4.  **빌드 및 실행**:
    Visual Studio에서 프로젝트를 빌드(Ctrl+Shift+B)하고 시작(F5)합니다.

## 📂 소스 코드 구조

*   `ytSoundfrm.cs`: 메인 UI 양식 및 사용자 이벤트 처리 로직이 포함된 핵심 파일입니다.
*   `service/ytService.cs`: YouTube Data API와 통신하여 동영상 정보를 가져오는 서비스 클래스입니다.
*   `utility/YouTubeAudioPlayer.cs`: `YoutubeExplode`를 사용하여 오디오 스트림을 가져오고 `NAudio`로 재생하는 로직을 캡슐화합니다.
*   `utility/StorageUtility.cs`: 재생 목록을 로컬 파일 시스템에 `JSON` 형태로 저장하고 불러오는 기능을 담당합니다.
*   `utility/ListViewWithButtons.cs`: UI의 리스트 뷰 컨트롤을 커스터마이징합니다.
*   `dto/PlayListDto.cs`: 재생 목록 데이터 구조를 정의하는 DTO(Data Transfer Object)입니다.