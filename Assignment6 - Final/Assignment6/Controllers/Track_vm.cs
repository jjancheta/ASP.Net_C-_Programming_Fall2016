using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment6.Controllers
{
    public class TrackBase
    {
        [Key]
        public int TrackId { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        public int? AlbumId { get; set; }

        public int MediaTypeId { get; set; }

        public int? GenreId { get; set; }

        [StringLength(220)]
        public string Composer { get; set; }

        public int Milliseconds { get; set; }

        public int? Bytes { get; set; }

        public decimal UnitPrice { get; set; }

        public string NameFull
        {
            get
            {
                var ms = Math.Round((((double)Milliseconds / 1000) / 60), 1);

                var composer = string.IsNullOrEmpty(Composer) ? "" : ", composer " + Composer;
                var trackLength = (ms > 0) ? ", " + ms.ToString() + " minutes" : "";
                var unitPrice = (UnitPrice > 0) ? ", $ " + UnitPrice.ToString() : "";

                return string.Format("{0}{1}{2}{3}", Name, composer, trackLength, unitPrice);
            }
        }

        public class TrackWithPlayList : TrackBase
        {
            public TrackWithPlayList()
            {
                Playlists = new List<PlaylistBase>();
            }
            public IEnumerable<PlaylistBase> Playlists { get; set; }
        }
    }
}