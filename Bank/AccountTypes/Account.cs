using Bank.Users; // For Person

namespace Bank.AccountTypes;
public abstract class Account {
  public string Number { get; protected init; }
  public Person Owner { get; protected set; }
  public Currency Balance { get; protected set; }
  
  protected Account(in string number, decimal balance, Person owner) {
    Number = number;
    Balance = balance;
    Owner = owner;
  }
  
  public abstract decimal Deposit(in decimal amount);
  public abstract decimal Withdraw(in decimal amount);
  public static bool Equals(Account acc, in string accountNumber) => acc.Number == accountNumber;
  public static bool operator ==(in Account self, in string number) => Equals(self, number);
  public static bool operator !=(in Account self, in string number) => !(self == number);
}