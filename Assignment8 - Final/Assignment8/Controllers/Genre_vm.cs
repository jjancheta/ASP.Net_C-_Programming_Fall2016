using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment8.Controllers
{
    public class GenreBase
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name ="Genre")]
        public string Name { get; set; }
    }
}