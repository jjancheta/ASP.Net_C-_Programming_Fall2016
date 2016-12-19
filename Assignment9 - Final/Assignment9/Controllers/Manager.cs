using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using AutoMapper;
using Assignment9.Models;
using System.Security.Claims;
using System.IO;

namespace Assignment9.Controllers
{
    public class Manager
    {
        // Reference to the data context
        private ApplicationDbContext ds = new ApplicationDbContext();

        // Get AutoMapper instance
        public IMapper mapper = AutoMapperConfig.RegisterMappings();

        // Declare a property to hold the user account for the current request
        // Can use this property here in the Manager class to control logic and flow
        // Can also use this property in a controller 
        // Can also use this property in a view; for best results, 
        // near the top of the view, add this statement:
        // var userAccount = new ConditionalMenu.Controllers.UserAccount(User as System.Security.Claims.ClaimsPrincipal);
        // Then, you can use "userAccount" anywhere in the view to render content
        public UserAccount UserAccount { get; private set; }

        public Manager()
        {
            // If necessary, add constructor code here

            // Initialize the UserAccount property
            UserAccount = new UserAccount(HttpContext.Current.User as ClaimsPrincipal);

            // Turn off the Entity Framework (EF) proxy creation features
            // We do NOT want the EF to track changes - we'll do that ourselves
            ds.Configuration.ProxyCreationEnabled = false;

            // Also, turn off lazy loading...
            // We want to retain control over fetching related objects
            ds.Configuration.LazyLoadingEnabled = false;
        }

        // Add methods below
        // Controllers will call these methods
        // Ensure that the methods accept and deliver ONLY view model objects and collections
        // The collection return type is almost always IEnumerable<T>

        // Suggested naming convention: Entity + task/action
        // For example:
        // ProductGetAll()
        // ProductGetById()
        // ProductAdd()
        // ProductEdit()
        // ProductDelete()

        // ***************************
        // ARTIST ENTITY

        public IEnumerable<ArtistBase> ArtistGetAll()
        {
            return mapper.Map<IEnumerable<Artist>, IEnumerable<ArtistBase>>(ds.Artists.OrderBy(a => a.Name));
        }

        public ArtistWithDetails ArtistGetById(int id)
        {
            var o = ds.Artists.Include("Albums").SingleOrDefault(e => e.Id == id);
            return (o == null) ? null : mapper.Map<Artist, ArtistWithDetails>(o);
        }

        public ArtistBase ArtistAdd(ArtistAdd newItem)
        {
            var o = ds.Artists.Add(mapper.Map<ArtistAdd, Artist>(newItem));
            o.Executive = UserAccount.Name;
            ds.SaveChanges();
            return (o==null)? null : mapper.Map<Artist, ArtistBase>(o);                  
        }

        public ArtistWithDetails ArtistWithMediaItemsGetById(int id)
        {
            var o = ds.Artists.Include("MediaItems").Include("Albums").SingleOrDefault(p => p.Id == id);

            return (o == null) ? null : mapper.Map<Artist, ArtistWithDetails>(o);
        }

        public ArtistWithDetails ArtistEdit(ArtistEditForm newItem)
        {
            var o = ds.Artists.SingleOrDefault(a => a.Id == newItem.Id);

            if (o == null)
            {
                // Problem - item was not found, so return
                return null;
            }
            else
            {
                //Set the executive user
                newItem.Executive = UserAccount.Name;
                
                // Update the object with the incoming values
                ds.Entry(o).CurrentValues.SetValues(newItem);
                ds.SaveChanges();

                // Prepare and return the object
                return mapper.Map<Artist, ArtistWithDetails>(o);
            }
        }

