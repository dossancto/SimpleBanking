namespace SimpleBanking.Application.Features.Balances.Data;

/// <summary>
/// Represents actions for Moing Balance
/// </summary>
public interface IBalanceMoveRepository
{
    /// <summary>
    /// Move balance
    /// </summary>
    Task MoveBalance(string TargetId, int ammount);
}
