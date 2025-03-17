using System.Threading;

namespace Bank;
public class Account(in string number) {
  public string Number { get; init; } = number.ToUpper();
  protected decimal _balance = 0.0m;
  protected Lock _lock = new();
  
  public override string ToString() => $"Account Number: {Number}\nBalance: {_balance}:";
  /// <summary>
  /// This overload takes in an account number.
  /// </summary>
  /// <param name="number"></param>
  /// <returns>true if <paramref name="number"/> is equal
  /// to "P:Bank.Account.Number" of this instance.</returns>
  public bool Equals(string number)
    => Number == number.ToUpper();
  
  public override bool Equals(object? obj)
    => obj is Account a && a.Equals(Number);
  public override int GetHashCode()
    => Number.GetHashCode();
  public override string ToString() 
    => $"Account Number: {Number}\nBalance: {_balance}:";
  
  public static bool operator ==(in Account self, object? o) => o switch {
    string s => self.Equals(s),
    _ => self.Equals(o),
  };
  public static bool operator !=(in Account self, object? o) => !(self == o);
}