namespace Bank.AccountTypes;
public interface ICustomer {
  public abstract Currency Balance { get; set; }
  public abstract decimal Withdraw(in decimal amount);
  public abstract decimal Deposit(in decimal amount);
}