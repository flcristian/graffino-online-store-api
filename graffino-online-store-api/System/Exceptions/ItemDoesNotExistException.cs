namespace graffino_online_store_api.System.Exceptions;

public class ItemDoesNotExistException(string? message) : Exception(message);