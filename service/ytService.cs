using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace ytSound.service
{
    public class ytService
    {
        private static string api = ConfigurationManager.AppSettings["YouTubeApiKey"];
        private static string appName = ConfigurationManager.AppSettings["ApplicationName"];

        private string singleVideoType = "v";
        private string listVideoType = "list";

        YouTubeService youtubeService = new YouTubeService(new BaseClientService.Initializer()
        {
            ApiKey = api,
            ApplicationName = appName
        });

        public async Task<Video> GetVideoDetailsAsync(string videoId)
        {
            try
            {
                var videoRequest = youtubeService.Videos.List("snippet,contentDetails,statistics");
                videoRequest.Id = videoId;
                var videoResponse = await videoRequest.ExecuteAsync();
                return videoResponse.Items.FirstOrDefault();
            }
            catch (Google.GoogleApiException ex)
            {
                // Handle Google API exceptions specifically
               MessageBox.Show("Google API error: " + ex.Message);
                return null;
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                MessageBox.Show("An error occurred: " + ex.Message);
                return null;
            }
        }


        public string ExtractVideoIdOrListID(string url, string type)
        {
            var uri = new Uri(url);
            var query = HttpUtility.ParseQueryString(uri.Query);

            if (type.Contains(singleVideoType))
            {
                return query.Get(type);
            }
            else
            {
                return query[listVideoType];
            }

            
        }

    }


}

