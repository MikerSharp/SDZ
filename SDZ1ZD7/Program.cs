using System;
using System.Collections.Generic;
using System.Linq;

namespace SDZ1ZD7
{

    public class Student
    {
        private static int ids = 0;
        public int id = ++ids;
        public string firstName;
        public string lastName;
        public List<Grade> Grades { get; set; } = new();
    }
    public class Subject
    {
        private static int ids = 0; 
        public int id = ++ids;
        public string name;
    }
    public class Grade
    {
        private static int ids = 0;
        public int id = ++ids, stId, sbjId, grade;
    }
    public class LocalDb
    {
        public List<Student> students = new();
        public List<Subject> subjects = new();
        public List<Grade> grades = new();

        public LocalDb()
        {
            Init();
        }

        private void Init()
        {
            subjects.AddRange(new List<Subject>()
            {
                new Subject() { name = "Математика" },
                new Subject() { name = "Украинский" },
                new Subject() { name = "Економика" },
                new Subject() { name = "Программирование" }
            });
        }

    }
    
    public class MenuItem
    {
        public string name;
        public Action action;
    }

    class Program
    {

        static void Main(string[] args)
        {
            
            Console.Title = "Журнал";
            Console.CursorVisible = false;
            Menu();
        }

        enum CusorDir
        {
            Up,Down
        }

