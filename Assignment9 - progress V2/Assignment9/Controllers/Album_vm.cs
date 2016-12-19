using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment9.Controllers
{
    public class AlbumBase
    {
        public AlbumBase()
        {
            ReleaseDate = DateTime.Now;
        }

        [Key]
        public int Id { get; set; }

        [Required, StringLength(100)]
        [Display(Name = "Album Name")]
        public string Name { get; set; }

        [Display(Name = "Release Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime ReleaseDate { get; set; }

        [Required, StringLength(200)]
        [Display(Name = "Album cover art")]
        public string UrlAlbum { get; set; }

        [Required]
        [Display(Name = "Album's primary genre")]
        public string Genre { get; set; }

        [Required, StringLength(200)]
        public string Coordinator { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Album description")]
        public string Description { get; set; }
    }

    public class AlbumWithDetails : AlbumBase
    {
        public AlbumWithDetails()
        {
            Artists = new List<ArtistBase>();
            Tracks = new List<TrackBase>();
        }
        public IEnumerable<ArtistBase> Artists { get; set; }

        public IEnumerable<TrackBase> Tracks { get; set; }

    }

    public class AlbumAdd
    {
       
        [Required, StringLength(100)]
        [Display(Name = "Album Name")]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Release Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ReleaseDate { get; set; }

        [Required, StringLength(200)]
        [Display(Name = "Album cover art")]
        public string UrlAlbum { get; set; }

        [Required]
        [Display(Name = "Album's primary genre")]
        public string Genre { get; set; }

        public string Coordinator { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Album description")]
        public string Description { get; set; }

        //For Display purposes
        public int ArtistId { get; set; }

        public string ArtistName { get; set; }

        [Display(Name = "Artist photo")]
        public string ArtistPhoto { get; set; }
    }

    public class AlbumAddForm : AlbumAdd
    {
        [Display(Name = "Album's primary genre")]
        public SelectList GenreList { get; set; }

    }
}