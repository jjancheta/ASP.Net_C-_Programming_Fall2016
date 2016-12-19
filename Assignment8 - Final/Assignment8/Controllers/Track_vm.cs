using Assignment8.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment8.Controllers
{
    public class TrackBase
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(50)]
        [Display(Name = "Track Name")]
        public string Name { get; set; }

        [Display(Name ="Composer(s)")]
        public string Composer { get; set; }

        [Display(Name = "Track Genre")]
        public string Genre { get; set; }

        [Display(Name = "Clerk who helps with album tasks")]
        public string Clerk { get; set; }

    }

    public class TrackWithDetails : TrackBase
    {
        public TrackWithDetails()
        {
          //  AlbumNames = new List<String>();
            Albums = new List<Album>();
            Artists = new List<Artist>();
        }

        // public IEnumerable<String> AlbumNames { get; set; }  - changed with list of Albums to create links for Album details
        [Display(Name = "Albums with this track")]
        public IEnumerable<Album> Albums { get; set; }

        public IEnumerable<Artist> Artists { get; set; }

        [Display(Name = "Number of albums with this track")]
        public int AlbumsCount { get; set; }
    }

    public class TrackAdd
    {
        [Required, StringLength(50)]
        [Display(Name = "Track Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Composer(s)")]
        public string Composer { get; set; }

        [Display(Name = "Track Genre")]
        public string Genre { get; set; }

        [Display(Name = "Clerk who helps with album tasks")]
        public string Clerk { get; set; }

        [Required]
        public int AlbumId { get; set; }

    }

    public class TrackAddForm : TrackAdd
    {
        public string AlbumName { get; set; }

        [Display(Name = "Track genre")]
        public SelectList GenreList { get; set; }

        [Display(Name = "Album cover")]
        public string UrlAlbumCover { get; set; }

    }

    public class TrackEdit
    {
        public TrackEdit()
        {
          
        }
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string Name { get; set; }
        public string Composer { get; set; }
        public string Genre { get; set; }
        public string Clerk { get; set; }
    }

    public class TrackEditForm : TrackEdit
    {
        [Display(Name = "Track genre")]
        public SelectList GenreList { get; set; }
    }
}