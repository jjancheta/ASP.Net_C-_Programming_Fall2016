using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment4.Controllers
{
    public class InvoiceBase
    {
        [Key]
        [Display(Name = "Invoice Number")]
        public int InvoiceId { get; set; }

        [Display(Name = "Customer ID")]
        public int CustomerId { get; set; }

        [Display(Name = "Invoice Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime InvoiceDate { get; set; }

        [StringLength(70)]
        [Display(Name = "Billing Address")]
        public string BillingAddress { get; set; }

        [StringLength(40)]
        [Display(Name = "Billing City")]
        public string BillingCity { get; set; }

        [StringLength(40)]
        [Display(Name = "Billing State")]
        public string BillingState { get; set; }

        [StringLength(40)]
        [Display(Name = "Billing Coutry")]
        public string BillingCountry { get; set; }

        [StringLength(10)]
        [Display(Name = "Postal Code")]
        public string BillingPostalCode { get; set; }

        [Display(Name = "Invoice Total")]
        public decimal Total { get; set; }
    }

    public class InvoiceWithDetail : InvoiceBase
    {
        //NAVIGATION PROPERTIES
        public InvoiceWithDetail()
        {
            //initialization
            InvoiceLines = new List<InvoiceLineWithDetail>();
        }

        //navigation property TO MANY (Invoice to InvoiceLine) 
        public IEnumerable<InvoiceLineWithDetail> InvoiceLines { get; set; }


        //some properties of Customer entity  -  flattening
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public string CustomerCity { get; set; }
        public string CustomerState { get; set; }

        //some properties of Employee entity through Customer entity  -  flattening
        public string CustomerEmployeeFirstName { get; set; }
        public string CustomerEmployeeLastName { get; set; }

       
    }

   
}