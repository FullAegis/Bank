using Bank.Users; // For Person

namespace Bank.AccountTypes;
public abstract class Account {
  public string Number { get; protected init; }
  public decimal Balance { get; protected set; }
  public Person Owner { get; protected set; }
  
  protected Account(in string number, decimal balance, Person owner) {
    Number = number;
    Balance = balance;
    Owner = owner;
  }
  
  /// <summary>
  /// This overload takes in an account number.
  /// </summary>
  /// <param name="accountNumber"></param>
  /// <returns>true if <paramref name="accountNumber"/> is equal
  /// to "P:Bank.Account.Number" of this instance.</returns>
  public abstract bool Equals(in string accountNumber);
  public abstract decimal Deposit(in decimal amount);
  public abstract decimal Withdraw(in decimal amount);
}