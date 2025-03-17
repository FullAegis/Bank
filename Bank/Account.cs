namespace Bank;

public class Account(in string number) {
  public string Number { get; init; } = number;
  protected decimal _balance = 0.0m;
  protected Lock _lock = new();
  
  public static bool Equals(in Account account1, in Account account2) {
    
  }

}