using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SDZ2.Core;
using SDZ2.Domain;
using SDZ2.Domain.Entities;

namespace SDZ2.Controllers
{
    public class GroupController : BaseController
    {
        public GroupController(MyDbContext dbContext) : base(dbContext){}

        public void Index(Course course)
        {
            var groups = _dbContext.Groups.Include(x=>x.Students).Where(x => x.Course.Id == course.Id).ToArray();
            while (true)
            {
                var rez = Display.ShowWithSelect($"****[{course.Institute.Name}->{course.Number} курс->Менеджер групп]****",
                    groups.Select(x => x.Name).ToArray());
                if (rez == -1) return;
                else
                {
                    Students(groups[rez]);
                }
            }
        }

        private void Students(Group group)
        {
            
            while (true)
            {
                var rez = Display.ShowWithSelect($"****[{group.Institute.Name}->{group.Course.Number} курс->группа {group.Name}->Менеджер студентов]****",
                    new[]
                    {
                        "Список студентов",
                        "Добавить студента",
                        "Удалить студента"
                    });
                if (rez == -1)
                {
                    return;
                }

                var stdC = new StudentController(_dbContext);
                if (rez == 0) stdC.Index(group);
                else if (rez == 1) stdC.AddStudent(group);
                else if (rez == 2) stdC.RemoveStudent(group);
            }
        }

        public void CreateNewGroup(Course course)
        {

            Display.ShowText("****Создание новой группы****", true);
            Display.ShowText("Введите название группы");
            string name = Console.ReadLine();
            Group newG = new Group()
            {
                Course = course,
                Institute = course.Institute,
                Name = name,
                Students = new List<Student>()
            };

            _dbContext.Groups.Add(newG);
            _dbContext.SaveChanges();

        }

        public void RemoveGroup(Course course)
        {
            var groups = course.Groups.ToArray();
            while (true)
            {
                var rez = Display.ShowWithSelect("****Выберите группу для удаления****",
                    groups.Select(x => x.Name).ToArray());

                if (rez == -1) return;

                var yesNo = Display.ShowWithSelect(
                    $"Вы уверены что хотите удалить группу [{groups[rez].Name}]" +
                    $"\nвсе данные связаные с этой группой будут утеряны",
                    new[] { "Да", "Нет" });
                if (yesNo == 0)
                {
                    _dbContext.Groups.Remove(groups[rez]);
                    _dbContext.SaveChanges();
                    return;
                }
            }
        }
    }
}
