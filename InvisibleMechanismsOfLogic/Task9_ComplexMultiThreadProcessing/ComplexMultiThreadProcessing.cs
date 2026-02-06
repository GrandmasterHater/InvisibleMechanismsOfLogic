namespace InvisibleMechanismsOfLogic.Task9_ComplexMultiThreadProcessing;

public class ComplexMultiThreadProcessing
{
    private const int SIZE = 1000000;
    private const int THREADS = 4;
    private static int[] _data = new int[SIZE];
    private static volatile int _sum = 0;

    public static void Run() 
    {
        Random random = new Random();
        
        for (int i = 0; i < SIZE; i++) 
        {
            _data[i] = random.Next(100);
        }
        int chunkSize = SIZE / THREADS;
        
        IEnumerable<Thread> threads = _data.Chunk(chunkSize)
            .Select(chunk =>
            {
                Thread thread = new Thread(() =>
                {
                    int thisThreadSum = chunk.Sum();
                    Interlocked.Add(ref _sum, thisThreadSum);
                });

                thread.Start();
                return thread;
            });
        
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