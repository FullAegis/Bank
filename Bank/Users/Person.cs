using System;

namespace Bank.Users;
public sealed class Person : User {
  public string FirstName { get; set; }
  public string LastName  { get; set; }
  public DateTime Birthday { get;  private init; }
  
  private Person() {
    // No call to base()
    FirstName = "NOT A PERSON";
    LastName = "N/A";
    Birthday = DateTime.UnixEpoch;
  }
  public static readonly Person None = new();
  
  public Person(string firstname, string lastname, DateTime birthday) : base() {
    FirstName = firstname;
    LastName = lastname;
    Birthday = birthday;
  }
  
  public override string ToString()
    => $"{LastName.ToUpper()} {FirstName} ({Birthday.Date.ToString("s")})"; 
}