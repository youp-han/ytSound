using System;
using System.IO;
using System.Threading.Tasks;
using YoutubeExplode;
using YoutubeExplode.Videos.Streams;
using NAudio.Wave;
using System.Windows.Forms;
using AngleSharp.Html.Dom;

namespace ytSound.utility
{
    internal class YouTubeAudioPlayer
    {
        private readonly YoutubeClient _youtubeClient;
        private WaveOutEvent outputDevice;
        private AudioFileReader audioFile;
        private TaskCompletionSource<bool> playbackFinished;
        private int status = 5;

        public YouTubeAudioPlayer()
        {
            _youtubeClient = new YoutubeClient();
        }

        public async Task PlayAudioFromVideoAsync(string videoId, ListViewItem listViewItem)
        {
            listViewItem.SubItems[status].Text = "Fetching...";
            
            try
            {
                var streamManifest = await _youtubeClient.Videos.Streams.GetManifestAsync(videoId);
                var audioStreamInfo = streamManifest.GetAudioOnlyStreams().GetWithHighestBitrate();
                var stream = await _youtubeClient.Videos.Streams.GetAsync(audioStreamInfo);

                var tempFilePath = Path.Combine(Path.GetTempPath(), $"{videoId}.mp3");

                using (var fileStream = new FileStream(tempFilePath, FileMode.Create, FileAccess.Write))
                {
                    await stream.CopyToAsync(fileStream);
                }
                listViewItem.SubItems[status].Text = "Playing...";
                audioFile = new AudioFileReader(tempFilePath);
                outputDevice = new WaveOutEvent();
                outputDevice.Init(audioFile);


                playbackFinished = new TaskCompletionSource<bool>();
                outputDevice.PlaybackStopped += (sender, args) => playbackFinished.SetResult(true);

                outputDevice.Play();

                await playbackFinished.Task;

                listViewItem.SubItems[status].Text = "Stop";
                audioFile.Dispose();
                outputDevice.Dispose();
                File.Delete(tempFilePath); // Clean up temporary file
            }
            catch (Exception ex)
            {                
                MessageBox.Show("Youtube requires you to login to play this url. " +
                    "the feature will be added later. " +
                    "Please the stop button to conitue using the application. Thank you");
                quit(listViewItem);

            }          
            
        }

        public void PauseAudio(ListViewItem listViewItem)
        {
            if (outputDevice != null && outputDevice.PlaybackState == PlaybackState.Playing)
            {
                outputDevice.Pause();
                listViewItem.SubItems[status].Text = "Paused";
            }
        }

        public void ResumeAudio(ListViewItem listViewItem)
        {
            if (outputDevice != null && outputDevice.PlaybackState == PlaybackState.Paused)
            {
                outputDevice.Play();
                listViewItem.SubItems[status].Text = "Playing...";
            }
        }

        public void StopAudio(ListViewItem listViewItem)
        {
            if (outputDevice != null && (outputDevice.PlaybackState == PlaybackState.Playing || outputDevice.PlaybackState == PlaybackState.Paused))
            {
                outputDevice.Stop();
                listViewItem.SubItems[status].Text = "Stopped";

                audioFile.Dispose();
                outputDevice.Dispose();
            }

        }

        void quit(ListViewItem listViewItem)
        {            
            listViewItem.SubItems[status].Text = "Stop to Continue";
            if (outputDevice != null)
            {
                outputDevice.Stop();                
                outputDevice.Dispose();

                if (audioFile != null){
                    audioFile.Dispose();
                }
            }
            
        }
    }
}
