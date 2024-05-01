using SimpleBanking.Domain.Features.Balances.Exceptions;

namespace SimpleBanking.API.Middlewares.ErrorMiddlewares;

public static class TransferHandlers
{
    public static HandledResponse HandleTransferError(this TransferException ex)
      => ex.ErrorType switch
      {
          TransferErrorType.UNSUPORTED_SENDER
          or TransferErrorType.INSUFICIENT_AMMOUNT
          or TransferErrorType.UNSUPORTED_SENDER
       => new()
       {
           Content = new BaseError()
           {
               Kind = ex.ErrorType.ToString(),
               Details = ex.Details ?? "",
               Message = ex.Message
           },
           StatusCode = 422
       },

          TransferErrorType.NOT_AUTHORIZED
           => new()
           {
               Content = new BaseError()
               {
                   Kind = ex.ErrorType.ToString(),
                   Details = ex.Details ?? "",
                   Message = ex.Message
               },
               StatusCode = 403
           },

          _ => new()
          {
              Content = new BaseError()
              {
                  Kind = TransferErrorType.GENERIC.ToString(),
                  Message = "Something went wrong while transferring"
              },
              StatusCode = 500
          }
      };
}

