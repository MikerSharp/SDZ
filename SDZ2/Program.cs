using System;
using SDZ2.Domain;
using SDZ2.Controllers;

namespace SDZ2
{
    class Program
    {
        private static MyDbContext _context;
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            _context = new MyDbContext();
            (new DbInit(_context)).Init();
            MainController main = new MainController(_context);
            main.Index();
            _context.Dispose();
        }
    }
}