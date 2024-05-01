using SimpleBanking.Domain.Exceptions;

namespace SimpleBanking.API.Middlewares.ErrorMiddlewares;

public static class EntityAlreadyExistsExceptionHandler
{
    public static HandledResponse HandleError(this EntityAlreadyExistsException ex)
      => new()
      {
          Content = new BaseError()
          {
              Kind = "ENTITY_ALREADY_EXISTS",
              Message = ex.Message,
          },
          StatusCode = 409,
      };
}




