using SimpleBanking.Domain.DomainTypes;
using SimpleBanking.Domain.Features.Persons.Entities;

namespace SimpleBanking.Infra.Database.EF.Entities;

public class EFPerson : BaseRecord
{
    /// <summary>
    /// This person unique identifier
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// This person CPF
    /// </summary>
    public string CPF { get; set; } = string.Empty;

    /// <summary>
    /// Represent the debit balance of the Account.
    /// As int, so, 100 == 1.00
    /// This value must be divited by `DebitFactor`
    /// </summary>
    public int Debit { get; set; }

    /// <summary>
    /// Represent the divition factor of the Debit
    /// </summary>
    public int DebitFactor { get; set; } = 100;

    /// <summary>
    /// The Fullname of the account owner
    /// </summary>
    /// <example>test@test.com</example>
    public string ResponsableFullName { get; set; } = string.Empty;

    /// <summary>
    /// A email email address associated with this account
    /// </summary>
    /// <example>test@test.com</example>
    public string EmailAddress { get; set; } = string.Empty;

    /// <summary>
    /// The hashed secure account password
    /// </summary>
    public string HashedPassword { get; set; } = string.Empty;

    /// <summary>
    /// Main search field used as search
    /// </summary>
    public string Search { get; set; } = string.Empty;

    /// <summary>
    /// A secure key used as additional security mechanism for the account
    /// </summary>
    public string Salt { get; set; } = string.Empty;

    public string BuildSearch()
      => EFPerson.BuildSearch(EmailAddress, CPF, Id);

    public static string BuildSearch(string EmailAddress, string CPF, string Id)
      => string.Join(",", [
        EmailAddress,
        CPF,
        Id
    ]);

    public static implicit operator EFPerson(Person p)
      => new()
      {
          Salt = p.Salt,
          HashedPassword = p.HashedPassword,
          Id = p.Id,
          CPF = p.CPF,
          Debit = p.Balance.Debit,
          DebitFactor = p.Balance.DebitFactor,
          EmailAddress = p.EmailAddress,
          ResponsableFullName = p.ResponsableFullName,
          Search = BuildSearch(p.EmailAddress, p.CPF, p.Id)
      };

    public static implicit operator Person?(EFPerson? p)
    {
        if (p is null)
        {
            return null;
        }

        return new()
        {
            Salt = p.Salt,
            HashedPassword = p.HashedPassword,
            Id = p.Id,
            CPF = p.CPF,
            Balance = new()
            {
                Debit = p.Debit,
                DebitFactor = p.DebitFactor,
            },
            EmailAddress = p.EmailAddress,
            ResponsableFullName = p.ResponsableFullName
        };
    }
}


