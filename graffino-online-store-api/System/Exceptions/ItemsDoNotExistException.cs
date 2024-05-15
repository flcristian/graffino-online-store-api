namespace graffino_online_store_api.System.Exceptions;

public class ItemsDoNotExistException(string? message) : Exception(message);