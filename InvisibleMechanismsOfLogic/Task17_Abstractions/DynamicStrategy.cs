namespace InvisibleMechanismsOfLogic.Task17_Abstractions;

#region Абстракция

// Абстракция описывает стратегию, которая меняет своё поведение в зависимости от привязанного контекста, где работает стратегия.
public abstract class DynamicStrategy<TContext> where TContext : class, IExecutionContext
{
    private TContext? _context;
    private int _lastExecutionVersion;
    
    public void Prepare(TContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _lastExecutionVersion = context.ContextVersion - 1;

        try
        {
            OnPrepare(_context);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    public void Execute()
    {
        if (_context is null)
            return;
        
        try
        {
            if (_context.ContextVersion > _lastExecutionVersion)
            {
                OnExecute(_context);
                _lastExecutionVersion = _context.ContextVersion;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    public void Clear()
    {
        try
        {
            OnClear();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        finally
        {
            _context = null;
        }
    }
    
    protected virtual void OnPrepare(TContext context) { }
    
    protected virtual void OnExecute(TContext context) { }

    protected virtual void OnClear() { }
}

public interface IExecutionContext
{
    int ContextVersion { get; }
}

#endregion


// Для примера взял управление автомобилем в зависимости от окружения и усталости водителя. Однако динамическая стратегия
// применима и для моделирования социального общения, планировании дня в зависимости от того какой именно день (праздники,
// будни, выходные). В целом любая среда, где поведение одного и того же субъекта может меняться в зависимости от окружения,
// но внимание нужно сохранять именно на субъекте.
#region Пример: управление авто в различных средах.

public interface IDriveContext<out TCar> : IExecutionContext where TCar : Car
{
    string Weather { get; }
    
    string RoadSurfaceType { get; }
    
    string DriverPhysicalCondition { get; }
    
    TCar Car { get; }

    void ChangeWeather(string weather);
    
    void ChangeRoadSurfaceType(string roadSurfaceType);
    
    void ChangeDriverPhysicalCondition(string driverPhysicalCondition);
}

public abstract class Car
{
    public abstract string EngineType { get; }
    
    public abstract string DriveType { get; }
    
    public abstract string WheelType { get; }
    
    public void SetSteeringWheelAngle(double angle)
    {
        // Обработка поворота рулевого колеса.
    }

    public void SetGas(double value)
    {
        // Обработка нажатия педали газа.
    }
}

public abstract class OffRoadCar : Car
{
    public abstract string WinchType { get; }
}

public class UazHunter : OffRoadCar
{
    public override string EngineType { get; } = "ЗМЗ-409";
    public override string DriveType { get; } = "4X4";
    public override string WheelType { get; } = "Offroad";
    public override string WinchType { get; } = "MiddlePower";
}

public class ToyotaLC : OffRoadCar
{
    public override string EngineType { get; } = "1UR-FE";
    public override string DriveType { get; } = "AWD";
    public override string WheelType { get; } = "OffRoad";
    public override string WinchType { get; } = "LargePower";
}

public class OffRoadDriveContext<TOffRoadCar> : IDriveContext<TOffRoadCar> where TOffRoadCar : OffRoadCar
{
    public string Weather { get; private set; }
    public string RoadSurfaceType { get; private set; }
    public string DriverPhysicalCondition { get; private set; }
    public TOffRoadCar Car { get; private set; }
    
    public int ContextVersion { get; private set; }
    
    public OffRoadDriveContext(string weather, string roadSurfaceType, string driverPhysicalCondition, TOffRoadCar car)
    {
        Weather = weather;
        RoadSurfaceType = roadSurfaceType;
        DriverPhysicalCondition = driverPhysicalCondition;
        Car = car;

        ContextVersion = 0;
    }
    
    public void ChangeWeather(string weather)
    {
        Weather = weather;
        ContextVersion++;
    }

    public void ChangeRoadSurfaceType(string roadSurfaceType)
    {
        RoadSurfaceType = roadSurfaceType;
        ContextVersion++;
    }

    public void ChangeDriverPhysicalCondition(string driverPhysicalCondition)
    {
        DriverPhysicalCondition = driverPhysicalCondition;
        ContextVersion++;
    }
}

public class BeginnerDriverBehaviour : DynamicStrategy<IDriveContext<OffRoadCar>>
{

    protected override void OnPrepare(IDriveContext<OffRoadCar> context)
    {
        // Инициализация поведения для работы в указанном контексте.
    }

    protected override void OnExecute(IDriveContext<OffRoadCar> context)
    {
        // Реализация управления авто в заданном контексте для начинающего водителя.
    }
    
    protected override void OnClear()
    {
        // Очистка поведения от контекста.
    }
}

public class ExpertDriverBehaviour : DynamicStrategy<IDriveContext<OffRoadCar>>
{
    protected override void OnPrepare(IDriveContext<OffRoadCar> context)
    {
        // Инициализация поведения для работы в указанном контексте.
    }

    protected override void OnExecute(IDriveContext<OffRoadCar> context)
    {
        // Реализация управления авто в заданном контексте для опытного водителя.
    }
    
    protected override void OnClear()
    {
        // Очистка поведения от контекста.
    }
}

#endregion

public class Runner
{
    public void Run()
    {
        UazHunter uaz = new();
        ToyotaLC toyotaLc = new();
        string weather = "Sunny";
        string roadSurfaceType = "HardGround";
        string driverPhysicalCondition = "Cheerful";

        IDriveContext<OffRoadCar>[] trialContexts = new IDriveContext<OffRoadCar>[2];
        trialContexts[0] = new OffRoadDriveContext<UazHunter>(weather, roadSurfaceType, driverPhysicalCondition: driverPhysicalCondition, uaz);
        trialContexts[1] = new OffRoadDriveContext<ToyotaLC>(weather, roadSurfaceType, driverPhysicalCondition, toyotaLc);
        
        DynamicStrategy<IDriveContext<OffRoadCar>> beginnerStrategy = new BeginnerDriverBehaviour();
        beginnerStrategy.Prepare(trialContexts[0]);
        DynamicStrategy<IDriveContext<OffRoadCar>> expertStrategy = new ExpertDriverBehaviour();
        expertStrategy.Prepare(trialContexts[1]);
        DynamicStrategy<IDriveContext<OffRoadCar>>[] drivers = {beginnerStrategy, expertStrategy};
        
        // Начинаем триал!
        foreach (DynamicStrategy<IDriveContext<OffRoadCar>> driver in drivers)
        {
            driver.Execute();
        }

        // Погода на маршруте сменилась!
        foreach (IDriveContext<OffRoadCar> trialContext in trialContexts)
        {
            trialContext.ChangeWeather("Rain");
            trialContext.ChangeRoadSurfaceType("Dirt");
        }
        
        //Корректируем поведение по новым условиям.
        foreach (DynamicStrategy<IDriveContext<OffRoadCar>> driver in drivers)
        {
            driver.Execute();
        }
        
        // Остановка.
        foreach (DynamicStrategy<IDriveContext<OffRoadCar>> driver in drivers)
        {
            driver.Clear();
        }
    }
}