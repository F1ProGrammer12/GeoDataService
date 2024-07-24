namespace GeoDataService.Logic.Exceptions;

public class NotFoundException(string message) : Exception(message) { }

public class ValidationRequestException(string message) : Exception(message) { }