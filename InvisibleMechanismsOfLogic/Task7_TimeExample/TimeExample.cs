using System.Globalization;

namespace InvisibleMechanismsOfLogic.Task6_TimeExample;

public class TimeExample
{
    // Эквивалент java коду 
    public void OriginalExample()
    {
        string dateString = "2024-05-13 14:30:00";
        string format = "yyyy-MM-dd HH:mm:ss";

        try
        {
            // Похоже проблема в том, что эти дата и время будут ни к чему не привязаны, соответственно могут исказиться при выводе
            // в консоль, поскольку вывод зависит от таймзоны и формата вывода.
            DateTime date = DateTime.ParseExact(dateString, format, CultureInfo.InvariantCulture);

            Console.WriteLine("Date: " + date);
        }
        catch (FormatException e)
        {
            Console.WriteLine(e);
        }
    }
    
    
    public void FixedExample()
    {
        string dateString = "2024-05-13 14:30:00";
        string format = "yyyy-MM-dd HH:mm:ss";

        try
        {
            // Похоже проблема в том, что эти дата и время будут ни к чему не привязаны, соответственно нарушается логика
            // работы с такими значениями. Любая попытка сериализации, вывода, преобразования формата приведёт к искажению данных 
            // в зависимости от локальных настроек машины на которой исполняется код. 
            // По этой причине явно указываем каким форматом времени оперируем (В данном случае UTC) чтобы на всех запускаемых
            // машинах результат был одинаковый.
            DateTime date = DateTime.ParseExact(dateString, format, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal);

            Console.WriteLine("Date: " + date);
        }
        catch (FormatException e)
        {
            Console.WriteLine(e);
        }
    }
}