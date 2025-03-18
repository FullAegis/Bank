using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;


namespace Bank.AccountTypes;
public readonly record struct Currency {
  static readonly CultureInfo Locale = CultureInfo.GetCultureInfo("fr-BE", true);
  readonly long _value;
  public decimal Value {
    get => decimal.FromOACurrency(_value);
    private init => _value = decimal.ToOACurrency(value);
  }

  public static Currency PositiveOnly(Currency self) {
    if (self._value < 0)
      throw new ArgumentOutOfRangeException(nameof(self), "Value must be positive");
    return self;
  }
  
  public static implicit operator Currency(decimal val) => new() { Value = val };
  public static implicit operator decimal(Currency val) => val.Value;
  
  public static implicit operator Currency(long val) => (Currency) decimal.FromOACurrency(val);
  public static implicit operator long(Currency val) => val._value;

  public override string ToString() => $"{Value.ToString("C", Locale)}";
}