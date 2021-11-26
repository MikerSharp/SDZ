using System;
using System.Collections.Generic;
using SDZ2.Domain.Entities;

namespace SDZ2.Domain
{
    public class DbInit
    {
        private MyDbContext _context;

        public DbInit(MyDbContext context) => _context = context;
        private List<Subject> subjects;
        public void Init()
        {

            subjects = new List<Subject>()
            {
                new Subject(){Name = "Subj 1",Grades = new List<Grade>(),Students = new List<Student>()},
                new Subject(){Name = "Subj 2",Grades = new List<Grade>(),Students = new List<Student>()},
                new Subject(){Name = "Subj 3",Grades = new List<Grade>(),Students = new List<Student>()},
                new Subject(){Name = "Subj 4",Grades = new List<Grade>(),Students = new List<Student>()},
                new Subject(){Name = "Subj 5",Grades = new List<Grade>(),Students = new List<Student>()},
            };

            List<Institute> insts = new List<Institute>()
            {
                new Institute() { Name = "Inst 1" },
                new Institute() { Name = "Inst 2" },
                new Institute() { Name = "Inst 3" },
                new Institute() { Name = "Inst 4" }
            };
            foreach (var inst in insts)
            {
                InitInstitute(inst);
            }
            _context.Institutes.AddRange(insts);
            _context.Subjects.AddRange(subjects);
            _context.SaveChanges();
        }

        private void InitInstitute(Institute inst)
        {
            List<Course> courses = new List<Course>();
            
            var rnd = new Random();
            for (int i = 1; i < 5; i++)
            {
                var course = new Course();
                course.Groups = new List<Group>();
                course.Institute = inst;
                course.Number = i;
                var groupsCount = rnd.Next(2, 6);
                for (int j = 0; j < groupsCount; j++)
                {
                    var g = NewGroup(j+1);
                    g.Course = course;
                    g.Institute = inst;
                    _context.Groups.Add(g);
                    course.Groups.Add(g);
                }

                _context.Courses.Add(course);
            }
            foreach (var course in courses)
            {
                inst.Courses.Add(course);
            }
        }

        private Group NewGroup(int name)
        {
            var group = new Group();
            group.Name = $"Group {name}";
            var rnd = new Random();
            var stdCount = rnd.Next(8, 20);
            var subjects = this.subjects;
            for (int i = 0; i < stdCount; i++)
            {
                var std = new Student();
                std.Name = $"Student {i + 1}";
                std.Group = group;
                std.Subjects = new List<Subject>();
                foreach (var subject in subjects)
                {
                    var gr = new Grade() { Score = rnd.Next(1, 5), Student = std, Subject = subject };
                    _context.Grades.Add(gr);
                    subject.Grades.Add(gr);
                    subject.Students.Add(std);
                    std.Subjects.Add(subject);
                }
                _context.Students.Add(std);
            }
            return group;
        }
    }
}
