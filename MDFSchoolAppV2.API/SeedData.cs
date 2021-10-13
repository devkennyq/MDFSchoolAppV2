using MDFSchoolAppV2.Core;
using MDFSchoolAppV2.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MDFSchoolAppV2.API
{
    public class SeedData
    {
        public static void SeedInitialValues(MDFSchoolAppV2DbContext context)
        {
            if(!context.Student.Any())
            {
                var students = new List<Student>
                {
                    new Student
                    {
                        Address="addres value",
                        Email="email value",
                        FirstName="John Doe",
                        LastName="last name value",
                        Phone="Phone value"
                    }
                };

                var courses = new List<Course>
                {
                    new Course
                    {
                        Description ="First description of course",
                        Title = "First Course"
                    }
                };

                context.AddRange(students);
                context.AddRange(courses);
                context.SaveChanges();
            }
        }
    }
}
