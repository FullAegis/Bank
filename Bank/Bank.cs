using System;
using System.Collections.Generic;
using System.Linq;

namespace Bank;
public class Bank(in string name) {
  public string Name { get; init; } = name;
  private HashSet<CurrentAccount> _currentAccounts = new();
  
  public CurrentAccount this[string number] {
    get => _currentAccounts.First(a => a == number);
  }
  
  public void Add(in CurrentAccount account) {
    _currentAccounts.Add(account);
  }
}

