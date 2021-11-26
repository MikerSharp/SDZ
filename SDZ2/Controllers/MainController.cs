using System;
using SDZ2.Core;
using SDZ2.Domain;

namespace SDZ2.Controllers
{
    public class MainController : BaseController
    {
        public MainController(MyDbContext context) : base(context){}

        public void Index()
        {
            while (true)
            {
                var rez = Display.ShowWithSelect("****Главное Меню****\nУправление с помощью (стрелок, enter, backspace)", new[]
                {
                    "Выбрать институт для просмотра и редактирования",
                    "Добавить институт",
                    "Удалить институт",
                    "Показать студентов с 2 и более двоек за сессию"
                });
                
                if (rez == -1)
                {
                    var yesNo = Display.ShowWithSelect(
                        $"Вы уверены что хотите выйти?", new[] { "Да", "Нет" });
                    if (yesNo == 0)
                    {
                        Console.Clear();
                        break;
                    }
                }
                var instView = new InstituteController(_dbContext);
                if (rez == 0) instView.Index();
                else if (rez == 1) instView.CreateNewInstitute();
                else if (rez == 2) instView.RemoveInstitute();
                else if (rez == 3) new StudentController(_dbContext).ShowAllWhereScore(2);
            }
        }
    }
}
