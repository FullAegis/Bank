using Bank.Users;
using NUnit.Framework;
using System;

namespace BankTests;

[TestFixture]
public class PersonTests
{
  [Test]
  public void Constructor_ValidParameters_CreatesPerson()
  {
    // Arrange
    string firstName = "Alice";
    string lastName = "Smith";
    DateTime birthday = new DateTime(1985, 5, 10);

    // Act
    Person person = new Person(firstName, lastName, birthday);

    // Assert
    Assert.That(person.FirstName, Is.EqualTo(firstName));
    Assert.That(person.LastName, Is.EqualTo(lastName));
    Assert.That(person.Birthday, Is.EqualTo(birthday));
  }

  [Test]
  public void ToString_ReturnsFormattedString()
  {
    // Arrange
    string firstName = "Bob";
    string lastName = "Johnson";
    DateTime birthday = new DateTime(1992, 8, 20);
    Person person = new Person(firstName, lastName, birthday);

    // Act
    string personString = person.ToString();

    // Assert
    Assert.That(personString, Is.EqualTo("JOHNSON Bob (1992-08-20T00:00:00)"));
  }

  [Test]
  public void None_ReturnsDefaultPerson()
  {
    // Act
    Person nonePerson = Person.None;

    // Assert
    Assert.That(nonePerson.FirstName, Is.EqualTo("NOT A PERSON"));
    Assert.That(nonePerson.LastName, Is.EqualTo("N/A"));
    Assert.That(nonePerson.Birthday, Is.EqualTo(DateTime.UnixEpoch));
  }
}