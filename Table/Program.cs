using System;
using System.Collections.Generic;

//SDZ1
Console.WriteLine("ZD1");
for (int i = 2; i < 10; i += 4)
{
    for (int g = 2; g < 10; g++)
    {
        Console.WriteLine(LineFactory(i,g,4));
    }
    Console.WriteLine();
}

string LineFactory(int number, int number2,int count)
{
    string rez = "";
    for (int i = 0; i < count; i++)
        rez += $"|{number+i}*{number2}={string.Format("{0,2}", ((number+i) * number2).ToString())}";
    return rez += "|";
}



//SDZ2
Console.WriteLine("ZD2");
while (true)
{
    Console.Write("Enter number (q): ");
    char Symbol = (char)Console.Read();
    Console.ReadLine();
    if (Symbol == 'q')
    {
        break;
    }
    if (Symbol >= '0' && Symbol <= '9')
    {
        Console.WriteLine("Is number " + Symbol);
    }
    else
    {
        Console.WriteLine("Is not number " + Symbol);
    }
}

Console.WriteLine();
Console.WriteLine();

//SDZ3
Console.WriteLine("ZD3");
double x = 4.32, y = 5;
Console.WriteLine($"X:{x}|Y:{y}");
Swap(ref x,ref y);
Console.WriteLine($"X:{x}|Y:{y}");
void Swap(ref double x, ref double y) => (x, y) = (y, x);


Console.WriteLine();
Console.WriteLine();
//SDZ4
Console.WriteLine("ZD4");
int min, max, n, m;
Console.Write("Укажите минимальное значение: ");
min = int.Parse(Console.ReadLine());
Console.Write("Укажите Максимальное значение: ");
max = int.Parse(Console.ReadLine()) + 1;

var rnd = new Random();
n = rnd.Next(3, 6);
m = rnd.Next(3, 6);
var arr = new int[n, m];
for (int i = 0; i < n; i++)
    for (int g = 0; g < m; g++)
        arr[i, g] = rnd.Next(min, max);
for (int i = 0; i < n; i++)
{
    for (int g = 0; g < m; g++)
    {
        Console.Write(arr[i, g]);
        if (g+1<m) Console.Write("|");
    }
    Console.WriteLine();
}

Console.WriteLine();
Console.WriteLine();
//SDZ5
Console.WriteLine("ZD5");
SDZ5();

void SDZ5()
{
    int n;
    Console.Write("Введите число: ");
    n = int.Parse(Console.ReadLine());

    var t = IsSimple(n) ? "" : " не";
    Console.WriteLine($"Число {n} -{t} является простым!");
    List<int> simple = new List<int>();
    for (int i = 2; i < n; i++)
        if (IsSimple(i))
            simple.Add(i);

    Console.WriteLine("Простые числа до вашего числа: " + string.Join("|",simple));

    bool IsSimple(int n)
    {
        int nt = n / 2;
        for (int i = 2; i <= nt; i++)
            if (n % i == 0) return false;
        return true;
    }



}
Console.WriteLine();
Console.WriteLine();
//SDZ6
Console.WriteLine("ZD6");
SDZ6();

void SDZ6() {
    while (true)
    {
        Console.ResetColor();
        Console.Write("Введите номер месяца (1-12): ");
        int month;
        try { month = int.Parse(Console.ReadLine()); }
        catch { continue; }
        if (month >= 3 && month <= 5)  ShowCurrentSeason("Весна",ConsoleColor.Yellow);
        else if(month >= 6 && month <= 8)  ShowCurrentSeason("Лето",ConsoleColor.Green);
        else if(month >= 9 && month <= 11) ShowCurrentSeason("Осень",ConsoleColor.Cyan);
        else if(month == 12 || month == 2 || month == 1) ShowCurrentSeason("Зима", ConsoleColor.Blue);
        else ShowCurrentSeason("Сезон не известен", ConsoleColor.Red);
    }
    void ShowCurrentSeason(string month,ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.CursorTop -= 1;
        Console.CursorLeft += 35;
        Console.WriteLine(month);
    }
    
}


