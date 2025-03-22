using Bank.AccountTypes;
using Bank.Users;
using NUnit.Framework;
using System;
using System.Runtime.InteropServices;

namespace BankTests;
// Tests using the Arrange, Act, Assert methodology.
[TestFixture] public class CurrentAccountTests {
  private CurrentAccount _account;
  private string _accountNumber;
  private Person _owner;

  [SetUp]
  public void Setup() {
    // Initialize a new CurrentAccount object before each test
    _accountNumber = "1234567890";
    _owner = new("John", "Doe", "13 Septembre 1977");  // Provide a birthday
    _account = new(_accountNumber, _owner);
  }

  [Test]
  public void Constructor_ValidAccountNumberAndOwner_CreatesAccountWithZeroBalance() {
    Assert.That(_account.Balance == 0, Is.True);
    Assert.That(_account.Number, Is.EqualTo(_accountNumber));
    Assert.That(_account.Owner, Is.EqualTo(_owner));
  }

  [Test]
  public void Deposit_ValidAmount_IncreasesBalance() {
    decimal depositAmount = 100;
    
    _account.Deposit(depositAmount);

    Assert.That(_account.Balance.Value, Is.EqualTo(depositAmount));
  }

  [Test]
  public void Deposit_InvalidAmount_ThrowsArgumentOutOfRangeException() {
    decimal depositAmount = -100;

    Assert.That(() => _account.Deposit(depositAmount),
                Throws.TypeOf<ArgumentOutOfRangeException>());
  }

  [Test]
  public void Deposit_ZeroAmount_DoesNotChangeBalance() {
    var initialBalance = _account.Balance;
    var depositAmount = 0;
    _account.Deposit(depositAmount);
    Assert.That(_account.Balance, Is.EqualTo(initialBalance));
  }

  [Test]
  public void Withdraw_ValidAmount_ReturnsNewBalance() {
    var withdrawalAmount = _account.Balance;
    Assert.That(() => _account.Withdraw(withdrawalAmount), Is.EqualTo(0m));
  }

  [Test]
  public void Withdraw_InvalidAmount_ThrowsArgumentOutOfRangeException() {
    var expected = _account.Balance;
    var amount = -100m;
    Assert.That(() => _account.Withdraw(amount), Throws.TypeOf<ArgumentOutOfRangeException>());
    Assert.That(_account.Balance, Is.EqualTo(expected));
  }

  [Test]
  public void Withdraw_ValidAmountOverBalance_DoesntThrow() {
    var expected = _account.Balance;
    var amount = expected.Value + _account.CreditLimit;
    Assert.That(() => _account.Withdraw(amount), Is.EqualTo(expected - amount));
  }

  [Test]
  public void ToString_ReturnsFormattedString() {
    _account.Deposit(1000);

    var accountString = _account.ToString();

    Assert.That(accountString, Does.Contain("Owner ID:"));
    Assert.That(accountString, Does.Contain("Account Holder: DOE John (13/09/1977)"));
    Assert.That(accountString, Does.Contain("Account Number: 1234567890"));
    Assert.That(accountString, Does.Contain("Balance: "));
    Assert.That(accountString, Does.Contain("Credit Limit: "));
  }

  [Test]
  public void Equals_SameAccountNumber_ReturnsTrue() {
    var sameAccountNumber = "1234567890";
    bool areEqual = _account.Equals(sameAccountNumber);
    Assert.That(areEqual, Is.True);
  }

  [Test]
  public void Equals_DifferentAccountNumber_ReturnsFalse() {
    var differentAccountNumber = "0987654321";
    var areEqual = _account.Equals(differentAccountNumber);
    Assert.That(areEqual, Is.False);
  }

  [Test]
  public void EqualityOperator_SameAccountNumber_ReturnsTrue() {
    var sameAccountNumber = "1234567890";
    var areEqual = _account == sameAccountNumber;
    Assert.That(areEqual, Is.True);
  }

  [Test]
  public void InequalityOperator_DifferentAccountNumber_ReturnsTrue() {
    var differentAccountNumber = "0987654321";
    var areNotEqual = _account != differentAccountNumber;
    Assert.That(areNotEqual, Is.True);
  }
}