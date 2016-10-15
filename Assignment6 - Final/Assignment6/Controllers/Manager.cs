using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using AutoMapper;
using Assignment6.Models;

namespace Assignment6.Controllers
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

        //PLAYLIST ENTITY
        public IEnumerable<PlaylistBase> PlaylistGetAll()
        {
            return mapper.Map<IEnumerable<Playlist>, IEnumerable<PlaylistBase>>(ds.Playlists.Include("Tracks").OrderBy(c => c.Name));
        }

        public PlaylistWithTracks PlaylistGetByIdWithTracks(int id)
        {
            var o = ds.Playlists.Include("Tracks").SingleOrDefault(e => e.PlaylistId == id);

            return (o == null) ? null : mapper.Map<Playlist, PlaylistWithTracks>(o);

        }

        //edit playlist
        public PlaylistWithTracks PlaylistEditTracks(PlaylistEditWithTracks newItem)
        {
            var o = ds.Playlists.Include("Tracks").SingleOrDefault(c => c.PlaylistId == newItem.PlaylistId);
            if (o == null)
            {
                return null;
            }
            else
            {
                o.Tracks.Clear();
                foreach (var t in newItem.TrackIds)
                {
                    var newTrack = ds.Tracks.Find(t);
                    o.Tracks.Add(newTrack);
                }
                ds.SaveChanges();
                return mapper.Map<Playlist, PlaylistWithTracks>(o);
            }
           
        }

        //Add new playlist - self challenge
      public PlaylistWithTracks PlaylistAndTracksAdd(PlaylistWithTracksAdd newItem)
        {
            var o = ds.Playlists.Add(mapper.Map<PlaylistWithTracksAdd, Playlist>(newItem));
            foreach (var t in newItem.TrackIds)
                {
                    var newTrack = ds.Tracks.Find(t);
                    o.Tracks.Add(newTrack);
                }
            ds.SaveChanges();
            return mapper.Map<Playlist, PlaylistWithTracks>(o);
       }


        //TRACKS ENTITY
        public IEnumerable<TrackBase> TrackGetAll()
        {
            return mapper.Map<IEnumerable<Track>, IEnumerable<TrackBase>>(ds.Tracks.OrderBy(c => c.Name));
        }

    }
}