using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SDZ2.Core;
using SDZ2.Domain;
using SDZ2.Domain.Entities;

namespace SDZ2.Controllers
{
    public class StudentController : BaseController
    {
        public StudentController(MyDbContext dbContext) : base(dbContext){}

        public void Index(Group group)
        {
            var students = _dbContext.Students.Include(x => x.Subjects).Where(x => x.Group.Id == group.Id).ToArray();

            while (true)
            {
                var rez = Display.ShowWithSelect($"****[{group.Institute.Name}->{group.Course.Number} курс->группа {group.Name}->Менеджер студентов]****",
                    students.Select(x => x.Name).ToArray());
                if (rez == -1) return;
                ShowStudentInfo(students[rez]);

            }
        }

        private void ShowStudentInfo(Student student)
        {
            var ss = _dbContext.Grades.Where(x => x.Student.Id == student.Id)
                .Select<Grade, (string sbj, string Score)>(x => new(x.Subject.Name, x.Score.ToString())).ToArray();

            Display.ShowWithSelect($"Имя: {student.Name}\nГруппа:{student.Group.Name}", ss);
        }

        public void AddStudent(Group group)
        {

            Display.ShowText($"****Добавление студента в группу [{group.Name}]****", true);
            Display.ShowText("Введите имя");
            string name = Console.ReadLine();


            var subjects = _dbContext.Subjects;

            var std = new Student();
            std.Name = name;
            std.Group = group;
            std.Subjects = new List<Subject>();
            var rnd = new Random();
            foreach (var subject in subjects)
            {
                var gr = new Grade() { Score = rnd.Next(1, 5), Student = std, Subject = subject };
                _dbContext.Grades.Add(gr);
                subject.Grades.Add(gr);
                subject.Students.Add(std);
                std.Subjects.Add(subject);
            }
            _dbContext.Students.Add(std);
            _dbContext.SaveChanges();
        }

        public void ShowAllWhereScore(int score)
        {
           var students = _dbContext.Students;
           List<Student> forDelete = new List<Student>();
           foreach (var student in students)
           {
               int scores = 0;
               var Grades = _dbContext.Grades.Where(x => x.Student == student).ToArray();
               foreach (var grade in Grades)
               {
                   if (grade.Score <= 2)
                   {
                       scores++;
                   }
               }

               if (scores>=2)
               {
                   forDelete.Add(student);
               }
           }

           var ordered = forDelete.OrderBy(x => x.Group.Name).OrderBy(x => x.Group.Course.Number).OrderBy(x => x.Group.Institute.Id).ToArray();
           List<string> toFile = new List<string>();
           Display.ShowText("Студенты на удаление",true);
           foreach (var deStudent in ordered)
           {
               Display.ShowText(deStudent.Name);
               toFile.Add($"{deStudent.Group.Institute.Name}->{deStudent.Group.Course.Number}->{deStudent.Group.Name}->{deStudent.Name}");
           }
           Display.ShowText(wait: true);

           File.WriteAllLines("DeletedStudents.txt",toFile);
        }

        public void RemoveStudent(Group group)
        {
            var students = group.Students.ToArray();
            while (true)
            {

                var rez = Display.ShowWithSelect("****Выберите студента для удаления****",
                    students.Select(x => x.Name).ToArray());

                if (rez == -1) return;

                var yesNo = Display.ShowWithSelect(
                    $"Вы уверены что хотите удалить студента [{students[rez].Name}]" +
                    $"\nвсе данные связаные с этим студентом будут утеряны",
                    new[] { "Да", "Нет" });
                if (yesNo == 0)
                {
                    _dbContext.Students.Remove(students[rez]);
                    _dbContext.SaveChanges();
                    return;
                }
            }
        }
    }
}
