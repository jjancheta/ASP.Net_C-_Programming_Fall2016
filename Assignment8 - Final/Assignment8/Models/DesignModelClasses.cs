using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using System.ComponentModel.DataAnnotations;

namespace Assignment8.Models
{
    // Add your design model classes below

    // Follow these rules or conventions:

    // To ease other coding tasks, the name of the 
    //   integer identifier property should be "Id"
    // Collection properties (including navigation properties) 
    //   must be of type ICollection<T>
    // Valid data annotations are pretty much limited to [Required] and [StringLength(n)]
    // Required to-one navigation properties must include the [Required] attribute
    // Do NOT configure scalar properties (e.g. int, double) with the [Required] attribute
    // Initialize DateTime and collection properties in a default constructor

    public class RoleClaim
    {

        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }
    }

    public class Artist
    {
        public Artist()
        {
            Albums = new List<Album>();
            BirthOrStartDate = new DateTime();
  
        }
        public int Id { get; set; }
        public string BirthName { get; set; }
        public DateTime BirthOrStartDate { get; set; }
        public string Executive { get; set; }
        public string Genre { get; set; }

        [Required, StringLength(50)]
        public string Name { get; set; }
        public string UrlArtist { get; set; }
        public ICollection<Album> Albums { get; set; }

    }

    public class Album
    {
        public Album()
        {
            Artists = new List<Artist>();
            Tracks = new List<Track>();
            ReleaseDate = new DateTime();
        }
        public int Id { get; set; }
        public string Coordinator { get; set; }
        public string Genre { get; set; }

        [Required, StringLength(50)]
        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string UrlAlbum { get; set; }
        public ICollection<Artist> Artists { get; set; }
        public ICollection<Track> Tracks { get; set; }

    }

    public class Track
    {
        public Track()
        {
            Albums = new List<Album>();
        }
        public int Id { get; set; }
        public string Clerk { get; set; }
        public string Composer { get; set; }
        public string Genre { get; set; }

        [Required, StringLength(50)]
        public string Name { get; set; }
        public ICollection<Album> Albums { get; set; }
    }

    public class Genre
    {
        public Genre()
        {

        }
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }

}
