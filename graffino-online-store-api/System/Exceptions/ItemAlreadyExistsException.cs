namespace graffino_online_store_api.System.Exceptions;

public class ItemAlreadyExistsException(string? message) : Exception(message);