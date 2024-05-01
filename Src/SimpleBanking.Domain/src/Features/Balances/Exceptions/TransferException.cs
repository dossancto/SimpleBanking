namespace SimpleBanking.Domain.Features.Balances.Exceptions;

/// <summary>
/// Represents a error while transferring money
/// </summary>
public class TransferException(string msg) : Exception(msg)
{
    /// <summary>
    /// The Error kind that occurred. 
    /// </summary>
    public required TransferErrorType ErrorType { get; set; }

    /// <summary>
    /// Details about the fail
    /// </summary>
    public string? Details { get; set; }
}

/// <summary>
/// Transfer Error Types
/// </summary>
public enum TransferErrorType
{
    /// <summary>
    /// Represents a generic transfer error
    /// </summary>
    GENERIC,

    /// <summary>
    /// When a sender is not supported to transfer
    /// </summary>
    UNSUPORTED_SENDER,

    /// <summary>
    /// The receiver is not suppored
    /// </summary>
    UNSUPORTED_RECEIVER,

    /// <summary>
    /// When the sender does not have the required ammount
    /// </summary>
    INSUFICIENT_AMMOUNT,

    /// <summary>
    /// Some error occurred in database level
    /// </summary>
    ERROR_IN_TRANSACTION,

    /// <summary>
    /// THe transaction is not authorized
    /// </summary>
    NOT_AUTHORIZED,
}
