using NUnit.Framework;
using Bank.AccountTypes;
using Bank.AccountTypes.Exceptions;

namespace Bank.AccountTypes.Tests;
[TestFixture]
public class InternationalBankAccountNumberTests {
    [Test]
    public void Constructor_ValidIban_CreatesInstance() {
        // Arrange
        var validIban = "GB82WEST12345698765432";

        // Act
        var iban = new InternationalBankAccountNumber(validIban);

        // Assert: No exception should be thrown.  We don't need a specific assertion
        //       if the constructor succeeds. The act of not throwing is the success.
        Assert.That(() => new InternationalBankAccountNumber(validIban), Throws.Nothing);
    }

    [Test]
    [TestCase("GB82WEST12345698765432")] // Valid
    [TestCase("DE91100000000123456789")] // Valid
    [TestCase("FR1420041010050500013M02606")] // Valid
    public void Constructor_MultipleValidIbans_CreatesInstances(string validIban) {
        Assert.That(() => new InternationalBankAccountNumber(validIban), Throws.Nothing);
    }

    [Test]
    public void Constructor_IbanTooShort_ThrowsInvalidIbanException() {
        // Arrange
        var shortIban = "GB82WEST1234";

        // Act & Assert
        var ex = Assert.Throws<InvalidIban>(() => new InternationalBankAccountNumber(shortIban));
        Assert.That(ex, Is.Not.Null);
        Assert.That(ex.ParamName, Is.EqualTo("iban"));
        Assert.That(ex.Message, Does.StartWith("Invalid Iban string:"));
    }

    [Test]
    public void Constructor_IbanTooLong_ThrowsInvalidIbanException() {
        // Arrange
        var longIban = "GB82WEST12345698765432123456789012345"; // Too long

        // Act & Assert
        var ex = Assert.Throws<InvalidIban>(() => new InternationalBankAccountNumber(longIban));
        Assert.That(ex, Is.Not.Null);
        Assert.That(ex.ParamName, Is.EqualTo("iban"));
        Assert.That(ex.Message, Does.StartWith("Invalid Iban string:"));
    }

    [Test]
    public void Constructor_InvalidCharacters_ThrowsInvalidIbanException() {
        var validWhitespace = "GB82 WEST 1234 5698 7654 32";
        Assert.That(() => new InternationalBankAccountNumber(validWhitespace), Throws.Nothing);
        var invalidChecksum = "GB82WEST1234569876543!";
        Assert.Throws<InvalidIban>(() => new InternationalBankAccountNumber(invalidChecksum));
    }

    [Test]
    public void Constructor_InvalidChecksum_ThrowsInvalidIbanException() {
        // Arrange
        var invalidIban = "GB82WEST12345698765430"; // Changed last digit

        // Act & Assert
        var ex = Assert.Throws<InvalidIban>(() => new InternationalBankAccountNumber(invalidIban));
        Assert.That(ex, Is.Not.Null);
        Assert.That(ex.ParamName, Is.EqualTo("iban"));
        Assert.That(ex.Message, Does.StartWith("Invalid check digits for iban:"));
    }

    [Test]
    public void Constructor_EmptyIban_ThrowsInvalidIbanException() {
        // Arrange
        var emptyIban = "";

        // Act & Assert
        var ex = Assert.Throws<InvalidIban>(() => new InternationalBankAccountNumber(emptyIban));
        Assert.That(ex, Is.Not.Null);
        Assert.That(ex.ParamName, Is.EqualTo("iban"));
        Assert.That(ex.Message, Does.StartWith("Invalid Iban string:"));
    }

    [Test]
    public void Constructor_WhitespaceIban_ThrowsInvalidIbanException() {
        // Arrange
        var whitespaceIban = "   ";

        // Act & Assert
        var ex = Assert.Throws<InvalidIban>(() => new InternationalBankAccountNumber(whitespaceIban));
        Assert.That(ex, Is.Not.Null);
        Assert.That(ex.ParamName, Is.EqualTo("iban"));
        Assert.That(ex.Message, Does.StartWith("Invalid Iban string:"));

    }
}