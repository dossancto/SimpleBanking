namespace SimpleBanking.Domain.Providers.Services;

/// <summary>
/// Represents a unit of work helper
/// </summary>
public interface IUnitOfWork : IDisposable
{
    Task Begin();
    Task Finish();
    Task Rollback();
    Task Apply();

    /// <summary>
    /// Run the following code in a sandbox.
    /// If something fails automaticly rollbacks
    /// </summary>
    /// <returns>Returns the function return type </returns>
    public async Task<T> Sandbox<T>(Func<Task<T>> func)
    {
        try
        {
            await Begin();

            var result = await func();

            await Apply();

            return result;
        }
        catch
        {
            await Rollback();
            throw;
        }
        finally
        {
            await Finish();
        }
    }

    /// <summary>
    /// Run the following code in a sandbox.
    /// If something fails automaticly rollbacks
    /// </summary>
    public Task Sandbox(Func<Task> func)
      => Sandbox(async () =>
      {
          await func();
          return true;
      });
}

