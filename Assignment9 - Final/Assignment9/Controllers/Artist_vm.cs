using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment9.Controllers
{
    public class ArtistBase
    {
        public ArtistBase()
        {
            BirthOrStartDate = DateTime.Now.AddYears(-20);
        }

        [Key]
        public int Id { get; set; }

        [Required, StringLength(100)]
        [Display(Name = "Artist name or stage name")]
        public string Name { get; set; }

        [StringLength(100)]
        [Display(Name = "If applicable, artist's birth name")]
        public string BirthName { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Birth date or start date")]
        public DateTime BirthOrStartDate { get; set; }

        [Required, StringLength(200)]
        [Display(Name = "Artist photo")]
        public string UrlArtist { get; set; }

        [Required]
        [Display(Name = "Artist's primary genre")]
        public string Genre { get; set; }

        [Required, StringLength(200)]
        [Display(Name = "Executive who looks after this artist")]
        public string Executive { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Artist profile")]
        public string Profile { get; set; }

    }

    public class ArtistWithDetails : ArtistBase
    {
        public ArtistWithDetails()
        {
            Albums = new List<AlbumBase>();
            MediaItems = new List<MediaItemBase>();
        }
        public IEnumerable<AlbumBase> Albums { get; set; }

        [Display(Name = "Albums Count")]
        public int AlbumsCount { get; set; }

        public IEnumerable<MediaItemBase> MediaItems { get; set; }
    }

    public class ArtistAdd
    {
        public ArtistAdd()
        {
            BirthOrStartDate = DateTime.Now.AddYears(-20);
        }

        [Required, StringLength(100)]
        [Display(Name = "Artist name or stage name")]
        public string Name { get; set; }

        [StringLength(100)]
        [Display(Name = "If applicable, artist's birth name")]
        public string BirthName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Birth date or start date")]
        public DateTime BirthOrStartDate { get; set; }

        [Required, StringLength(200)]
        [Display(Name = "URL to artist photo")]
        public string UrlArtist { get; set; }

        [Required]
        [Display(Name = "Artist's primary genre")]
        public string Genre { get; set; }

        [Display(Name = "Executive who looks after this artist")]
        public string Executive { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Artist profile")]
        public string Profile { get; set; }

    }

    public class ArtistAddForm : ArtistAdd
    {
        [Display(Name = "Artist's primary genre")]
        public SelectList GenreList { get; set; }
    }

    public class ArtistEditForm : ArtistAddForm
    {
        [Key]
        public int Id { get; set; }

    }
}  