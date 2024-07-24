using GeoDataService.Logic.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace GeoDataService.Logic.Interfaces;

public interface IValidableRequest
{
    public static Task IsValidRequest<T>(T request)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));
        var validationContext = new ValidationContext(request);
        var results = new List<ValidationResult>();

        if (!Validator.TryValidateObject(request, validationContext, results, true))
            throw new ValidationRequestException(string.Join("\n", results.Select(x => x.ErrorMessage)));

        return Task.CompletedTask;
    }
}