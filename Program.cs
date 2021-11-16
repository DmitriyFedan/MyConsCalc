using System.Collections.Generic;

MyCalc calc = new MyCalc();
calc.PrintStartMenu();

public class MyCalc
{
    private double Result = 0;
    private List<double> ResultsHist = new List<double>() { };

    public void PrintStartMenu()
    {
        Console.Clear();
        Console.WriteLine($"+----------------------------F.D----------------------------+\n" +
                          $"|          Вы в главном меню консольного калькулятора       |\n" +
                          $"+-----------------------------------------------------------+\n" +
                          $" Для получения справки введите - h, для сброса введите - с   \n" +
                          $"                                                               ");
        Console.Write("История: ");
        foreach (double res in ResultsHist)
            Console.Write($"{res} ");
        Console.WriteLine("");
        Console.WriteLine($"Текущий результат:  {Result}");
        CalcRun(); // вызываем калькулятор 
    }

    public void CalcRun()
    {
        bool CalcOn = true;
        while (CalcOn)
        {
            Console.Write("ВВОД: ");
            string ans = Console.ReadLine();

            if (ans == "h") PrintHelp();
            else if (ans == "m") PrintStartMenu();
            else if (ans == "c") ClearResult();
            else if (ans == "d") DelHistory();
            else Calculate(ans);
        }
    }
    public void Calculate(string ans)
    {
        try
        {
            char oper = ans[0];
            switch (oper)
            {
                case '+':
                    Result += Convert.ToDouble(ans.Substring(1, ans.Length - 1));
                    break;
                case '-':
                    Result -= Convert.ToDouble(ans.Substring(1, ans.Length - 1));
                    break;
                case '*':
                    Result *= Convert.ToDouble(ans.Substring(1, ans.Length - 1));
                    break;
                case '/':
                    if (Convert.ToDouble(ans.Substring(1, ans.Length - 1)) == 0)
                    {
                        Console.WriteLine("Отказываюсь делить на 0");
                        throw new Exception();
                    }
                    else
                        Result /= Convert.ToDouble(ans.Substring(1, ans.Length - 1));
                    break;
                case '^':
                    Result = Math.Pow(Result, Convert.ToDouble(ans.Substring(1, ans.Length - 1)));
                    break;
            }
            AddToResultsHist(Result);
            PrintStartMenu();
        }
        catch // вставим костыль тут, допилю позже 
        {
            Console.WriteLine("возникла ошибка, чтобы вызвать справку по работе введите - h ");
        }
    }

    public void AddToResultsHist(double newval)
    {
        if (ResultsHist.Count >= 10)
        {
            ResultsHist.RemoveAt(0);
            ResultsHist.Add(newval);
        }
        else ResultsHist.Add(newval);
    }
    public void PrintHelp()
    {
        Console.Clear();
        Console.WriteLine($"+----------------------------F.D----------------------------+\n" +
                          $"|                          Справка                          |\n" +
                          $"+-----------------------------------------------------------+\n" +

                           "В данный момент поддерживаются следующие комманды: \n" +
                           "                  +x, -x, /x, *x ^x                \n" +
                           "" +
                           "1) +x - суммирует значение x c предыдущим результатом  \n" +
                           "2) -x - вычитает значение x из предыдущего результата \n" +
                           "3) *х - умножает результат на х \n" +
                           "4) /х - делит результат на х \n" +
                           "5) ^х - возводит результат в степень х \n" +
                           "6) ^0.5 - вычисляет квадратный корень от результат \n" +
                           "История хранит 10 последних результатов\n" +
                           "\n" +
                           "Для сброса результата из памяти введите - с\n" +
                           "Для выхода в главное меню введите - m\n" +
                           "Для очистки истории введите - d ");
    }
    public void ClearResult()
    {
        Result = 0;
        PrintStartMenu();
    }
    public void DelHistory()
    {
        ResultsHist.Clear();
        PrintStartMenu();
    }
}

