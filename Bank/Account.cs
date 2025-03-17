namespace Bank;

public class Account(in string number) {
  public string Number { get; init; } = number;
  protected decimal _balance = 0.0m;
  protected Lock _lock = new();
  
  public override string ToString() => $"Account Number: {Number}\nBalance: {_balance}:";
  public override bool Equals(object? obj) => obj is Account a && Number == a.Number;
}