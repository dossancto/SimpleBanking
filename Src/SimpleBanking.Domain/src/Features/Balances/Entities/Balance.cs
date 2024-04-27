namespace SimpleBanking.Domain.Features.Balances.Entities;

/// <summary>
/// Represents a user Balance
/// </summary>
public class Balance
{
    /// <summary>
    /// Represent the debit balance of the Account.
    /// As int, so, 100 == 1.00
    /// This value must be divited by `DebitFactor`
    /// </summary>
    public int Debit { get; set; }

    /// <summary>
    /// Represent the divition factor of the Debit
    /// </summary>
    public int DebitFactor { get; set; }
}

