using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MvcPractice.Models.Enums;

namespace MvcPractice.Models
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder )
        {
            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    Id = 1,
                    Name = "Prithwiw",
                    Department = Dept.IT,
                    Email = "prethwiraj06@gmail.com"
                }
                );
        }
    }
}
