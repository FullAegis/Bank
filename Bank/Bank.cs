using System;
using System.Linq;
using System.Collections.Generic;
using Bank.AccountTypes;

namespace Bank;
public class Bank(in string name) {
  public string Name { get; init; } = name;
  private readonly HashSet<CurrentAccount> _accounts = new();
  
  public CurrentAccount this[string number] => _accounts.FirstOrDefault(a => a.Equals(number));
  public void Add(in CurrentAccount account) => _accounts.Add(account);
  public void Remove(in string account) => _accounts.Remove(this[account]);
}

