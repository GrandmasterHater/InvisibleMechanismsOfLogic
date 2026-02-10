using InvisibleMechanismsOfLogic.Task1_SaveStringsToDB;

namespace InvisibleMechanismsOfLogic.Task11_UseConfigs;

public class ConfigExample
{
    // Улучшаем пример из первого урока по работе с БД:
    /* public class MainTask
    *  {
    *      public static void RunMain(string[] args)
    *      {
    *          var connectionString = "Server=localhost;Database=testdb;UserId=admin;Password=1234;";
    *          
    *          Storage storage = new DatabaseStorage(connectionString);
    *          
    *          storage.Save("Test data 1");
    *          storage.Save("Test data 2");
    *          
    *          Console.WriteLine($"Received data for id={1}: {storage.Retrieve(1)}");
    *          Console.WriteLine($"Received data for id={2}: {storage.Retrieve(2)}");
    *      } 
    *  }
    */

    public void RunExample(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Получаем данные из конфига 
        string connectionString = builder.Configuration.GetConnectionString("PostgresConnection");

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            Console.WriteLine("No connection string found.");
            return;
        }

        // Выполняем бинд конфигурации и класса работы с БД
        builder.Services.AddScoped<Storage>(provider => 
            new DatabaseStorage(connectionString)
        );

        var app = builder.Build();

        // Определяем эндпоинты для тестовой работы с БД.
        app.MapPost("/api/data", (string data, IStorage storage) =>
        {
            storage.Save(data);
            return Results.Ok("Data saved successfully");
        });

        app.MapGet("/api/data/{id}", (int id, IStorage storage) =>
        {
            string data = storage.Retrieve(id);
    
            if (string.IsNullOrEmpty(data))
            {
                return Results.NotFound();
            }
    
            return Results.Ok(data);
        });

        app.Run();
        
        // Возможна реализация вызова из кода либо через запрос.
    }
}