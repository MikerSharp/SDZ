using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SDZ2.Core;
using SDZ2.Domain;
using SDZ2.Domain.Entities;

namespace SDZ2.Controllers
{
    public class InstituteController : BaseController
    {
        public InstituteController(MyDbContext dbContext) : base(dbContext) {}
        public void Index()
        {
            var institutes = _dbContext.Institutes.ToArray();
            while (true)
            {
                var rez = Display.ShowWithSelect("****Выберите Институт****",
                    institutes.Select(x => x.Name).ToArray());
                if (rez == -1) return;
                else
                {
                    ShowCourses(institutes[rez].Id);
                }
            }
        }
        public void CreateNewInstitute()
        {

            Display.ShowText("****Создание нового института****", true);
            Display.ShowText("Введите имя нового института");
            string name = Console.ReadLine();
            Institute inst = new();
            inst.Name = name;
            inst.Courses = new List<Course>();
            for (int i = 1; i < 5; i++)
            {
                var c = new Course() { Number = i, Groups = new List<Group>(), Institute = inst };
                inst.Courses.Add(c);
                _dbContext.Courses.Add(c);
            }

            _dbContext.Institutes.Add(inst);
            _dbContext.SaveChanges();
        }

        public void RemoveInstitute()
        {
            var institutes = _dbContext.Institutes.ToArray();
            while (true)
            {

                var rez = Display.ShowWithSelect("****Выберите Институт для удаления****",
                    institutes.Select(x => x.Name).ToArray());

                if (rez == -1) return;

                var yesNo = Display.ShowWithSelect(
                    $"Вы уверены что хотите удалить институт [{institutes[rez].Name}]" +
                    $"\nвсе данные связаные с этим институтом будут утеряны",
                    new[] { "Да", "Нет" });
                if (yesNo == 0)
                {
                    _dbContext.Institutes.Remove(institutes[rez]);
                    _dbContext.SaveChanges();
                    return;
                }
            }
        }

        private void ShowCourses(int instituteId)
        {
            var institute = _dbContext.Institutes.Include(x => x.Courses).FirstOrDefault(x => x.Id == instituteId);
            if (institute is null)
            {
                Display.ShowError($"Инстиут не найден id:{instituteId}");
                return;
            }

            while (true)
            {
                var rez = Display.ShowWithSelect($"****[{institute.Name}->Курсы]****",
                    institute.Courses.Select(x => x.Number.ToString()).ToArray());
                if (rez == -1) return;
                else
                {
                    Groups(institute.Courses.ToArray()[rez].Id);
                }
            }

        }

        private void Groups(int courseId)
        {

            var course = _dbContext.Courses.Include(x => x.Groups).FirstOrDefault(x => x.Id == courseId);
            if (course is null)
            {
                Display.ShowError($"Курс не найден id:{courseId}");
                return;
            }

            while (true)
            {
                var rez = Display.ShowWithSelect($"****[{course.Institute.Name}->{course.Number} курс->Менеджер групп]****",
                    new[]
                    {
                        "Выбрать группу для просмотра и редактирования",
                        "Добавить группу",
                        "Удалить группу"
                    });
                if (rez == -1)
                {
                    return;
                }
                var groupC = new GroupController(_dbContext);
                if (rez == 0) groupC.Index(course);
                else if (rez == 1) groupC.CreateNewGroup(course);
                else if (rez == 2) groupC.RemoveGroup(course);
            }
        }
    }
}
