namespace SimpleBanking.Domain.Providers.Queues;

/// <summary>
/// Represents usable queues
/// </summary>
public enum QueuePipeline
{
    /// <summary>
    /// Represents a event when some money has been transfered
    /// </summary>
    MONEY_TRANSFERED = 1,
}
