namespace InvisibleMechanismsOfLogic.Task2_BankAcount;

public class TestBankAccount
{
    public static void Main(string[] args)
    {
        BankAccount account = new BankAccount(1000);
        account.Deposit(500);
        account.Withdraw(200);
        
        Console.WriteLine($"Balance: {account.GetBalance()}");
    } 
}