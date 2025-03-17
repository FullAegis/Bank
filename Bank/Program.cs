using System;
using static System.Console;

namespace Bank;
public static class Program {
  public static void Main(string[] args) {
    var iban = "BE02953715721640";
    var bank = new Bank("OXO");
    bank.Add(new CurrentAccount(iban));
    WriteLine(bank[iban]);
    var nonAccount = bank["nope"];
    WriteLine(nonAccount?.ToString() ?? "nope");
  }
}