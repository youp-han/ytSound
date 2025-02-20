using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ytSound.dto
{
    internal class PlayListDto
    {
        public string Title { get; set; } // Items.Snippet.Title
        public string Description { get; set; } // Items.Snippet.Description
        public string ChannelId { get; set; } // Items.Snippet.ChennelId
        public string ChannelTitle { get; set; } // Items.Snippet.ChannelTitle
        public string PlaylistId { get; set; } // Items.Snippet.PlayListId
        public string VideoId { get; set; } // Items.Snippet.ResourceId.VideoId
        public DateTime? PublishedAt { get; set; } // Items.Snippet.PublishedAt
        //public Thumbnail Thumbnail { get; set; } // Adjust this according to your needs

    }

    //internal class Thumbnail
    //{
    //    public string URL { get; set; }
    //    public long Width { get; set; }
    //    public long Height { get; set; }
    //}


}
