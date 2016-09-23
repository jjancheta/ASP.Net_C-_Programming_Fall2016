using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment3.Controllers
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
    }
}