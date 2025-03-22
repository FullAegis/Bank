using Bank.AccountTypes;
using Bank.Users;
using NUnit.Framework;
using System;
using System.Runtime.InteropServices;

namespace BankTests;

[TestFixture]
public class CurrentAccountTests
{
  private CurrentAccount _account;
  private string _accountNumber;
  private Person _owner;

  [SetUp]
  public void Setup()
  {
    // Initialize a new CurrentAccount object before each test
    _accountNumber = "1234567890";
    _owner = new Person("John", "Doe", "13 Septembre 1977"); // Provide a birthday
    _account = new CurrentAccount(_accountNumber, _owner);
  }

  [Test]
  public void Constructor_ValidAccountNumberAndOwner_CreatesAccountWithZeroBalance()
  {
    Assert.That(_account.Balance == 0, Is.True);
    Assert.That(_account.Number, Is.EqualTo(_accountNumber));
    Assert.That(_account.Owner, Is.EqualTo(_owner));
  }

  [Test]
  public void Deposit_ValidAmount_IncreasesBalance()
  {
    // Arrange
    decimal depositAmount = 100;

    // Act
    _account.Deposit(depositAmount);

    // Assert (Constraint Model)
    Assert.That(_account.Balance.Value, Is.EqualTo(depositAmount));
  }

  [Test]
  public void Deposit_InvalidAmount_ThrowsArgumentOutOfRangeException()
  {
    // Arrange
    decimal depositAmount = -100;

    // Act & Assert (Constraint Model for Exceptions)
    Assert.That(() => _account.Deposit(depositAmount), Throws.TypeOf<ArgumentOutOfRangeException>());
  }

  [Test]
  public void Deposit_ZeroAmount_DoesNotChangeBalance()
  {
    // Arrange
    decimal initialBalance = _account.Balance;
    decimal depositAmount = 0;

    // Act
    _account.Deposit(depositAmount);

    // Assert
    Assert.That((decimal) _account.Balance, Is.EqualTo(initialBalance));
  }

  [Test]
  public void Withdraw_ValidAmount_ReturnsNewBalance() {
    // Arrange
    var withdrawalAmount = _account.Balance;

    // Act & Assert
    Assert.That(() => _account.Withdraw(withdrawalAmount), Is.EqualTo(0));
  }  
  
  [Test]
  public void Withdraw_InvalidAmount_ThrowsArgumentOutOfRangeException() {
    // Arrange
    decimal expected = _account.Balance;
    var amount = -100m;
    // Act & Assert
    Assert.That(() => _account.Withdraw(amount), Throws.TypeOf<ArgumentOutOfRangeException>());
    Assert.That(_account.Balance.Value, Is.EqualTo(expected));
  }
  
  [Test]
  public void Withdraw_ValidAmountOverBalance_DoesntThrow() {
    // Arrange
    var expected = _account.Balance;
    var amount = expected.Value + _account.CreditLimit;
    // Act & Assert
    Assert.That(() => _account.Withdraw(amount),Is.EqualTo(expected - amount));
  }

  [Test]
  public void ToString_ReturnsFormattedString()
  {
    // Arrange
    _account.Deposit(1000);

    // Act
    string accountString = _account.ToString();

    // Assert (Using NUnit 4 String Constraints)
    Assert.That(accountString, Does.Contain("Owner ID:"));
    Assert.That(accountString, Does.Contain("Account Holder: DOE John (13/09/1977)"));
    Assert.That(accountString, Does.Contain("Account Number: 1234567890"));
    Assert.That(accountString, Does.Contain("Balance: "));
    Assert.That(accountString, Does.Contain("Credit Limit: "));
  }

  [Test]
  public void Equals_SameAccountNumber_ReturnsTrue()
  {
    // Arrange
    string sameAccountNumber = "1234567890";

    // Act
    bool areEqual = _account.Equals(sameAccountNumber);

    // Assert
    Assert.That(areEqual, Is.True);
  }

  [Test]
  public void Equals_DifferentAccountNumber_ReturnsFalse()
  {
    // Arrange
    string differentAccountNumber = "0987654321";

    // Act
    bool areEqual = _account.Equals(differentAccountNumber);

    // Assert
    Assert.That(areEqual, Is.False);
  }

  [Test]
  public void EqualityOperator_SameAccountNumber_ReturnsTrue()
  {
    // Arrange
    string sameAccountNumber = "1234567890";

    // Act
    bool areEqual = _account == sameAccountNumber;

    // Assert
    Assert.That(areEqual, Is.True);
  }

  [Test]
  public void InequalityOperator_DifferentAccountNumber_ReturnsTrue()
  {
    // Arrange
    string differentAccountNumber = "0987654321";

    // Act
    bool areNotEqual = _account != differentAccountNumber;

    // Assert
    Assert.That(areNotEqual, Is.True);
  }
}