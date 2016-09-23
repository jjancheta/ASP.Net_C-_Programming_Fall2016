using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using AutoMapper;
using Assignment2.Models;

namespace Assignment2.Controllers
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

        public IEnumerable<EmployeeBase> EmployeeGetAll()
        {
            return mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeBase>>(ds.Employees);
        }

        public EmployeeBase EmployeeGetById(int id)
        {
            var o = ds.Employees.Find(id);
            return (o == null) ? null : mapper.Map<Employee, EmployeeBase>(o);
        }

        public EmployeeBase EmployeeAdd(EmployeeAdd newEmployee)
        {
            var addedEmployee = ds.Employees.Add(mapper.Map<EmployeeAdd, Employee>(newEmployee));
            ds.SaveChanges();
            return (addedEmployee == null) ? null : mapper.Map<Employee, EmployeeBase>(addedEmployee); 
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
    }
}