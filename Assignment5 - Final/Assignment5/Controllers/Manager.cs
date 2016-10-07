using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using AutoMapper;
using Assignment5.Models;

namespace Assignment5.Controllers
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

        //ALBUM ENTITY
        public IEnumerable<AlbumBase> AlbumGetAll()
        {
            return mapper.Map<IEnumerable<Album>, IEnumerable<AlbumBase>>(ds.Albums.Include("Artist").OrderBy(c => c.Title));
        }

        public AlbumBase AlbumGetById(int id)
        {
            var o = ds.Albums.Include("Artist").SingleOrDefault(m => m.AlbumId == id);
            return (o == null) ? null : mapper.Map<Album, AlbumBase>(o);
        }

        //MEDIATYPE ENTITY
        public IEnumerable<MediaTypeBase> MediaTypeGetAll()
        {
            return mapper.Map<IEnumerable<MediaType>, IEnumerable<MediaTypeBase>>(ds.MediaTypes.OrderBy(c => c.Name));
        }

        public MediaTypeBase MediaTypeGetById(int id)
        {
            var o = ds.MediaTypes.Find(id);
            return (o == null) ? null : mapper.Map<MediaType, MediaTypeBase>(o);
        }

        //TRACK ENTITY
        public IEnumerable<TrackWithDetail> TrackWithDetailGetAll()
        {
            var o = ds.Tracks.Include("Album.Artist")
                .Include("MediaType")
                .OrderBy(c => c.Name)
                .ThenBy(c=> c.AlbumId);
            return mapper.Map<IEnumerable<Track>, IEnumerable<TrackWithDetail>>(o);
        }

        public TrackWithDetail TrackWithDetailGetById(int id)
        {
            var o = ds.Tracks.Include("Album.Artist")
                .Include("MediaType")
                .SingleOrDefault(m => m.TrackId == id);
            return (o == null) ? null : mapper.Map<Track, TrackWithDetail>(o);
        }

        public TrackWithDetail TrackAdd(TrackAdd newItem)
        {
            var a = ds.Albums.Find(newItem.AlbumId);
            var m = ds.MediaTypes.Find(newItem.MediaTypeId);

            if(a == null || m == null)
            {
                return null;
            }
            else
            {
                var addedItem = ds.Tracks.Add(mapper.Map<TrackAdd, Track>(newItem));
                addedItem.Album = a;
                addedItem.MediaType = m;
                ds.SaveChanges();
                return (addedItem == null) ? null : mapper.Map<Track, TrackWithDetail>(addedItem);
            }

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