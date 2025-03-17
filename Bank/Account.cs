namespace Bank;

public class Account(in string number) {
  public string Number { get; init; } = number;
  private decimal _balance = 0.0m;
  
  public override string ToString()
    => $"Account Number: {Number}\nBalance: {_balance}:";
  
  public static bool Equals(in Account account1, in Account account2) {
    
  }

}