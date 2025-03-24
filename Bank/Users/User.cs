using System;
using System.Threading;

namespace Bank;
public abstract class User {
  private static long ID = long.MinValue;
  public readonly long id = Interlocked.Increment(ref ID);
}