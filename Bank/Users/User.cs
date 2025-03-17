using System;
using System.Threading;

namespace Bank.Users;
public abstract class User {
  private static long ID = long.MinValue;
  public readonly long id;
  protected User() {
    id = Interlocked.Increment(ref ID);
    if (id == long.MaxValue) {
      Console.Error.WriteLine("[WARNING] in User(): Can't believe you had enough RAM.");
    } 
  }
}