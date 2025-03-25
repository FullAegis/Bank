namespace Bank.AccountTypes.Exceptions;
using System; // For: ArgumentException

public sealed class InvalidIban(in string? message, in string? paramName) : ArgumentException {
  public override string Message { get; } = message ?? "Invalid Iban. No message provided.";
  public override string? ParamName { get; } = paramName;
}