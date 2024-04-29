namespace SimpleBanking.Adapters.Transfering;

public interface ITransferAuthorizerAdapter
{
    public Task<bool> Authorize();
}
