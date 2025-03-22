using System; // DateTime
using Bank.AccountTypes;

namespace Bank.Users;
public sealed class Person(string firstname, string lastname, DateTime birthday) : User {
  public string FirstName { get; set; } = firstname;
  public string LastName { get; set; } = lastname;
  public DateTime Birthday { get; init; } = birthday;

  public static readonly Person None = new("NOT A PERSON", "N/A", DateTime.UnixEpoch);
  public Person(string firstname, string lastname, string birthday)
    : this(firstname, lastname, DateTime.Parse(birthday, Currency.Locale))
  {}
  public override string ToString() => $"{LastName.ToUpper()} {FirstName} ({Birthday.Date:d})"; 
}