        static void Menu()
        {
            LocalDb localDb = new LocalDb();
            
            List<MenuItem> menu = new List<MenuItem>();
            menu.Add(new MenuItem() { name = "Добавить студента", action = (() => AddNewStudent()) });
            menu.Add(new MenuItem() { name = "Список всех студентов", action = (() => ShowAllStudents()) });
            menu.Add(new MenuItem() { name = "Список отличников", action = (() => ShowExcellentStudents()) });
            menu.Add(new MenuItem() { name = "Список должников", action = (() => ShowLosersStudents()) });
            int currentCursorPos = 0;
            ShowMenu(currentCursorPos, menu.Select(x => x.name).ToList());
            ConsoleKeyInfo key;
            while (true)
            {
                
                key = Console.ReadKey();
                if (key.Key == ConsoleKey.DownArrow && currentCursorPos < menu.Count - 1)
                {
                    CursorMove(CusorDir.Down, ref currentCursorPos);
                }
                if (key.Key == ConsoleKey.UpArrow && currentCursorPos > 0)
                {
                    CursorMove(CusorDir.Up, ref currentCursorPos);
                }
                if (key.Key == ConsoleKey.Enter)
                {
                    menu[currentCursorPos].action?.Invoke();
                    ShowMenu(currentCursorPos, menu.Select(x => x.name).ToList());
                }

                Console.SetCursorPosition(2, currentCursorPos);
            }


            void CursorMove(CusorDir dir, ref int CursorPos)
            {
                Console.SetCursorPosition(0, CursorPos);
                Console.Write("  ");
                if (dir == CusorDir.Up)
                    CursorPos -= 1;
                else
                    CursorPos += 1;
                Console.SetCursorPosition(0, CursorPos);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("->");
                Console.ResetColor();
            }

            void ShowMenu(int currentCursorPos, List<string> menu, bool clear = true)
            {
                if (clear) Console.Clear();
                menu.ForEach(x => Console.WriteLine($"   {x}"));
                Console.SetCursorPosition(0, currentCursorPos);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("->");
                Console.ResetColor();
            }

            //Actions
            void AddNewStudent()
            {
                Console.Clear();
                Student student = new Student();
                Console.Write("Введите имя: ");
                student.firstName = Console.ReadLine();
                Console.Write("Введите фамилию: ");
                student.lastName = Console.ReadLine();
                Console.Clear();
                Console.WriteLine("Заполнить оценки вручную?");

                int tmpCursorPos = Console.CursorTop, tmpBaseCursor = tmpCursorPos, selected = 0;
                ShowMenu(tmpCursorPos, new List<string>() { "Да", "Нет (Рандомное заполнение!)" }, false);
                while (true)
                {
                    key = Console.ReadKey();
                    if (key.Key == ConsoleKey.DownArrow && tmpCursorPos == tmpBaseCursor)
                    {
                        CursorMove(CusorDir.Down, ref tmpCursorPos);
                        selected++;
                    }
                    if (key.Key == ConsoleKey.UpArrow && tmpCursorPos == tmpBaseCursor + 1)
                    {
                        CursorMove(CusorDir.Up, ref tmpCursorPos);
                        selected--;
                    }
                    if (key.Key == ConsoleKey.Enter)
                    {
                        localDb.students.Add(student);
                        if (selected == 0)
                        {
                            SetGrades(student);
                        }
                        else if (selected == 1)
                        {
                            RandomGrades(student);
                        }
                        Console.Clear();
                        Console.WriteLine($"Студент {student.id} {student.lastName} {student.firstName} добавлен");
                        List<string> ls = student.Grades.Select(x => $"{localDb.subjects.Find(s => s.id == x.sbjId)?.name}:{x.grade}").ToList();
                        Console.WriteLine(string.Join('|', ls));
                        WaitMe();
                        return;

                    }
                    Console.SetCursorPosition(2, tmpCursorPos);
                }
                void SetGrades(Student student)
                {
                    Console.Clear();
                    List<Grade> grades = new List<Grade>();
                    foreach (var item in localDb.subjects)
                    {
                        int grade;
                        while (true)
                        {
                            Console.Write($"Введите оценку по {item.name} (0-5): ");
                            try
                            {
                                grade = int.Parse(Console.ReadLine());
                                break;
                            }
                            catch
                            {
                                Console.Write($"Не коректные данные");
                            }
                        }
                        grades.Add(new Grade() { grade = grade, sbjId = item.id, stId = student.id });
                    }
                    student.Grades.AddRange(grades);
                    localDb.grades.AddRange(grades);
                }
                void RandomGrades(Student student)
                {
                    Random rnd = new Random();
                    List<Grade> grades = new List<Grade>();
                    foreach (var item in localDb.subjects)
                    {
                        grades.Add(new Grade() { grade = rnd.Next(0, 6), sbjId = item.id, stId = student.id });
                    } 
                    student.Grades.AddRange(grades);
                    localDb.grades.AddRange(grades);
                }

            }

            void ShowAllStudents()
            {
                Console.Clear();

                foreach (var student in localDb.students)
                {
                    Console.Write($"[{student.id}] {student.lastName} {student.firstName} : ");
                    List<string> ls = student.Grades.Select(x => $"{localDb.subjects.Find(s => s.id == x.sbjId)?.name}:{x.grade}").ToList();
                    Console.WriteLine(string.Join('|', ls));
                }
                WaitMe();
                return;

            }
            void ShowExcellentStudents()
            {
                Console.Clear();
                var students = localDb.students.Where(x => (x.Grades.All(g => g.grade >= 4)));
                foreach (var student in students)
                {
                    Console.Write($"[{student.id}] {student.lastName} {student.firstName} : ");
                    List<string> ls = student.Grades.Select(x => $"{localDb.subjects.Find(s => s.id == x.sbjId)?.name}:{x.grade}").ToList();
                    Console.WriteLine(string.Join('|', ls));
                }
                WaitMe();
                return;
            }
            void ShowLosersStudents()
            {
                Console.Clear();
                var students = localDb.students.Where(x => (x.Grades.Any(g => g.grade <= 2)));
                foreach (var student in students)
                {
                    Console.Write($"[{student.id}] {student.lastName} {student.firstName} : ");
                    List<string> ls = student.Grades.Select(x => $"{localDb.subjects.Find(s => s.id == x.sbjId)?.name}:{x.grade}").ToList();
                    Console.WriteLine(string.Join('|', ls));
                }
                WaitMe();
                return;
            }

            void WaitMe()
            {
                Console.WriteLine("Нажмите любую клавишу для возвращения в меню");
                Console.ReadKey();
            }

        }
    }
}