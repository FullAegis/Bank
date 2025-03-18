namespace Bank.Users;
using System; // DateTime

public sealed class Person(string firstname, string lastname, DateTime birthday) : User {
  public string FirstName { get; set; } = firstname;
  public string LastName  { get; set; } = lastname;
  public DateTime Birthday { get;  private init; } = birthday;

  private Person() : this("NOT A PERSON", "N/A", DateTime.UnixEpoch) {
  }
  public static readonly Person None = new();

  public override string ToString()
    => $"{LastName.ToUpper()} {FirstName} ({Birthday.Date:s})"; 
}