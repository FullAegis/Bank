using System;
using System.Threading;
using Bank.Users;

namespace Bank.AccountTypes;
public class CurrentAccount : Account {
  public CurrentAccount(in string number, Person owner, in decimal balance = 0m)
    : base(number, balance, owner)
  {}
  public CurrentAccount(in string number, in decimal balance = 0m)
    :this(number, Person.None, balance)
  {}

  private static class Money {
    public static readonly Lock Lock = new();
    public static long Read(in decimal x) => decimal.ToOACurrency(x);
    public static decimal Write(in long x) => decimal.FromOACurrency(x);
  }
  
  public override decimal Deposit(in decimal amount) {
    var sum = Money.Read(amount);
    if (sum < 0)
      throw new ArgumentOutOfRangeException("amount", "Cannot deposit negative amount.");
      
    long bal;
    lock (Money.Lock) {
      bal = Money.Read(Balance);  // Read
      bal = checked (sum + bal);  // Modify
      Balance = Money.Write(bal); // Write
    }
    return bal;
  }

  public override decimal Withdraw(in decimal amount) {
    var sum = Money.Read(amount);
    if (sum < 0)
      throw new ArgumentOutOfRangeException("amount", "Cannot withdraw negative amount.");
    
    long bal;
    lock (Money.Lock) {
      bal = Money.Read(Balance);
      if (bal < sum)
        throw new OperationCanceledException("Not enough money in balance.");
      bal = checked (bal - sum);
      Balance = Money.Write(bal);
    }
    return bal;
  }

  public override string ToString() => $"""
    Current Account Number: {Number}
    Account Holder: {Owner.ToString()}
    Balance: {Balance.ToString("C", System.Globalization.CultureInfo.CurrentCulture)}
    Owner ID: {Owner.id}
  """;
  public override bool Equals(in string accountNumber)
    => accountNumber == Number;
  public static bool operator ==(in CurrentAccount self, in string number)
    => self.Equals(number);
  public static bool operator !=(in CurrentAccount self, in string number)
    => !(self == number);
}