        public bool ArtistDelete(int id)
        {
            var itemToDelete =ds.Artists.Include("MediaItems").Include("Albums.Tracks").SingleOrDefault(p => p.Id == id);

            if (itemToDelete == null)
            {
                return false;
            }
            else
            {
                // delete associated data - MediaItems
                if (itemToDelete.MediaItems.Count > 0)
                {
                    var mediaItems = itemToDelete.MediaItems.Select(m => new { m.Id, m.FileName }).ToList();
                    foreach (var mediaItem in mediaItems)
                    {
                       
                        ds.MediaItems.Remove(ds.MediaItems.Find(mediaItem.Id));
                        ds.SaveChanges();
                    }
                }

                // delete associated data - Albums
                if (itemToDelete.Albums.Count > 0)
                {
                    var albumsIds = itemToDelete.Albums.Select(a=>a.Id).ToList();
                    foreach (var albumId in albumsIds)
                    {
                        var album = ds.Albums.Find(albumId);
                       
                        //inner association album to tracks
                        if (album.Tracks.Count > 0)
                        {
                            var trackIds = album.Tracks.Select(t => t.Id).ToList(); 
                            foreach (var trackId in trackIds)
                            {
                                ds.Tracks.Remove(ds.Tracks.Find(trackId));
                                ds.SaveChanges();
                            }
                        }

                        //delete Album after deleting the Tracks
                        ds.Albums.Remove(ds.Albums.Find(album.Id));
                        ds.SaveChanges();
                    }
                }

                // delete the Artist
                ds.Artists.Remove(itemToDelete);
                ds.SaveChanges();

                return true;
            }
        }

        // ***************************
        // ALBUM ENTITY

        public IEnumerable<AlbumBase> AlbumGetAll()
        {
            return mapper.Map<IEnumerable<Album>, IEnumerable<AlbumBase>>(ds.Albums.OrderBy(a => a.Name));
        }

        public AlbumWithDetails AlbumGetById (int id)
        {
            var o = ds.Albums.Include("Artists").Include("Tracks").SingleOrDefault(e => e.Id == id);
            return (o == null) ? null : mapper.Map<Album, AlbumWithDetails>(o);
        }

        public AlbumWithDetails AlbumAdd(AlbumAdd newItem)
        {
            var a = ds.Artists.Find(newItem.ArtistId);

            if (a == null)
            {
                return null;
            }
            else
            {
                var o = ds.Albums.Add(mapper.Map<AlbumAdd, Album>(newItem));
                o.Artists.Add(a);
                o.Coordinator = UserAccount.Name;
                ds.SaveChanges();
                return (o == null) ? null : mapper.Map<Album, AlbumWithDetails>(o);
            }
        }


        public AlbumWithDetails AlbumEdit(AlbumEditForm newItem)
        {
            var o = ds.Albums.SingleOrDefault(a => a.Id == newItem.Id);

            if (o == null)
            {
                // Problem - item was not found, so return
                return null;
            }
            else
            {
                //Set the coordinator user
                newItem.Coordinator = UserAccount.Name;


                // Update the object with the incoming values
                ds.Entry(o).CurrentValues.SetValues(newItem);
                ds.SaveChanges();

                // Prepare and return the object
                return mapper.Map<Album, AlbumWithDetails>(o);
            }
        }

        public bool AlbumDelete(int id)
        {
            var itemToDelete = ds.Albums.Include("Tracks").SingleOrDefault(p => p.Id == id);

            if (itemToDelete == null)
            {
                return false;
            }
            else
            {
               //delete associated data - Tracks
               if (itemToDelete.Tracks.Count > 0)
               {
                    var trackIds = itemToDelete.Tracks.Select(t => t.Id).ToList();
                     foreach (var trackId in trackIds)
                     {
                        ds.Tracks.Remove(ds.Tracks.Find(trackId));
                        ds.SaveChanges();
                     }
               }

               //delete Album after deleting the Tracks
               ds.Albums.Remove(itemToDelete);
               ds.SaveChanges();

                return true;
            }
        }


        // ***************************
        // TRACK ENTITY

        public IEnumerable<TrackBase> TrackGetAll()
        {
            return mapper.Map<IEnumerable<Track>, IEnumerable<TrackBase>>(ds.Tracks.OrderBy(a => a.Name));
        }

        public TrackBase TrackGetById(int id)
        {
            var o = ds.Tracks.Find(id);
            return (o == null) ? null : mapper.Map<Track, TrackBase>(o);
        }

