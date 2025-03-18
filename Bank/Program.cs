using System;
using static System.Console;
using Bank.AccountTypes;

namespace Bank;
public static class Program {
  public static void Main(string[] args) {
    var iban = "BE02953715721640";
    var bank = new Bank("OXO");
    bank.Add(new CurrentAccount(iban));
    var account = bank[number: iban];
    account.Deposit(1e6m);
    WriteLine(account);
    
    var nonAccount = bank[number: "nope"];
    WriteLine(nonAccount?.ToString() ?? "nope");
  }
}