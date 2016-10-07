using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using AutoMapper;
using Assignment4.Models;

namespace Assignment4.Controllers
{
    public class Manager
    {
        // Reference to the data context
        private DataContext ds = new DataContext();

        // Get AutoMapper instance
        public IMapper mapper = AutoMapperConfig.RegisterMappings();

        public Manager()
        {
            // If necessary, add constructor code here
        }
        public IEnumerable<InvoiceBase> InvoiceGetAll()
        {
            var o = ds.Invoices
                .OrderBy(m => m.CustomerId)
                .ThenBy(m => m.InvoiceDate); 

            return mapper.Map<IEnumerable<Invoice>, IEnumerable<InvoiceBase>>(o);
        }

        public InvoiceBase InvoiceGetById(int id)
        {
            var o = ds.Invoices.Find(id);

            return (o == null ? null : mapper.Map<Invoice, InvoiceBase>(o));
        }

        public InvoiceWithDetail InvoiceGetByIdWithDetail(int id)
        {
            var o = ds.Invoices.Include("Customer.Employee")
                    .Include("InvoiceLines.Track.Album.Artist")
                    .Include("InvoiceLines.Track.MediaType")
                    .SingleOrDefault(t => t.InvoiceId == id);
            return (o==null) ? null: mapper.Map<Invoice, InvoiceWithDetail>(o);
        }

        // Suggested naming convention: Entity + task/action
        // For example:
        // ProductGetAll()
        // ProductGetById()
        // ProductAdd()
        // ProductEdit()
        // ProductDelete()
    }
}