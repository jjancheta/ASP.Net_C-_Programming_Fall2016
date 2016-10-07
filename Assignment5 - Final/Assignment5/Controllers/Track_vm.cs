using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment5.Controllers
{
    public class TrackBase
    {
        [Key]
        public int TrackId { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name ="Track Name")]
        public string Name { get; set; }

        [StringLength(220)]
        public string Composer { get; set; }

        [Display(Name = "Length (ms)")]
        public int Milliseconds { get; set; }

        [Display(Name ="Unit Price")]
        public decimal UnitPrice { get; set; }
    }

    public class TrackWithDetail : TrackBase
    {
        [Display(Name ="Artist Name")]
        public string AlbumArtistName { get; set; }

        [Display(Name = "Album Title")]
        public string AlbumTitle { get; set; }

        public MediaTypeBase MediaType { get; set; }
    }

    public class TrackAdd
    {
        [Required]
        [StringLength(200)]
        [Display(Name = "Track Name")]
        public string Name { get; set; }

        [StringLength(220)]
        public string Composer { get; set; }

        [Display(Name = "Length (ms)")]
        public int Milliseconds { get; set; }

        [Display(Name = "Unit Price")]
        public decimal UnitPrice { get; set; }

        //Identifier for associated item - Album Entity
        [Required(ErrorMessage = "The Album field is required.")]
        [Range(1, Int32.MaxValue)]
        public int AlbumId { get; set; }

        //Identifier for associated item - MediaType Entity
        [Required(ErrorMessage = "The Media Type field is required.")]
        [Range(1, Int32.MaxValue)]
        public int MediaTypeId { get; set; }

    }

    public class TrackAddForm : TrackAdd
    { 
        //SelectList for assiociated item - Album Entity
        [Display(Name = "Album")]
        public SelectList AlbumList { get; set; }


        //SelectList for associated item - MediaType Entity
        [Display(Name ="Media Type")]
        public SelectList MediaTypeList { get; set; }

    }
}