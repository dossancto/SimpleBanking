using SimpleBanking.Domain.Exceptions;

namespace SimpleBanking.API.Middlewares.ErrorMiddlewares;

public static class NotFoundExceptionHandler
{
    public static HandledResponse HandleError(this NotFoundException ex)
      => new()
      {
          Content = new BaseError()
          {
              Kind = "NOT_FOUND",
              Message = ex.Message,
          },
          StatusCode = 404,
      };
}



