using System.Text.RegularExpressions;

namespace SimpleBanking.Application.Features.Merchants.UseCases.CreateMerchant;

public class CreateMerchantInput
{
    /// <summary>
    /// The person fullname
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// Represents the Person email
    /// </summary>
    public required string Email { get; set; }

    /// <summary>
    /// Represents the Person password to be hashed so stored
    /// </summary>
    public required string Password { get; set; }

    /// <summary>
    /// The user CPF. Any non digit character will be removed
    /// </summary>
    /// <example>92211617069</example>
    public required string CNPJ { get; set; }

    /// <summary>
    /// Formats and clean the input
    /// </summary>
    public void Format()
    {
        Email = Email.Trim().ToLower();
        CNPJ = Regex.Replace(CNPJ, @"\D", ""); ;
    }
}
