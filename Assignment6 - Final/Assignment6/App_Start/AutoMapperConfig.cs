using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using AutoMapper;

namespace Assignment6
{
    public static class AutoMapperConfig
    {
        public static IMapper RegisterMappings()
        {
            //  Using AutoMapper instance API
            // new MapperConfiguration(cfg => cfg.CreateMap< FROM , TO >());
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Models.Playlist, Controllers.PlaylistBase>();
                cfg.CreateMap<Models.Playlist, Controllers.PlaylistWithTracks>();
                cfg.CreateMap<Controllers.PlaylistWithTracks, Controllers.PlaylistEditWithTracksForm>();
                cfg.CreateMap<Controllers.PlaylistWithTracksAdd, Models.Playlist>();
                cfg.CreateMap<Models.Track, Controllers.TrackBase>();
                // Add map creation statements here


            });

            var mapper = config.CreateMapper();
            // or: IMapper mapper = new Mapper(config);
            return mapper;

        }
    }
}