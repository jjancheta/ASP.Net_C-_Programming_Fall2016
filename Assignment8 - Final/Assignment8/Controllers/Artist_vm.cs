using Assignment8.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment8.Controllers
{
    public class ArtistBase
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "If applicable, artist's birth name")]
        public string BirthName { get; set; }

        [Display(Name = "Birth date or start date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]

        public DateTime BirthOrStartDate { get; set; }

        [Display(Name = "Executive who looks after this artist")]
        public string Executive { get; set; }
        [Display(Name = "Artist's primary genre")]
        public string Genre { get; set; }

        [Required, StringLength(50)]
        [Display(Name ="Artist name or stage name")]
        public string Name { get; set; }

        [Display(Name = "Artist photo")]
        public string UrlArtist { get; set; }

    }

    public class ArtistWithAlbum : ArtistBase
    {
        public ArtistWithAlbum()
        {
            Albums = new List<Album>();
        }
        [Display(Name = "Album(s)")]
        public IEnumerable<Album> Albums { get; set; }

        [Display(Name = "Album Count")]
        public int AlbumsCount { get; set; }

    }

    public class ArtistAdd
    {
        public ArtistAdd()
        {
            BirthOrStartDate = DateTime.Today;
        }
        [Required, StringLength(50)]
        [Display(Name = "Artist name or stage name")]
        public string Name { get; set; }

        [Display(Name = "If applicable, artist's birth name")]
        public string BirthName { get; set; }

        [Display(Name = "Birth date or start date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime BirthOrStartDate { get; set; }

        public string Executive { get; set; }

        [Display(Name = "Artist's primary genre")]
        public string Genre { get; set; }
       
        [Display(Name = "URL to artist photo")]
        public string UrlArtist { get; set; }

    }

    public class ArtistAddForm : ArtistAdd
    {
        [Display(Name = "Genre")]
        public SelectList GenreList { get; set; }
    }


}