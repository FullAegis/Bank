using System;     // For IEquatable
using Bank.Users; // For Person

namespace Bank.AccountTypes;
public abstract class Account(in string number, decimal balance, Person owner)
  : IEquatable<string>, IEquatable<Account>
{
  public string Number { get; protected init; } = number;
  public Person Owner { get; protected set; } = owner;
  
  public abstract override bool Equals(object? obj);
  public abstract bool Equals(Account? other);
  public abstract bool Equals(string? other);
  public abstract override int GetHashCode();
}