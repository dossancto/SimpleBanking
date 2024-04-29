namespace SimpleBanking.Domain.Providers.Exceptions;

/// <summary>
/// Represents queue errors
/// </summary>
public class QueueException(string msg) : Exception(msg)
{
    /// <summary>
    /// Represents a specific queue error
    /// </summary>
    public QueueExceptionType Type { get; set; } = QueueExceptionType.GENERIC;
}

/// <summary>
/// Represents a specific queue error
/// </summary>
public enum QueueExceptionType
{
    /// <summary>
    /// Unknown queue error
    /// </summary>
    GENERIC,

    /// <summary>
    /// When the indicated queue was
    /// not found or is not supported
    /// </summary>
    QUEUE_NOT_FOUND,
}

