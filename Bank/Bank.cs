using System;
using System.Collections.Generic;
using System.Linq;
using Bank.AccountTypes;

namespace Bank;
public class Bank(in string name) {
  public string Name { get; init; } = name;
  private readonly HashSet<CurrentAccount> _currentAccounts = new();
  
  public CurrentAccount? this[string number] 
    => _currentAccounts.FirstOrDefault(a => a == number);

  public void Add(in CurrentAccount account) {
    _currentAccounts.Add(account);
  }
  
  public void RemoveCurrentAccount(in string number) {
    _currentAccounts.Remove(this[number]);
  }
}

