using System;
using System.Threading;
using Bank.Users;

namespace Bank.AccountTypes;
public class CurrentAccount : Account {
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
  public override decimal Deposit(in decimal amount) {
    long sum = Currency.PositiveOnly(amount);
    long bal;
    lock (_lock) {
      bal = Balance;  // Read
      bal = checked (sum + bal);  // Modify
      Balance = bal; // Write
    }
    return (Currency) bal;
  }

  public override decimal Withdraw(in decimal amount) {
    long sum = Currency.PositiveOnly(amount);
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
    return (Currency) bal;
  }

  public override string ToString() => $"""
    Owner ID: {Owner.id}
    Account Holder: {Owner.ToString()}
    Account Number: {Number}
    Balance: {Balance.ToString()}
    Credit Limit: {CreditLimit.ToString()}
    """;

  public override bool Equals(object? obj) => Account.Equals(obj, Number);
}