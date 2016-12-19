using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment9.Controllers
{
    public class TrackBase
    {
        public TrackBase()
        {
           Albums = new List<AlbumBase>();
        }

        [Key]
        public int Id { get; set; }

        [Required, StringLength(200)]
        [Display(Name = "Track Name")]
        public string Name { get; set; }

        // Simple comma-separated string of all the track's composers
        [Required, StringLength(500)]
        public string Composers { get; set; }

        [Required]
        public string Genre { get; set; }

        // User name who added/edited the track
        [Required, StringLength(200)]
        public string Clerk { get; set; }

        public IEnumerable<AlbumBase> Albums { get; set; }


        //a property that will fetch the audio clip
        [Display(Name = "Sample clip")]
        public string AudioUrl
        {
            get
            {
                return $"/clip/{Id}";
            }
        }

    }

    public class TrackAddBase
    {
        public TrackAddBase()
        {
           Albums = new List<AlbumBase>();
        }

        [Display(Name = "Track Name")]
        [Required, StringLength(200)]
        public string Name { get; set; }

        // Simple comma-separated string of all the track's composers
        [Required, StringLength(500)]
        public string Composers { get; set; }

        [Required]
        public string Genre { get; set; }

        // User name who added/edited the track
        public string Clerk { get; set; }

        public IEnumerable<AlbumBase> Albums { get; set; }

        //For display purposes
        public string AlbumName { get; set; }

        [Display(Name = "Album cover")]
        public string AlbumCover { get; set; }
        public int AlbumId { get; set; }

    }

    public class TrackAddWithMedia : TrackAddBase
    {
        //Media item
        [Required]
        public HttpPostedFileBase AudioUpload { get; set; }

    }
    public class TrackAddForm : TrackAddBase
    {
        // Media item
        [Required]
        [Display(Name = "Sample clip")]
        [DataType(DataType.Upload)]
        public string AudioUpload { get; set; }

        [Display(Name = "Track genre")]
        public SelectList GenreList { get; set; }

    }

    public class TrackEditForm : TrackAddForm
    {
        [Key]
        public int Id { get; set; }
    }


    public class TrackAudio
    {
        public int Id { get; set; }
        public string AudioContentType { get; set; }
        public byte[] Audio { get; set; }
    }
}