        public TrackBase TrackAdd(TrackAddWithMedia newItem)
        {
            var a = ds.Albums.Find(newItem.AlbumId);

            if (a == null)
            {
                return null;
            }
            else
            {
                var o = ds.Tracks.Add(mapper.Map<TrackAddWithMedia, Track>(newItem));

                // First, extract the bytes from the HttpPostedFile object
                byte[] audioBytes = new byte[newItem.AudioUpload.ContentLength];
                newItem.AudioUpload.InputStream.Read(audioBytes, 0, newItem.AudioUpload.ContentLength);
 
                o.Albums.Add(a);
                o.Clerk = UserAccount.Name;

                // Then, configure the new object's properties
                o.Audio = audioBytes;
                o.AudioContentType = newItem.AudioUpload.ContentType;
                o.FileName = Path.GetFileName(newItem.AudioUpload.FileName);

                ds.SaveChanges();

                return (o == null) ? null : mapper.Map<Track, TrackBase>(o);
            }
        }
     
        public TrackBase TrackEdit(TrackEditForm newItem)
        {

            var o = ds.Tracks.SingleOrDefault(a => a.Id == newItem.Id);

            if (o == null)
            {
                // Problem - item was not found, so return
                return null;
            }
            else
            {
                //Set the coordinator user
                newItem.Clerk = UserAccount.Name;


                // Update the object with the incoming values
                ds.Entry(o).CurrentValues.SetValues(newItem);
                ds.SaveChanges();

                // Prepare and return the object
                return mapper.Map<Track, TrackBase>(o);

            }
        }

        public bool TrackDelete(int id, HttpPostedFileBase file)
        {
            var itemToDelete = ds.Tracks.SingleOrDefault(p => p.Id == id);

            if (itemToDelete == null)
            {
                return false;
            }
            else
            {
               
                //delete Tracks
                ds.Tracks.Remove(itemToDelete);
                ds.SaveChanges();

                return true;
            }
        }


        // ***************************
        // TRACKAUDIO ENTITY  - for special-purpose media item delivery controller
        public TrackAudio TrackAudioGetById(int id)
        {
            var o = ds.Tracks.Find(id);
            return (o == null) ? null : mapper.Map<Track, TrackAudio>(o);
        }

        // ***************************
        // GENRE ENTITY

        public IEnumerable<GenreBase> GenreGetAll()
        {
            return mapper.Map<IEnumerable<Genre>, IEnumerable<GenreBase>>(ds.Genres.OrderBy(a => a.Name));
        }


        // ***************************
        // MEDIAITEM ENTITY

        public MediaItemContent ArtistMediaItemGetById(string stringId)
        {
            var o = ds.MediaItems.SingleOrDefault(p => p.StringId == stringId);

            return (o == null) ? null : mapper.Map<MediaItem, MediaItemContent>(o);
        }

        public ArtistBase MediaItemAdd(MediaItemAdd newItem)
        {
            var a = ds.Artists.Find(newItem.ArtistId);

            if (a == null)
            {
                return null;
            }
            else
            {
                var o = ds.MediaItems.Add(mapper.Map<MediaItemAdd, MediaItem>(newItem));
                o.Artist = a;

                // First, extract the bytes from the HttpPostedFile object
                byte[] mediaByte = new byte[newItem.MediaUpload.ContentLength];
                newItem.MediaUpload.InputStream.Read(mediaByte, 0, newItem.MediaUpload.ContentLength);

                // Then, configure the new object's properties
                o.Content = mediaByte;
                o.ContentType = newItem.MediaUpload.ContentType;
                o.FileName = Path.GetFileName(newItem.MediaUpload.FileName);

                ds.SaveChanges();

                return (o == null) ? null : mapper.Map<Artist, ArtistBase>(a);
            }
        }

        // Add some programmatically-generated objects to the data store
        // Can write one method, or many methods - your decision
        // The important idea is that you check for existing data first
        // Call this method from a controller action/method

        public List<string> RoleClaimGetAllStrings()
        {
            return ds.RoleClaims.OrderBy(r => r.Name).Select(r => r.Name).ToList();
        }

