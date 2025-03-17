using System;
using Bank.Users;

namespace Bank.AccountTypes;
public class CurrentAccount : Account {
  public CurrentAccount(in string number) : base(number, 0m, Person.None) {}
  
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