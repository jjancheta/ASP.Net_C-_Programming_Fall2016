using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using AutoMapper;

namespace Assignment9
{
    public static class AutoMapperConfig
    {
        public static IMapper RegisterMappings()
        {
            // Create map statements - using AutoMapper instance API
            // new MapperConfiguration(cfg => cfg.CreateMap< FROM , TO >());
            var config = new MapperConfiguration(cfg =>
            {

                cfg.CreateMap<Models.RegisterViewModel, Models.RegisterViewModelForm>();

                //ARTIST
                cfg.CreateMap<Models.Artist, Controllers.ArtistBase>();                 //getAll
                cfg.CreateMap<Models.Artist, Controllers.ArtistWithDetails>();          //getOne
                cfg.CreateMap<Controllers.ArtistAdd, Models.Artist>();                  //add
                cfg.CreateMap<Controllers.ArtistAdd, Controllers.ArtistAddForm>();      //add error
                cfg.CreateMap<Models.Artist, Controllers.ArtistAddForm>();              //edit

                //ALBUM
                cfg.CreateMap<Models.Album, Controllers.AlbumBase>();                   //getAll
                cfg.CreateMap<Models.Album, Controllers.AlbumWithDetails>();            //getOne
                cfg.CreateMap<Controllers.AlbumAdd, Models.Album>();                    //add
                cfg.CreateMap<Controllers.AlbumAdd, Controllers.AlbumAddForm>();        //add error

                //TRACK
                cfg.CreateMap<Models.Track, Controllers.TrackBase>();                   //getAll

                //GENRE
                cfg.CreateMap<Models.Genre, Controllers.GenreBase>();                   //getAll

            });

            var mapper = config.CreateMapper();
            // or: IMapper mapper = new Mapper(config);
            return mapper;

        }
    }
}