        public bool LoadData()
        {
            // User name
            var user = HttpContext.Current.User.Identity.Name;

            // Monitor the progress
            bool done = false;

            // ############################################################
            // RoleClaim

            if(ds.RoleClaims.Count() == 0)
            {
                ds.RoleClaims.Add(new RoleClaim { Name = "Executive" });
                ds.RoleClaims.Add(new RoleClaim { Name = "Coordinator" });
                ds.RoleClaims.Add(new RoleClaim { Name = "Clerk" });
                ds.RoleClaims.Add(new RoleClaim { Name = "Staff" });
                ds.RoleClaims.Add(new RoleClaim { Name = "Finance" });
                ds.RoleClaims.Add(new RoleClaim { Name = "Marketing" });
            }

            // ############################################################
            // Genre

            if (ds.Genres.Count() == 0)
            {
                // Add genres

                ds.Genres.Add(new Genre { Name = "Alternative" });
                ds.Genres.Add(new Genre { Name = "Classical" });
                ds.Genres.Add(new Genre { Name = "Country" });
                ds.Genres.Add(new Genre { Name = "Easy Listening" });
                ds.Genres.Add(new Genre { Name = "Hip-Hop/Rap" });
                ds.Genres.Add(new Genre { Name = "Jazz" });
                ds.Genres.Add(new Genre { Name = "Pop" });
                ds.Genres.Add(new Genre { Name = "R&B" });
                ds.Genres.Add(new Genre { Name = "Rock" });
                ds.Genres.Add(new Genre { Name = "Soundtrack" });

                ds.SaveChanges();
                done = true;
            }

            // ############################################################
            // Artist

            if (ds.Artists.Count() == 0)
            {
                // Add artists

                ds.Artists.Add(new Artist
                {
                    Name = "The Beatles",
                    BirthOrStartDate = new DateTime(1962, 8, 15),
                    Executive = user,
                    Genre = "Pop",
                    UrlArtist = "https://upload.wikimedia.org/wikipedia/commons/9/9f/Beatles_ad_1965_just_the_beatles_crop.jpg"
                });

                ds.Artists.Add(new Artist
                {
                    Name = "Adele",
                    BirthName = "Adele Adkins",
                    BirthOrStartDate = new DateTime(1988, 5, 5),
                    Executive = user,
                    Genre = "Pop",
                    UrlArtist = "http://www.billboard.com/files/styles/article_main_image/public/media/Adele-2015-close-up-XL_Columbia-billboard-650.jpg"
                });

                ds.Artists.Add(new Artist
                {
                    Name = "Bryan Adams",
                    BirthOrStartDate = new DateTime(1959, 11, 5),
                    Executive = user,
                    Genre = "Rock",
                    UrlArtist = "https://upload.wikimedia.org/wikipedia/commons/7/7e/Bryan_Adams_Hamburg_MG_0631_flickr.jpg"
                });

                ds.SaveChanges();
                done = true;
            }

            // ############################################################
            // Album

            if (ds.Albums.Count() == 0)
            {
                // Add albums

                // For Bryan Adams
                var bryan = ds.Artists.SingleOrDefault(a => a.Name == "Bryan Adams");

                ds.Albums.Add(new Album
                {
                    Artists = new List<Artist> { bryan },
                    Name = "Reckless",
                    ReleaseDate = new DateTime(1984, 11, 5),
                    Coordinator = user,
                    Genre = "Rock",
                    UrlAlbum = "https://upload.wikimedia.org/wikipedia/en/5/56/Bryan_Adams_-_Reckless.jpg"
                });

                ds.Albums.Add(new Album
                {
                    Artists = new List<Artist> { bryan },
                    Name = "So Far So Good",
                    ReleaseDate = new DateTime(1993, 11, 2),
                    Coordinator = user,
                    Genre = "Rock",
                    UrlAlbum = "https://upload.wikimedia.org/wikipedia/pt/a/ab/So_Far_so_Good_capa.jpg"
                });

                ds.SaveChanges();
                done = true;
            }

            // ############################################################
            // Track

            if (ds.Tracks.Count() == 0)
            {
                // Add tracks

                // For Reckless
                var reck = ds.Albums.SingleOrDefault(a => a.Name == "Reckless");

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { reck },
                    Name = "Run To You",
                    Composers = "Bryan Adams, Jim Vallance",
                    Clerk = user,
                    Genre = "Rock"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { reck },
                    Name = "Heaven",
                    Composers = "Bryan Adams, Jim Vallance",
                    Clerk = user,
                    Genre = "Rock"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { reck },
                    Name = "Somebody",
                    Composers = "Bryan Adams, Jim Vallance",
                    Clerk = user,
                    Genre = "Rock"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { reck },
                    Name = "Summer of '69",
                    Composers = "Bryan Adams, Jim Vallance",
                    Clerk = user,
                    Genre = "Rock"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { reck },
                    Name = "Kids Wanna Rock",
                    Composers = "Bryan Adams, Jim Vallance",
                    Clerk = user,
                    Genre = "Rock"
                });

