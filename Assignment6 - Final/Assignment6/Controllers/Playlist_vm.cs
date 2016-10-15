using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment6.Controllers
{
    public class PlaylistBase
    {
        [Key]
        public int PlaylistId { get; set; }

        [StringLength(120)]
        [Display(Name ="Playlist name")]
        public string Name { get; set; }

        [Display(Name ="Number of tracks in this playlist")]
        public int TracksCount { get; set; }
    }

    public class PlaylistWithTracks : PlaylistBase
    {
        public PlaylistWithTracks()
        {
            Tracks = new List<TrackBase>();
        }
        public IEnumerable<TrackBase> Tracks { get; set; }
    }


    public class PlaylistEditWithTracksForm
    {
        public PlaylistEditWithTracksForm()
        {
            Tracks = new List<TrackBase>();
        }
        [Key]
        public int PlaylistId { get; set; }

        [StringLength(120)]
        public string Name { get; set; }

        [Display(Name = "Number of tracks in this playlist")]
        public int TracksCount { get; set; }

        public MultiSelectList TracksList { get; set; }

       public IEnumerable<TrackBase> Tracks { get; set; }
    }

    public class PlaylistEditWithTracks
    {
        public PlaylistEditWithTracks()
        {
            TrackIds = new List<int>();
        }
        [Key]
        public int PlaylistId { get; set; } 

        public IEnumerable<int> TrackIds { get; set; }
    }

    //self challenge - Add use case

    public class PlaylistWithTracksAdd
    {
        public PlaylistWithTracksAdd()
        {
            TrackIds = new List<int>();
        }

        [Required]
        [StringLength(120)]
        [Display(Name = "Playlist name")]
        public string Name { get; set; }

        public IEnumerable<int> TrackIds { get; set; }
    }

    public class PlaylistWithTracksAddForm : PlaylistWithTracksAdd
    {

        [Required(ErrorMessage = "The Track field is required.")]
        [Display(Name = "Tracks")]
        public MultiSelectList TracksList { get; set; }
    }
}
