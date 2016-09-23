using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using AutoMapper;
using Assignment3.Models;

namespace Assignment3.Controllers
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

        //Employee Entity
        public IEnumerable<EmployeeBase> EmployeeGetAll()
        {
            return mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeBase>>(ds.Employees);
        }


        public EmployeeBase EmployeeGetById(int id)
        {
            var o = ds.Employees.Find(id);
            return (o == null) ? null : mapper.Map<Employee, EmployeeBase>(o);
        }

        public EmployeeBase EmployeeEditContactInfo(EmployeeEditContactInfo newItem)
        {
            // Attempt to fetch the object
            var o = ds.Employees.Find(newItem.EmployeeId);

            if (o == null)
            {
                // Problem - item was not found, so return
                return null;
            }
            else
            {
                // Update the object with the incoming values
                ds.Entry(o).CurrentValues.SetValues(newItem);
                ds.SaveChanges();

                // Map and return the object
                return mapper.Map<Employee, EmployeeBase>(o);
            }
        }


        //Track Entity
        public IEnumerable<TrackBase> TrackGetAll()
        {
            var o = ds.Tracks
               .OrderBy(m => m.AlbumId)
               .ThenBy(m => m.Name);

            return mapper.Map<IEnumerable<Track>, IEnumerable<TrackBase>>(o);
        }

        public IEnumerable<TrackBase> TrackGetAllPop()
        {
            var o = ds.Tracks
                .Where(m => m.GenreId == 9)
                .OrderBy(m => m.Name);

            return mapper.Map<IEnumerable<Track>, IEnumerable<TrackBase>>(o);

        }

        public IEnumerable<TrackBase> TrackGetDeepPurple()
        {
            var o = ds.Tracks
                .Where(m => m.Composer.Contains("Jon Lord"))               
                .OrderBy(m => m.TrackId);

            return mapper.Map<IEnumerable<Track>, IEnumerable<TrackBase>>(o);
       
        }

        public IEnumerable<TrackBase> TrackGetAllTop100Longest()
        {
            var o = ds.Tracks
                .OrderByDescending(m => m.Milliseconds)
                .Take(100);

            return mapper.Map<IEnumerable<Track>, IEnumerable<TrackBase>>(o);
        }
        
    }
}