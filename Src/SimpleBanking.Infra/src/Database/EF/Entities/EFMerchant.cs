using SimpleBanking.Domain.DomainTypes;
using SimpleBanking.Domain.Features.Merchants.Entities;

namespace SimpleBanking.Infra.Database.EF.Entities;

public class EFMerchant : BaseRecord
{
    /// <summary>
    /// This Merchant unique identifier
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// This Merchant CNPJ
    /// </summary>
    public string CNPJ { get; set; } = string.Empty;

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

    public static implicit operator EFMerchant(Merchant p)
      => new()
      {
          Salt = p.Salt,
          HashedPassword = p.HashedPassword,
          Id = p.Id,
          CNPJ = p.CNPJ,
          Debit = p.Balance.Debit,
          DebitFactor = p.Balance.DebitFactor,
          EmailAddress = p.EmailAddress,
          ResponsableFullName = p.ResponsableFullName
      };

    public static implicit operator Merchant?(EFMerchant? p)
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
            CNPJ = p.CNPJ,
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



