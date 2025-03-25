namespace Bank.AccountTypes;
using System;
using System.Linq; // For: Enumerable<T>.{ Where(Func<T,bool>), Skip/Take(int), To/Array/String() }
using AccountTypes.Exceptions; // For: InvalidIban

public readonly record struct InternationalBankAccountNumber : IEquatable<string> {
  private string Init { get; init; }
  
  public InternationalBankAccountNumber(in string iban) {
    Init = new(iban.ToUpper().Where(char.IsLetterOrDigit).ToArray());
    // Check IBAN (cf: [EBS204 V3.2](https://www.ecbs.org/Download/EBS204_V3.2.pdf))
    if (Init.Length is < 15 or > 32) { // 1. Check length
      throw new InvalidIban($"Invalid Iban string: '{Init}'", nameof (iban));
    } else {
      // 2. Move the first 4 chars to the end of the string. (rotate)
      var _iban = Init.Skip(4)
                      .Concat(Init.Take(4))
                      .ToArray()
                      ;
      
      // 4. Interpret the string as a decimal integer (log((10^32)-1)/log(2) = 106.30 so Int128)
      var checksum = new Int128();
      foreach (var c in _iban) {
        // 3. Replace each letter in the string with two digits, thereby expanding the string
        if (char.IsLetter(c)) { checksum = (checksum * 100) + (c - 'A' + 10); }
        else                  { checksum = (checksum *  10) + (c - '0');      }
      }
      if (checksum % 97 != 1) { // 5. Check the remainder of that number on division by 97 is 1.
        throw new InvalidIban($"Invalid check digits for iban: {Init}", nameof (iban));
      }
    }
  }

  public bool Equals(InternationalBankAccountNumber other) => Init == other.Init;
  public bool Equals(string? iban) {
    if (!string.IsNullOrEmpty(iban)) try {
      return Equals(other: new(iban ?? ""));
    } catch (InvalidIban) {
      // Compare with invalid IBAN, not an error.
      Console.Error.WriteLine($"Comparing '{Init}' against invalid IBAN '{iban}'.");
    } 
    return false;
  }
  public override int GetHashCode() => Init.GetHashCode();
  public override string ToString() => string.Join(' ', Init.Chunk(size: 4)
                                                            .Select(static x => new string(x)));
}
