namespace SimpleBanking.Domain.Exceptions;

/// <summary>
/// Represents a an when a entity was not found.
/// </summary>
public class NotFoundException(string msg) : Exception(msg);

