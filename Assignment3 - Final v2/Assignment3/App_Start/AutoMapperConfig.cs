using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using AutoMapper;

namespace Assignment3
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
                cfg.CreateMap<Models.Employee, Controllers.EmployeeBase>();
                cfg.CreateMap<Controllers.EmployeeBase, Controllers.EmployeeEditContactInfoForm>();
                cfg.CreateMap<Models.Track, Controllers.TrackBase>();

            });

            var mapper = config.CreateMapper();
            // or: IMapper mapper = new Mapper(config);
            return mapper;

        }
    }
}