using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using ytSound.utility;
using System.Drawing;
using ytSound.service;
using System.Collections.Generic;
using YoutubeExplode.Videos;

namespace ytSound
{
    public partial class ytSoundfrm : Form
    {
        ListViewWithButtons listViewLinkInfo;
        private YouTubeAudioPlayer audioPlayer;
        private string singleVideoType = "v";
        private string listVideoType = "list";

        public ytSoundfrm()
        {
            InitializeComponent();
            //txtListUrl.Text = testUrlForList;
            InitializeListView();
            audioPlayer = new YouTubeAudioPlayer();

            CheckSavedUrls();
        }

        

        private void InitializeListView()
        {
            listViewLinkInfo = new ListViewWithButtons
            {
                View = View.Details,
                FullRowSelect = true,
                CheckBoxes = true,
                Location = new Point(10, 100), // Set appropriate location
                Size = new Size(1000, 250) // Set appropriate size
            };

            listViewLinkInfo.Columns.Add("Title", 150);
            listViewLinkInfo.Columns.Add("Description", 250);
            listViewLinkInfo.Columns.Add("Channel Title", 150);
            listViewLinkInfo.Columns.Add("Published At", 100);
            listViewLinkInfo.Columns.Add("Duration", 100);
            listViewLinkInfo.Columns.Add("Status", 100);
            listViewLinkInfo.Columns.Add("VideoID", 0);


            this.Controls.Add(listViewLinkInfo);
        }

        
        private async void btnGet_Click(object sender, EventArgs e)
        {
            string url = txtListUrl.Text.ToString();
            if (Utility.IsValidUrl(url))
            {

                await HandleUrlAnync(url);
            }
            else
            {
                MessageBox.Show("Not a Valid URL");
            }
        }

        /// <summary>
        /// checks the url, and fech video info
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        async Task HandleUrlAnync(string url)
        {
            if (url.Contains(listVideoType + "="))
            {
                //await FetchPlayListAsync(url);
            }
            else
            {
                await FetchVideoAsync(url);
            }
        }

