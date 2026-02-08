namespace InvisibleMechanismsOfLogic.Task9_ComplexMultiThreadProcessing;

public class ComplexMultiThreadProcessing
{
    private const int SIZE = 1000000;
    private const int THREADS = 4;
    private static long _sum = 0;

    public static void Run() 
    {
        Random random = new Random();
        int chunkSize = SIZE / THREADS;
        
        IEnumerable<Thread> threads = Enumerable.Range(0, SIZE)
            .Select(_ => random.Next())
            .Chunk(chunkSize)
            .Select(chunk =>
            {
                Thread thread = new Thread(() =>
                {
                    long thisThreadSum = chunk
                        .Select(Convert.ToInt64)
                        .Sum();
                    Interlocked.Add(ref _sum, thisThreadSum);
                });
                
                return thread;
            });

        foreach (Thread thread in threads)
        {
            thread.Start();
        }
        
        foreach (Thread thread in threads)
        {
            try
            {
                thread.Join();
            }
            catch (ThreadInterruptedException e)
            {
                Console.WriteLine(e);
            }
        }

        Console.WriteLine($"Sum of all elements: {_sum}");
    }
}