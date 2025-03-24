namespace Bank.AccountTypes;
public interface ICustomer {
  public Currency Balance { get; set; }
  public decimal Withdraw(in decimal amount);
  public decimal Deposit(in decimal amount);
}