        private async void btnPlay_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listViewLinkInfo.Items)
            {
                if (item.Checked)
                {
                    btnControl(false);
                    string videoId = (string)item.Tag;
                    await audioPlayer.PlayAudioFromVideoAsync(videoId, item);                    
                    break;
                }
            }
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listViewLinkInfo.Items)
            {
                if (item.Checked)
                {
                    if (btnPause.Text == "Pause")
                    {
                        audioPlayer.PauseAudio(item);
                        btnPause.Text = "Resume";
                    }
                    else if (btnPause.Text == "Resume")
                    {
                        audioPlayer.ResumeAudio(item);
                        btnPause.Text = "Pause";
                    }
                    break;
                }

            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listViewLinkInfo.Items)
            {
                if (item.Checked)
                {
                    audioPlayer.StopAudio(item);
                    btnControl(true);
                    break;
                }
            }
        }

        /// <summary>
        /// it enables or disables ui controls.
        /// </summary>
        /// <param name="control"></param>
        private void btnControl(bool control)
        {
            btnPlay.Enabled = control;
            txtListUrl.Enabled = control;
        }
        


        // get a single video info
        async Task FetchVideoAsync(string url)
        {
            ytService ytService = new ytService();
            var videoId = ytService.ExtractVideoIdOrListID(url, singleVideoType);
            
            if (videoId == null)
            {
                MessageBox.Show("Invalid Yourube URL");
                return;
            }
            
            var videoDetails = await ytService.GetVideoDetailsAsync(videoId);
            if (videoDetails == null)
            {
                MessageBox.Show("Video not found or an error occurred.");
                return;
            }

            listViewLinkInfo.Items.Clear();
            SetListView(videoDetails);        
        }

        private void SetListView(Google.Apis.YouTube.v3.Data.Video videoDetails)
        {
            var listViewItem = new ListViewItem(videoDetails.Snippet.Title);
            listViewItem.Tag = videoDetails.Id;
            listViewItem.SubItems.Add(videoDetails.Snippet.Description);
            listViewItem.SubItems.Add(videoDetails.Snippet.ChannelTitle);
            listViewItem.SubItems.Add(videoDetails.Snippet.PublishedAt.ToString());
            listViewItem.SubItems.Add(videoDetails.ContentDetails.Duration);
            listViewItem.SubItems.Add("Stop");
            listViewItem.SubItems.Add(videoDetails.Id);
            listViewLinkInfo.Items.Add(listViewItem);
        }

        private void ClearText(object sender, EventArgs e)
        {
            txtListUrl.Text= string.Empty;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> checkedUrls = new Dictionary<string, string>();

            foreach (ListViewItem item in listViewLinkInfo.Items)
            {
                if (item.Checked)
                {
                    string title = item.Text;
                    string url = item.Tag?.ToString();
                    if (!string.IsNullOrEmpty(url))
                    {
                        checkedUrls.Add(title, url);
                    }
                }
            }

            StorageUtility.SaveCheckedUrls(checkedUrls);
            MessageBox.Show("Saved");
        }



        async Task CheckSavedUrls(bool reloadList = false)
        {
            var savedUrls = StorageUtility.LoadCheckedUrls();
            if (savedUrls.Count > 0)
            {
                var result = DialogResult.Yes;
                txtListUrl.Text = string.Empty;
                listViewLinkInfo.Items.Clear();

                //DialogResult = always Yes
                //unless, reloadList is false && No botton is pressed
                if (!reloadList)
                {
                    result = MessageBox.Show("Saved Data Exists. Do you want to reload?",
                        "Reload Data", MessageBoxButtons.YesNo);
                }

                if (result == DialogResult.Yes)
                {
                    foreach (var kvp in savedUrls)
                    {                        
                        await FetchVideo(kvp.Value.ToString());                       
                        
                    }
                }
            }
        }

        // get a single video info
        private async Task FetchVideo(string videoId)
        {
            ytService ytService = new ytService();            
            if (videoId == null)
            {
                MessageBox.Show("Invalid Yourube URL");
                return;
            }

            var videoDetails = await ytService.GetVideoDetailsAsync(videoId);
            if (videoDetails == null)
            {
                MessageBox.Show("Video not found or an error occurred.");
                return;
            }

            SetListView(videoDetails);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> checkedUrls = new Dictionary<string, string>();

            foreach (ListViewItem item in listViewLinkInfo.Items)
            {
                if (item.Checked)
                {
                    string title = item.Text;
                    string url = item.Tag?.ToString();
                    if (!string.IsNullOrEmpty(url))
                    {
                        checkedUrls.Add(title, url);
                    }
                }
            }

            StorageUtility.DeleteCheckedUrls(checkedUrls);
            MessageBox.Show("Deleted.");
            CheckSavedUrls(true);

        }



        //async Task FetchPlayListAsync(string url)
        //{
        //    ytService ytService = new ytService();

        //    var playlistId = ytService.ExtractVideoIdOrListID(url, listVideoType);

        //    if (playlistId == null)
        //    {
        //        MessageBox.Show("Invalid Yourube URL");
        //        return;
        //    }

        //    var playlistRequest = youtubeService.PlaylistItems.List("snippet");
        //    playlistRequest.PlaylistId = playlistId;
        //    playlistRequest.MaxResults = 50;

        //    var playlistResponse = await playlistRequest.ExecuteAsync();

        //    List<PlayListDto> playListDtos = new List<PlayListDto>();

        //    foreach (var item in playlistResponse.Items)
        //    {
        //        PlayListDto playListDto = new PlayListDto();
        //        //Thumbnail thumbnail = new Thumbnail();

        //        playListDto.Title = item.Snippet.Title;
        //        playListDto.Description = item.Snippet.Description;
        //        playListDto.ChannelId = item.Snippet.ChannelId;
        //        playListDto.ChannelTitle = item.Snippet.ChannelTitle;
        //        playListDto.PlaylistId = item.Snippet.PlaylistId;
        //        playListDto.VideoId = item.Snippet.ResourceId.VideoId;
        //        playListDto.PublishedAt = item.Snippet.PublishedAt;
        //        //thumbnail.URL = item.Snippet.Thumbnails.Default__.Url;
        //        //thumbnail.Width = (long)item.Snippet.Thumbnails.Default__.Width;
        //        //thumbnail.Height = (long)item.Snippet.Thumbnails.Default__.Height;
        //        //playListDto.Thumbnail = thumbnail;

        //        playListDtos.Add(playListDto);

        //    }

        //    var count = playListDtos.Count;

        //}


    }


}
