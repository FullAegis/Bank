using Bank.Users;
using NUnit.Framework;
using System;

namespace BankTests;

[TestFixture] public class PersonTests {
  [Test] public void Constructor_ValidParameters_CreatesPerson() {
    var firstName = "Alice";
    var lastName = "Smith";
    var birthday = new DateTime(1985, 5, 10);
    // Act
    var person = new Person(firstName, lastName, birthday);
    // Assert
    Assert.That(person.FirstName, Is.EqualTo(firstName));
    Assert.That(person.LastName, Is.EqualTo(lastName));
    Assert.That(person.Birthday, Is.EqualTo(birthday));
  }

  [Test] public void ToString_ReturnsFormattedString() {
    // Arrange
    var firstName = "Bob";
    var lastName = "Johnson";
    var birthday = new DateTime(1992, 8, 20);
    var person = new Person(firstName, lastName, birthday);
    // Act
    var personString = person.ToString();
    // Assert
    Assert.That(personString, Is.EqualTo("JOHNSON Bob (20/08/1992)"));
  }

  [Test] public void None_ReturnsDefaultPerson() {
    // Act
    var nonePerson = Person.None;
    // Assert
    Assert.That(nonePerson.FirstName, Is.EqualTo("NOT A PERSON"));
    Assert.That(nonePerson.LastName, Is.EqualTo("N/A"));
    Assert.That(nonePerson.Birthday, Is.EqualTo(DateTime.UnixEpoch));
  }
}