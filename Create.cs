using EFhomework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EFhomework
{
    public class Create
    {
        public ICollection<Entities.Faculty> FacultysCreate()
        {
            List<Entities.Faculty> faculties = new List<Entities.Faculty>();
            Console.WriteLine("Set Faculty Name or you can go to creating Group (-N)-   ");
            string oo = Console.ReadLine();
            while (oo != "-N")
            {
                faculties.Add(new Entities.Faculty { Name = oo });
                oo = Console.ReadLine();
                Console.WriteLine("Set Faculty Name or you can go to creating Group (-N)-   ");
            }
            return faculties;
        }
        public ICollection<Entities.Group> GroupCreate()
        {
            List<Entities.Group> groups = new List<Entities.Group>();
            Console.WriteLine("Set Group Name or you can go to creating Student (-N) -   ");
            string oo = Console.ReadLine();
            int ff;
            while (oo != "-N")
            {
                Console.WriteLine("Set Faculty Id -   ");
                ff = Int32.Parse(Console.ReadLine());
                groups.Add(new Entities.Group { Name = oo, FacultyId = ff});
                Console.WriteLine("Set Group Name or you can go to creating Student (-N) -   ");
                oo = Console.ReadLine();
            }
            return groups;
        }
        public ICollection<Entities.Student> StudentCreate()
        {
            List<Entities.Student> students = new List<Entities.Student>();
            Console.WriteLine("Set Student Name or you can exit (-N) -   ");
            string oo = Console.ReadLine();
            int ff;
            string ss;
            int cc;
            while (oo != "-N")
            {
                Console.WriteLine("Set Student Surname -   ");
                ss = Console.ReadLine();
                Console.WriteLine("Set Student Course -   ");
                cc = Int32.Parse(Console.ReadLine());
                Console.WriteLine("Set Group Id -   ");
                ff = Int32.Parse(Console.ReadLine());
                students.Add(new Entities.Student { Name = oo, Surname = ss, Course = cc, GroupId = ff });
                Console.WriteLine("Set Student Name or you can exit (-N) -   ");
                oo = Console.ReadLine();
            }
            return students;
        }
        public void DoThisTask()
        {
            Create method = new Create();
            using (ApplicationContext db = new ApplicationContext())
            {

                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        db.AddRange(method.FacultysCreate());
                        db.AddRange(method.GroupCreate());
                        db.AddRange(method.StudentCreate());
                        db.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        Console.WriteLine("Error!");
                    }
                }

            }
        }
        public void Show()
        {
            using(ApplicationContext db = new ApplicationContext())
            {
                List<Group> groups = db.Groups.ToList();
                List<Student> students = db.Students.ToList();
                foreach (Faculty p in db.Facultys)
                {
                    Console.WriteLine("Faculty Name: {0}", p.Name);
                    foreach (Group g in groups.Where(x => x.FacultyId == p.Id))
                    {
                        Console.WriteLine("----Group name: {0}", g.Name);
                        foreach (Student s in students.Where(s => s.GroupId == g.Id))
                            Console.WriteLine("--------Student name: {0}   Surname: {1}   Course: {2}", s.Name, s.Surname, s.Course);
                    }

                }
            }
        }
    }
}
