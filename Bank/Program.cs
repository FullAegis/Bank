using System;
using static System.Console;
using Bank.AccountTypes;
using Bank.Users;

namespace Bank;
public static class Program {
  public static void Main(string[] args) {
    var iban = "BE02953715721640";
    var bank = new Bank("OXO");
    
    var nate = new Person("Nathan", "Whitehat", DateTime.Parse("1995-11-30"));
    bank.Add(new CurrentAccount(number: iban, owner: nate, balance: 0m, creditLimit: 5000)); 
      
    var account = bank[number: iban];
    account.Withdraw(1773.69m);
    WriteLine(account);
    
    var nonAccount = bank[number: "nope"];
    WriteLine(nonAccount?.ToString() ?? "nope");
  }
}