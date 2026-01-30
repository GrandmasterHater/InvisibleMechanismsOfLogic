namespace InvisibleMechanismsOfLogic.Task2_BankAcount;

public class BankAccount
{
    private double _balance;

    public BankAccount(double balance) => _balance = balance;
    
    
    public void Deposit(double amount) => _balance += amount;
    
    
    public void Withdraw(double amount) => _balance -= amount;
    
    
    public double GetBalance() => _balance;
}