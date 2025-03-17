using System.Threading;

namespace Bank;
public class Account(in string number) {
  public string Number { get; init; } = number.ToUpper();
  protected decimal _balance = 0.0m;
  protected Lock _lock = new();
  
  public override string ToString() => $"Account Number: {Number}\nBalance: {_balance}:";
  public override bool Equals(object? obj) => obj is Account a && Number == a.Number;
  public static bool operator ==(in Account self, object? o) => o switch {
    string s => self.Number == s,
    _ => self.Equals(o),
  };
  public static bool operator !=(in Account self, object? o) => !(self == o);
}