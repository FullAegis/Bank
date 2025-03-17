using Bank.Users; // For Person

namespace Bank.AccountTypes;
public abstract class Account {
  public string Number { get; protected init; }
  public decimal Balance { get; protected set; }
  public User Owner { get; protected set; }
  
  protected Account(in string number, decimal balance, User owner) {
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
}