using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment9.Controllers
{
    public class MediaItemBase
    {
        [Key]
        public int Id { get; set; }

        public DateTime Timestamp { get; set; }

        // For the generated identifier
        [Required, StringLength(100)]
        public string StringId { get; set; }

        [StringLength(200)]
        public string ContentType { get; set; }

        // Brief descriptive caption
        [Required, StringLength(100)]
        public string Caption { get; set; }
    }

    public class MediaItemWithArtistIds
    {
        public MediaItemWithArtistIds()
        {
            MediaItems = new List<MediaItemBase>();
        }

        public IEnumerable<MediaItemBase> MediaItems { get; set; }
    }


    public class MediaItemAdd
    {
        public int ArtistId { get; set; }

        // Brief descriptive caption
        [Required, StringLength(100)]
        public string Caption { get; set; }

        // Media item
        [Required]
        public HttpPostedFileBase MediaUpload { get; set; }

    }

    public class MediaItemAddForm
    {
        public int ArtistId { get; set; }
        // Brief descriptive caption
        [Required, StringLength(100)]
        [Display(Name = "Descriptive caption")]
        public string Caption { get; set; }

        [Required]
        [Display(Name = "Media item")]
        [DataType(DataType.Upload)]
        public string MediaUpload { get; set; }

        public string ArtistName { get; set; }

        [Display(Name = "Artist photo")]
        public string ArtistPhoto { get; set; }
    }



    public class MediaItemContent
    {
        public int Id { get; set; }
        public string ContentType { get; set; }
        public byte[] Content { get; set; }
    }

}