using System;
using System.Threading;
using Bank.Users;

namespace Bank.AccountTypes;
public class CurrentAccount : Account, ICustomer {
  public Currency Balance { get; set; }

  public Currency CreditLimit { get; protected set; }
  public CurrentAccount(in string number, Person owner, in decimal balance, in decimal creditLimit)
    : base(number, balance, owner)
  {
    CreditLimit = creditLimit;
  }
  public CurrentAccount(in string number, in decimal balance, in decimal creditLimit)
    : this(number, Person.None, balance, creditLimit)
  {}
  public CurrentAccount(in string number, Person owner, in decimal balance = 0) 
    : this(number, owner, balance, 0)
  {}

  private readonly Lock _lock = new();
  public decimal Deposit(in decimal amount) {
    var sum = (long) Currency.EnsurePositive(amount);
    var bal = Currency.EnsurePositive(0);
    lock (_lock) {
      bal = Balance;  // Read
      bal = checked (sum + bal);  // Modify
      Balance = bal; // Write
    }
    return bal;
  }

  public decimal Withdraw(in decimal amount) {
    long sum = Currency.EnsurePositive(amount);
    long bal, limit;
    lock (_lock) {
      bal = Balance; // Read
      limit = CreditLimit;
      
      if (sum > checked (limit + bal)) {
        throw new ArgumentOutOfRangeException(nameof(amount), "Credit limit too low for transaction.");
      }
      
      bal = checked (bal - sum); // Modify
      Balance = bal;             // Write
    } // lock

    return (Currency)bal; 
  }

  public override string ToString() => $"""
    Owner ID: {Owner.id}
    Account Holder: {Owner.ToString()}
    Account Number: {Number}
    Balance: {Balance.ToString()}
    Credit Limit: {CreditLimit.ToString()}
    """;

  public override bool Equals(Account? other)
    => other is not null && (ReferenceEquals(this, other) || Number == other.Number);
  public override bool Equals(object? obj) => Equals((Account?) obj);
  public override bool Equals(string? other) => Number.Equals(other);
  public override int GetHashCode() => Number.GetHashCode();

  public static bool operator ==(in CurrentAccount self, in string number) => self.Equals(number);
  public static bool operator !=(in CurrentAccount self, in string number) => !self.Equals(number);
}