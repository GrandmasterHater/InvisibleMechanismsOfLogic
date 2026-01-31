namespace InvisibleMechanismsOfLogic.Task1_SaveStringsToDB;

public class MainTask
{
    public static void RunMain(string[] args)
    {
        var connectionString = "Server=localhost;Database=testdb;UserId=admin;Password=1234;";
        
        Storage storage = new DatabaseStorage(connectionString);
        
        storage.Save("Test data 1");
        storage.Save("Test data 2");
        
        Console.WriteLine($"Received data for id={1}: {storage.Retrieve(1)}");
        Console.WriteLine($"Received data for id={2}: {storage.Retrieve(2)}");
    } 
}