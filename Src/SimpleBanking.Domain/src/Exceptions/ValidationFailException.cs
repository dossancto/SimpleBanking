namespace SimpleBanking.Domain.Exceptions;

/// <summary>
/// Represents a error unit, with field and erro message
/// </summary>
public record ValidationError(string Field, string Message);

/// <summary>
/// Errors Grouped by Field.
/// </summary>
public record ValidationFieldWithErrors(string Field, IEnumerable<string> Errors);

/// <summary>
/// Represents a error in some validaiton
/// </summary>
public class ValidationFailException : Exception
{
    /// <summary>
    /// The list of errors
    /// </summary>
    public IEnumerable<ValidationError> Errors { get; private set; }

    /// <summary>
    /// An Fail with Description Message and Error list
    /// </summary>
    public ValidationFailException(string msg, IEnumerable<ValidationError> errors) : base(msg)
     => Errors = errors;

    /// <summary>
    /// An Fail with only Description Message;
    /// </summary>
    public ValidationFailException(string msg)
      : this(msg, new List<ValidationError>()) { }

    /// <summary>
    /// Gets the errors in a text-friendly way
    /// </summary>
    public override string ToString()
    {
        var erros = string.Join("\n", Errors);
        return $"{Message}\n-----------\n{erros}";
    }

    /// <summary>
    /// Get a list of errors from a field
    /// </summary>
    public IEnumerable<ValidationFieldWithErrors> ListErrorList()
      => Errors
              .GroupBy(x => x.Field)
              .Select(x => new ValidationFieldWithErrors(x.Key, x.Select(y => y.Message)))
              ;

}
