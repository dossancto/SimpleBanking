using SimpleBanking.Domain.Exceptions;

namespace SimpleBanking.API.Middlewares.ErrorMiddlewares;

public class ValidationFailHandled : BaseError
{
    public required IEnumerable<ValidationFieldWithErrors> Errors { get; set; }
}

public static class ValidationFailExceptionHandler
{
    public static HandledResponse HandleError(this ValidationFailException ex)
      => new()
      {
          Content = new ValidationFailHandled()
          {
              Errors = ex.ListErrorList(),
              Kind = "VALIDATION_FAIL",
              Message = ex.Message,
          },
          StatusCode = 400,
      };
}


