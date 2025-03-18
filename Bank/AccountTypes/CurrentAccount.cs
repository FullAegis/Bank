using System;
using System.Threading.Tasks;
using Bank.Users;

namespace Bank.AccountTypes;
public class CurrentAccount : Account {
  public CurrentAccount(in string number, Person owner, in decimal balance = 0m)
    : base(number, balance, owner)
  {}
  public CurrentAccount(in string number, in decimal balance = 0m)
    :this(number, Person.None, balance)
  {}

  private readonly object _lock = new();
  public override decimal Deposit(in decimal amount) {
    // I can't have a `const long sum = decimal.ToOACurrency(amount)`. Local function it is.
    // ToOACurrency(x) -> (long)(x * 10_000)
    static long read(in decimal x) => decimal.ToOACurrency(x);
    static decimal write(long x) => decimal.FromOACurrency(x);
    var sum = read(amount);
    if (sum < 0)
      throw new ArgumentOutOfRangeException("amount", "Cannot deposit negative amount.");
      
    long newBalance;
    lock (_lock) {
      newBalance = read(Balance);  // Read
      newBalance += sum;           // Modify
      Balance = write(newBalance); // Write
    }
    return newBalance;
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