using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using AutoMapper;
using Assignment8.Models;
using System.Security.Claims;

namespace Assignment8.Controllers
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


        public List<string> RoleClaimGetAllStrings()
        {
            return ds.RoleClaims.OrderBy(r => r.Name).Select(r => r.Name).ToList();
        }


        // ############################################################
        // LOAD INITIAL DATA

        // Load Initial Data / RoleClaim
        public bool LoadDataRoleClaim()
        {
            // User name
            var user = HttpContext.Current.User.Identity.Name;

            // Monitor the progress
            bool done = false;

            if (ds.RoleClaims.Count() == 0)
            { 
                // Create and add objects
                ds.RoleClaims.Add(new RoleClaim { Name = "Executive" });
                ds.RoleClaims.Add(new RoleClaim { Name = "Coordinator" });
                ds.RoleClaims.Add(new RoleClaim { Name = "Clerk" });
                ds.RoleClaims.Add(new RoleClaim { Name = "Staff" });

                // Save changes
                ds.SaveChanges();

                return true;
            }
            return done;
        }


        // Load Initial Data / Genre
        public bool LoadDataGenre()
        {
            if (ds.Genres.Count() == 0)
            {
                // Create and add objects
                ds.Genres.Add(new Genre { Name = "Pop" });
                ds.Genres.Add(new Genre { Name = "Rock" });
                ds.Genres.Add(new Genre { Name = "Jazz" });
                ds.Genres.Add(new Genre { Name = "Dance" });
                ds.Genres.Add(new Genre { Name = "R&B" });
                ds.Genres.Add(new Genre { Name = "Country" });
                ds.Genres.Add(new Genre { Name = "Folk" });
                ds.Genres.Add(new Genre { Name = "Hip hop" });
                ds.Genres.Add(new Genre { Name = "Blues" });
                ds.Genres.Add(new Genre { Name = "Classic" });

                // Save changes
                ds.SaveChanges();

                return true;
            }

            return false;
        }

        // Load Initial Data / Artist
        public bool LoadDataArtist()
        {
            if (ds.Artists.Count() == 0)
            {
                // Create and add objects
                ds.Artists.Add(new Artist
                {
                    Name = "Adele",
                    BirthName = "Adelle Adkins",
                    BirthOrStartDate = new DateTime(1988, 05, 08),
                    Executive = "exec@example.com",
                    Genre = "Pop",
                    UrlArtist = "https://images-na.ssl-images-amazon.com/images/I/71A0AHSw78L._SX450_.jpg"
                });

                ds.Artists.Add(new Artist
                {
                    Name = "Shayne Ward",
                    BirthName = "Shayne Thomas Ward",
                    BirthOrStartDate = new DateTime(1984, 10, 16),
                    Executive = "exec@example.com",
                    Genre = "Pop",
                    UrlArtist = "https://upload.wikimedia.org/wikipedia/en/7/79/Shayne_Ward_-_Shayne_Ward_(2006).JPG"
                });

                ds.Artists.Add(new Artist
                {
                    Name = "Westlife",
                    BirthName = "Nicky Bryne, Kian Egan, Mark Feehily, Shane Filan",
                    BirthOrStartDate = new DateTime(1988, 07, 03),
                    Executive = "exec@example.com", Genre = "Pop",
                    UrlArtist = "https://upload.wikimedia.org/wikipedia/en/2/2f/Westlife_-_Back_Home.jpg"
                });

                // Save changes
                ds.SaveChanges();

                return true;
            }

            return false;
        }

        // Load Initial Data / Album
        public bool LoadDataAlbum()
        {
            if (ds.Albums.Count() == 0)
            {
                //Adele
                var adele = ds.Artists.SingleOrDefault(a => a.Name == "Adele");
                ds.Albums.Add(new Album
                {
                    Artists = new List<Artist> { adele },
                    Name = "Adele 21",
                    ReleaseDate = new DateTime(2011, 01, 24),
                    Coordinator = "coord@example.com",
                    Genre = "Pop",
                    UrlAlbum = "https://images-na.ssl-images-amazon.com/images/I/71A0AHSw78L._SX450_.jpg"
                });

                ds.Albums.Add(new Album
                {
                    Artists = new List<Artist> { adele },
                    Name = "Rooling in the Deep",
                    ReleaseDate = new DateTime(2010, 11, 29),
                    Coordinator = "coord@example.com",
                    Genre = "Pop",
                    UrlAlbum = "https://upload.wikimedia.org/wikipedia/en/5/5d/Adele-Rolling_In_The_Deep.jpg"
                });

                //Shayne
                var shayne = ds.Artists.SingleOrDefault(a => a.Name == "Shayne Ward");
                ds.Albums.Add(new Album
                {
                    Artists = new List<Artist> { shayne },
                    Name = "Shayne Ward",
                    ReleaseDate = new DateTime(2006, 04, 17),
                    Coordinator = "coord@example.com",
                    Genre = "Pop",
                    UrlAlbum = "https://upload.wikimedia.org/wikipedia/en/7/79/Shayne_Ward_-_Shayne_Ward_(2006).JPG"
                });

                ds.Albums.Add(new Album
                {
                    Artists = new List<Artist> { shayne },
                    Name = "Closer",
                    ReleaseDate = new DateTime(2006, 04, 17),
                    Coordinator = "coord@example.com",
                    Genre = "Pop",
                    UrlAlbum = "https://upload.wikimedia.org/wikipedia/en/c/ce/Shayne_Ward_-_Closer_%28Official_Album_Cover%29.png"
                });

                //westlife
                var westlife = ds.Artists.SingleOrDefault(a => a.Name == "Westlife");
                ds.Albums.Add(new Album
                {
                    Artists = new List<Artist> { westlife },
                    Name = "Coast to Coast",
                    ReleaseDate = new DateTime(2000, 11, 06),
                    Coordinator = "coord@example.com",
                    Genre = "Pop",
                    UrlAlbum = "https://upload.wikimedia.org/wikipedia/en/7/7e/Coast_To_Coast_-_Westlife.Jpg"
                });

                ds.Albums.Add(new Album
                {
                    Artists = new List<Artist> { westlife },
                    Name = "Back Home",
                    ReleaseDate = new DateTime(2007, 11, 05),
                    Coordinator = "coord@example.com",
                    Genre = "Pop",
                    UrlAlbum = "https://upload.wikimedia.org/wikipedia/en/2/2f/Westlife_-_Back_Home.jpg"
                });

                // Save changes
                ds.SaveChanges();

                return true;
            }

            return false;
        }


        // Load Initial Data / Track
        public bool LoadDataTrack()
        {
            if (ds.Tracks.Count() == 0)
            {
                //adele21
                var adele = ds.Albums.SingleOrDefault(a => a.Name == "Adele 21");
                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { adele },
                    Name = "Turning Tables",
                    Composer = "Ryan Tedder",
                    Genre = "Pop",
                    Clerk = "clerk@example.com"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { adele },
                    Name = "Don't You Remember",
                    Composer = "Dan Wilson",
                    Genre = "Pop",
                    Clerk = "clerk@example.com"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { adele },
                    Name = "Rumour Has It",
                    Composer = "Ryan Tedder",
                    Genre = "Pop",
                    Clerk = "clerk@example.com"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { adele },
                    Name = "Set Fire to the Rain",
                    Composer = "Fraser Smith",
                    Genre = "Pop",
                    Clerk = "clerk@example.com"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { adele },
                    Name = "Someone like You",
                    Composer = "Dan Wilson",
                    Genre = "Pop",
                    Clerk = "clerk@example.com"
                });

                //shayne ward
                var shayne = ds.Albums.SingleOrDefault(a => a.Name == "Shayne Ward");
                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { shayne },
                    Name = "That's My Goal",
                    Composer = "Jorgen Elofsson, Jem Godfrey, Bill Padley",
                    Genre = "Pop",
                    Clerk = "clerk@example.com"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { shayne },
                    Name = "No Promises",
                    Composer = "Jonas Schroder, Lucas Sieber",
                    Genre = "Pop",
                    Clerk = "clerk@example.com"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { shayne },
                    Name = "Stand by Me",
                    Composer = "Andreas Romdhane, Savan Kotecha",
                    Genre = "Pop",
                    Clerk = "clerk@example.com"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { shayne },
                    Name = "All My Life",
                    Composer = "Karla Bonoff",
                    Genre = "Pop",
                    Clerk = "clerkd@example.com"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { shayne },
                    Name = "What About Me",
                    Composer = "Garry Frost, Frances Swan",
                    Genre = "Pop",
                    Clerk = "clerkd@example.com"
                });


                //coast to coast
                var coast = ds.Albums.SingleOrDefault(a => a.Name == "Coast to Coast");
                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { coast },
                    Name = "My Love",
                    Composer = "Jorgen Elofsson, David Kreuger, Per Magnusson, Pelle Nylen",
                    Genre = "Pop",
                    Clerk = "clerk@example.com"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { coast },
                    Name = "What Makes a Man",
                    Composer = "Steve Mac, Wayne Hector",
                    Genre = "Pop",
                    Clerk = "clerk@example.com"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { coast },
                    Name = "I Lay My Love on You",
                    Composer = "Jorgen Elofsson, David Kreuger, Per Magnusson",
                    Genre = "Pop",
                    Clerk = "clerk@example.com"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { coast },
                    Name = "I Have a Dream",
                    Composer = "Benny Andersson, Bjorn Ulvaeus",
                    Genre = "Pop",
                    Clerk = "clerk@example.com"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { coast },
                    Name = "When You're Looking Like That",
                    Composer = "Rami, Adreas Carlsson, Max Martin",
                    Genre = "Pop",
                    Clerk = "clerk@example.com"
                });

                // Save changes
                ds.SaveChanges();

                return true;
            }
            return false;
        }


        // ############################################################
        // REMOVE DATA

        //Remove Data / RoleClaims
        public bool RemoveDataRoleClaim()
        {
            try
            {
                foreach (var e in ds.RoleClaims)
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

        //Remove Data / Genre
        public bool RemoveDataGenre()
        {
            try
            {
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

        //Remove Data / Artist
        public bool RemoveDataArtist()
        {
            try
            {
                foreach (var e in ds.Artists)
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

        //Remove Data / Album
        public bool RemoveDataAlbum()
        {
            try
            {
                foreach (var e in ds.Albums)
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

        //Remove Data / Track
        public bool RemoveDataTrack()
        {
            try
            {
                foreach (var e in ds.Tracks)
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

        //DELETE DATABASE
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


        //ARTIST ENTITY
        public IEnumerable<ArtistBase> ArtistGetAll()
        {
            return mapper.Map<IEnumerable<Artist>, IEnumerable<ArtistBase>>(ds.Artists.OrderBy(a => a.Name));
        }

        public ArtistWithAlbum ArtistGetByIdWithAlbum(int id)
        {
            var o = ds.Artists.Include("Albums").SingleOrDefault(e => e.Id == id);

            return (o == null) ? null : mapper.Map<Artist, ArtistWithAlbum>(o);
            
        }

        public ArtistBase ArtistAdd(ArtistAdd newItem)
        {
            var o = ds.Artists.Add(mapper.Map<ArtistAdd, Artist>(newItem));
            o.Executive = UserAccount.Name;
            ds.SaveChanges();
            return mapper.Map<Artist, ArtistBase>(o);
        }

        //ALBUM ENTITY
        public IEnumerable<AlbumBase> AlbumGetAll()
        {
            return mapper.Map<IEnumerable<Album>, IEnumerable<AlbumBase>>(ds.Albums.OrderBy(a => a.Name));
        }

        public AlbumWithArtistAndTrack AlbumGetByIdWithDetails(int id)
        {
            var o = ds.Albums.Include("Tracks")
                .Include("Artists")
                .SingleOrDefault(e => e.Id == id);

            return (o == null) ? null : mapper.Map<Album, AlbumWithArtistAndTrack>(o);

        }

        public AlbumWithArtistAndTrack AlbumAdd(AlbumAdd newItem)
        {
            //check if artist/s exist/s before adding new album
            var a = new List<Artist>();
            foreach (var i in newItem.ArtistIds)
            {
                var artist = ds.Artists.Find(i);
                if (artist == null)
                {
                    return null;
                }
                else
                {
                    a.Add(artist);
                }
            }

            var o = ds.Albums.Add(mapper.Map<AlbumAdd, Album>(newItem));
            o.Coordinator = UserAccount.Name;
            o.Artists = a;
            ds.SaveChanges();
            return mapper.Map<Album, AlbumWithArtistAndTrack>(o);
            
        }

        //TRACK ENTITY
        public IEnumerable<TrackBase> TrackGetAll()
        {
            return mapper.Map<IEnumerable<Track>, IEnumerable<TrackBase>>(ds.Tracks.OrderBy(a => a.Name));
        }

        public TrackWithDetails TrackGetById(int id)
        {

            var o = ds.Tracks.Include("Albums.Artists")
                .SingleOrDefault(t => t.Id == id);

            if (o == null)
            {
                return null;
            }
            else
            {
                // Create the result collection
                var result = mapper.Map<Track, TrackWithDetails>(o);
                // Fill in the album names
                // result.AlbumNames = o.Albums.Select(a => a.Name);  -all Album properties were needed to display link for Album details
                return result;
            }
        }

        public TrackWithDetails TrackAdd(TrackAdd newItem)
        {
            //check if album exists before adding new track
            var a = ds.Albums.Find(newItem.AlbumId);
            
            if (a == null)
            {
                return null;
            }
            else
            {
                var o = ds.Tracks.Add(mapper.Map<TrackAdd, Track>(newItem));
                o.Clerk = UserAccount.Name;
                o.Albums.Add(a);
                ds.SaveChanges();
                return mapper.Map<Track, TrackWithDetails>(o);
            }
        }

        public TrackWithDetails TrackEdit(TrackEdit newItem)
        {
            // Attempt to fetch the object
            var o = ds.Tracks.Find(newItem.Id);

            if (o == null)
            {
                // Problem - item was not found, so return
                return null;
            }
            else
            {
                // Update the object with the incoming values
                // change clerk with the current user
                o.Clerk = UserAccount.Name;
                ds.Entry(o).CurrentValues.SetValues(newItem);
                ds.SaveChanges();

                // Prepare and return the object
                return mapper.Map<Track, TrackWithDetails>(o);
            }
        }
    

        public bool TrackDelete(int id)
        {
            var t = ds.Tracks.Find(id);
            if (t == null)
            {
                return false;
            }
            else
            {
                // Remove the object
                ds.Tracks.Remove(t);
                ds.SaveChanges();
                return true;
            }
        }


        //GENRE ENTITY
        public IEnumerable<GenreBase> GenreGetAll()
        {
            return mapper.Map<IEnumerable<Genre>, IEnumerable<GenreBase>>(ds.Genres.OrderBy(a => a.Name));
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