                // For Reckless
                var so = ds.Albums.SingleOrDefault(a => a.Name == "So Far So Good");

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { so },
                    Name = "Straight from the Heart",
                    Composers = "Bryan Adams, Eric Kagna",
                    Clerk = user,
                    Genre = "Rock"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { so },
                    Name = "It's Only Love",
                    Composers = "Bryan Adams, Jim Vallance",
                    Clerk = user,
                    Genre = "Rock"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { so },
                    Name = "This Time",
                    Composers = "Bryan Adams, Jim Vallance",
                    Clerk = user,
                    Genre = "Rock"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { so },
                    Name = "(Everything I Do) I Do It for You",
                    Composers = "Bryan Adams, Jim Vallance",
                    Clerk = user,
                    Genre = "Rock"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { so },
                    Name = "Heat of the Night",
                    Composers = "Bryan Adams, Jim Vallance",
                    Clerk = user,
                    Genre = "Rock"
                });

                ds.SaveChanges();
                done = true;
            }

            return done;
        }

        public bool RemoveData()
        {
            try
            {
                foreach (var e in ds.MediaItems)
                {
                    ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
                }
                ds.SaveChanges();

                foreach (var e in ds.Tracks)
                {
                    ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
                }
                ds.SaveChanges();

                foreach (var e in ds.Albums)
                {
                    ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
                }
                ds.SaveChanges();

                foreach (var e in ds.Artists)
                {
                    ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
                }
                ds.SaveChanges();

                foreach (var e in ds.Genres)
                {
                    ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
                }
                ds.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool RemoveDatabase()
        {
            try
            {
                return ds.Database.Delete();
            }
            catch (Exception)
            {
                return false;
            }
        }

    }

    // New "UserAccount" class for the authenticated user
    // Includes many convenient members to make it easier to render user account info
    // Study the properties and methods, and think about how you could use it
    public class UserAccount
    {
        // Constructor, pass in the security principal
        public UserAccount(ClaimsPrincipal user)
        {
            if (HttpContext.Current.Request.IsAuthenticated)
            {
                Principal = user;

                // Extract the role claims
                RoleClaims = user.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);

                // User name
                Name = user.Identity.Name;

                // Extract the given name(s); if null or empty, then set an initial value
                string gn = user.Claims.SingleOrDefault(c => c.Type == ClaimTypes.GivenName).Value;
                if (string.IsNullOrEmpty(gn)) { gn = "(empty given name)"; }
                GivenName = gn;

                // Extract the surname; if null or empty, then set an initial value
                string sn = user.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Surname).Value;
                if (string.IsNullOrEmpty(sn)) { sn = "(empty surname)"; }
                Surname = sn;

                IsAuthenticated = true;
                IsAdmin = user.HasClaim(ClaimTypes.Role, "Admin") ? true : false;
            }
            else
            {
                RoleClaims = new List<string>();
                Name = "anonymous";
                GivenName = "Unauthenticated";
                Surname = "Anonymous";
                IsAuthenticated = false;
                IsAdmin = false;
            }

            NamesFirstLast = $"{GivenName} {Surname}";
            NamesLastFirst = $"{Surname}, {GivenName}";
        }

        // Public properties
        public ClaimsPrincipal Principal { get; private set; }
        public IEnumerable<string> RoleClaims { get; private set; }

        public string Name { get; set; }

        public string GivenName { get; private set; }
        public string Surname { get; private set; }

        public string NamesFirstLast { get; private set; }
        public string NamesLastFirst { get; private set; }

        public bool IsAuthenticated { get; private set; }

        // Add other role-checking properties here as needed
        public bool IsAdmin { get; private set; }

        public bool HasRoleClaim(string value)
        {
            if (!IsAuthenticated) { return false; }
            return Principal.HasClaim(ClaimTypes.Role, value) ? true : false;
        }

        public bool HasClaim(string type, string value)
        {
            if (!IsAuthenticated) { return false; }
            return Principal.HasClaim(type, value) ? true : false;
        }
    }

}