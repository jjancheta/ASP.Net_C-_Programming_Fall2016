using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment4.Controllers
{
    public class InvoiceLineBase
    {
        [Key]
        public int InvoiceLineId { get; set; }

        public int InvoiceId { get; set; }

        public int TrackId { get; set; }

        public decimal UnitPrice { get; set; }

        public int Quantity { get; set; }
    }

  public class InvoiceLineWithDetail : InvoiceLineBase
    {
        //NAVIGATION PROPERTIES

        //navigation property TO-ONE (InvoiceLine to Invoice)
        public InvoiceBase Invoice { get; set; }
        
        //some properties of Track entity - flattening
        public string TrackName { get; set; }
        public string TrackComposer { get; set; }

        //property of Album entity - flattening

        public string TrackAlbumTitle { get; set; }

        //property of Artist entity - flattening
        public string TrackAlbumArtistName { get; set; }

        //property of MediaType entity - flattening

        public string TrackMediaTypeName { get; set; }


    }
}