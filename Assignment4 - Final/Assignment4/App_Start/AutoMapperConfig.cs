using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using AutoMapper;

namespace Assignment4
{
    public static class AutoMapperConfig
    {
        public static IMapper RegisterMappings()
        {
            //  Using AutoMapper instance API
            // new MapperConfiguration(cfg => cfg.CreateMap< FROM , TO >());
            var config = new MapperConfiguration(cfg =>
            {
                // Add map creation statements here
                cfg.CreateMap<Models.Invoice, Controllers.InvoiceBase>();
                cfg.CreateMap<Models.Invoice, Controllers.InvoiceWithDetail>();
                cfg.CreateMap<Models.InvoiceLine, Controllers.InvoiceLineBase>();
                cfg.CreateMap<Models.InvoiceLine, Controllers.InvoiceLineWithDetail>();
              
            });

            var mapper = config.CreateMapper();
            // or: IMapper mapper = new Mapper(config);
            return mapper;

        }
    }
}