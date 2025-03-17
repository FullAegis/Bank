namespace Bank.AccountTypes;
public abstract class Account(in string number) {
  public string Number { get; init; } = number.ToUpper();
  
  /// <summary>
  /// This overload takes in an account number.
  /// </summary>
  /// <param name="accountNumber"></param>
  /// <returns>true if <paramref name="accountNumber"/> is equal
  /// to "P:Bank.Account.Number" of this instance.</returns>
  public abstract bool Equals(in string accountNumber);
}