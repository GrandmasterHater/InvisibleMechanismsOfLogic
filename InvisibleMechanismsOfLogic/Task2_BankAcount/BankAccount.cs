namespace InvisibleMechanismsOfLogic.Task2_BankAcount;

public class BankAccount
{
    private readonly string AMOUNT_LESS_THAN_ZERO_MESSAGE = "Balance cannot be negative";
    private readonly string INSUFFICIENT_FUNDS_MESSAGE = "Balance cannot be negative";
    private readonly string OVERFLOW_FUNDS_MESSAGE = "The account balance has been exceeded";
    private double _balance;

    public BankAccount(double balance)
    {
        AssertAmountGreaterThanZero(balance, AMOUNT_LESS_THAN_ZERO_MESSAGE);
        
        _balance = balance;
    }


    public void Deposit(double amount)
    {
        AssertAmountGreaterThanZero(amount, AMOUNT_LESS_THAN_ZERO_MESSAGE);

        double newBalance;

        try
        {
            checked
            {
                newBalance = _balance + amount;
            }
        }
        catch
        {
            throw new InvalidOperationException(OVERFLOW_FUNDS_MESSAGE);
        }
        
        _balance = newBalance;
    }


    public void Withdraw(double amount)
    {
        AssertAmountGreaterThanZero(amount, AMOUNT_LESS_THAN_ZERO_MESSAGE);
        
        double newBalance = _balance - amount;
        
        AssertAmountGreaterThanZero(newBalance, INSUFFICIENT_FUNDS_MESSAGE);
        
        _balance = newBalance;
    }
    
    
    public double GetBalance() => _balance;

    private void AssertAmountGreaterThanZero(double amount, string message)
    {
        if (amount < 0.0d) 
            throw new InvalidOperationException(message);
    }
}