using SimpleBanking.Domain.Features.Accounts.Entities;
using SimpleBanking.Domain.Features.Balances.Entities;

namespace SimpleBanking.Domain.Features.Merchants.Entities;

/// <summary>
/// Represents a Merchant Account
/// </summary>
public class Merchant : Account
{
    /// <summary>
    /// This person unique identifier
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// This merchant CNPJ
    /// </summary>
    public string CNPJ { get; set; } = string.Empty;

    /// <summary>
    /// Represents information about Balance
    /// </summary>
    public Balance Balance { get; set; } = new();
}

