namespace InvisibleMechanismsOfLogic.Task8_MultyThreadExample;

public class MultyThreadExample
{
    private static int counter = 0;

    public static void Run()
    {
        ThreadStart task = () =>
        {
            for (int i = 0; i < 1000; i++)
            {
                // Исправление: заменяет небехопасный инкремент counter++ на безопасный.
                Interlocked.Increment(ref counter);
            }
        };

        Thread thread1 = new Thread(task);
        Thread thread2 = new Thread(task);

        thread1.Start();
        thread2.Start();

        try
        {
            thread1.Join();
            thread2.Join();
        }
        catch (ThreadInterruptedException e)
        {
            Console.WriteLine(e.ToString());
        }

        Console.WriteLine("Counter: " + counter);
    }
}