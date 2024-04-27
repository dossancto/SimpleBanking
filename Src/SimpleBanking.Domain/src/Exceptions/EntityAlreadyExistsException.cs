namespace SimpleBanking.Domain.Exceptions;

/// <summary>
/// Represents a an when a entity already exists
/// </summary>
public class EntityAlreadyExistsException(string msg) : Exception(msg);
