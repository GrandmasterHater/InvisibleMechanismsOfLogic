namespace InvisibleMechanismsOfLogic.Task6_SynchronizationMechanisms;

// 1. ReaderWriterLockSlim
public class ReaderWriterLockSlimExample
{
     // Аналог ReentrantReadWriteLock из Java. 
    public void RunMain(string[] args)
    { 
        ReaderWriterLockSlim lockSlim = new ReaderWriterLockSlim();
        List<string> datas = new List<string>() {"ExampleData1", "ExampleData2", "ExampleData3"};
        
        Task writeTask = new Task(() =>
        {
            while (true)
            {
                lockSlim.EnterWriteLock();

                try
                {
                    for (var index = 0; index < datas.Count; index++)
                    {
                        datas[index] = DateTime.Now.ToString("HH:mm:ss");
                    }
                }
                finally
                {
                    lockSlim.ExitWriteLock();
                }
                
                Thread.Sleep(1000);
            }
        });
        
        int readTasksCount = 5;
        Task[] readTasks = new Task[readTasksCount];
        Random random = new Random();

        for (int i = 0; i < readTasksCount; i++)
        {
            readTasks[i] = new Task(() =>
            {
                while (true)
                {
                    lockSlim.EnterReadLock();

                    try
                    {
                        int index = random.Next(datas.Count);
                        Console.WriteLine($"Data: {datas[index]}, reader: {index}");
                    }
                    finally
                    {
                        lockSlim.ExitReadLock();
                    }

                    Thread.Sleep(500);
                }
            });
        }
        
        writeTask.Start();
        Task.WhenAll(readTasks);
    }
}

// 2. AutoResetEvent
public class AutoResetEventExample
{
    // Две задачи ожидают вызов события. При первом вызове события, сначала сработает одна задача, при следующем вызове - вторая.
    public void RunExample()
    {
        AutoResetEvent autoResetEvent = new AutoResetEvent(false);

        Task waitTaskFirst = new Task(() =>
        {
            Console.WriteLine($"Поток {Thread.CurrentThread.Name}: ожидает событие.");
            autoResetEvent.WaitOne();
            Console.WriteLine($"Поток {Thread.CurrentThread.Name}: завершил ожидание события.");
        });
        
        Task waitTaskSecond = new Task(() =>
        {
            Console.WriteLine($"Поток {Thread.CurrentThread.Name}: ожидает событие.");
            autoResetEvent.WaitOne();
            Console.WriteLine($"Поток {Thread.CurrentThread.Name}: завершил ожидание события.");
        });
        
        Task raiseEventTask = new Task(() =>
        {
            Console.WriteLine("Скоро вызовем событие");
            Thread.Sleep(1000);
            Console.WriteLine($"Вызываем событие в потоке {Thread.CurrentThread.Name}");
            autoResetEvent.Set();
            Thread.Sleep(1000);
            Console.WriteLine($"Еще раз вызываем событие в потоке {Thread.CurrentThread.Name}");
            autoResetEvent.Set();
        });

        waitTaskFirst.Start();
        waitTaskSecond.Start();
        raiseEventTask.Start();
    }
} 

// 3. Interlocked.Barriers
public class InterlockedExample
{
    private bool _isHalfCountdownPassed = false;
    
    // Аналог Barriers из Java. 
    public void RunExample()
    {
        int count = 0;

        Task incrementTask = new Task(() =>
        {
            Thread.Sleep(50);
            
            for (int i = 0; i < 100000; i++)
            {
                count++;

                if (i > 50000 && !_isHalfCountdownPassed)
                {
                    // Гарантируем, что чтение переменной _isHalfCountdownPassed будет строго после инкремента.
                    Interlocked.MemoryBarrier();
                    _isHalfCountdownPassed = true;
                }
            }
        });
        
        Task valueWaiter = new Task(() =>
        {
            while (!_isHalfCountdownPassed)
            {
                Thread.SpinWait(100);
            }
            
            Interlocked.MemoryBarrier();
            
            Console.WriteLine("Половину отсчёта уже прошли!");
        });
        
        incrementTask.Start();
        valueWaiter.Start();
    }
}

// 4. Volatile
public class VolatileExample
{
    // Аналог Interlocked.Barriers, но скрывающий прямые вызовы барьера и, в некоторых случаях, более производительный. 
    // Помимо гарантии порядка операций, также гарантирует чтение актуального значения переменной из любого потока.
    // Ключевое слово volatile гарантирует, что чтение переменной _isHalfCountdownPassed будет строго после инкремента,
    // а его значение будет актуальным.
    volatile bool _isHalfCountdownPassed = false;
    
    public void RunExample()
    {
        int count = 0;

        Task incrementTask = new Task(() =>
        {
            Thread.Sleep(50);
            
            for (int i = 0; i < 100000; i++)
            {
                count++;

                if (i > 50000 && !_isHalfCountdownPassed)
                    _isHalfCountdownPassed = true;
            }
        });
        
        Task valueWaiter = new Task(() =>
        {
            while (!_isHalfCountdownPassed)
            {
                Thread.SpinWait(100);
            }
            
            Console.WriteLine("Половину отсчёта уже прошли!");
        });
        
        incrementTask.Start();
        valueWaiter.Start();
    }
}

// 5. SemaphoreSlim
public class LockExample
{
    
    // SemaphoreSlim - оптимизированная версия Semaphore для работы в контексте одного процесса.
    private readonly SemaphoreSlim _railwaySemaphore = new SemaphoreSlim(2, 2);

    public void RunExample()
    {
        int trainCount = 10;

        Task[] trains = new Task[trainCount];

        for (int i = 0; i < trainCount; i++)
        {
            int trainNumber = i + 1;
            
            trains[i] = new Task( async void () =>
            {
                await _railwaySemaphore.WaitAsync();

                try
                {
                    Console.WriteLine($"Поезд {trainNumber} занял пути.");
                    Thread.Sleep(1000);
                    Console.WriteLine($"Поезд {trainNumber} освободил пути.");
                }
                finally
                {
                    _railwaySemaphore.Release();
                }
            });
        }
        
        foreach (Task train in trains)
        {
            train.Start();
        }
    }
}
    