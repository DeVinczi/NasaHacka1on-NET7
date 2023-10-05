using NasaHacka1on.Cqrs;
using NasaHacka1on.Models.Extensions;

namespace NasaHacka1on.Models.Errors;

internal static class ResultErrorExtensions
{
    public static IReadOnlyList<ResultApiError> ToApiErrors(this IEnumerable<KeyValuePair<string, IReadOnlyCollection<string>>> errors)
    {
        var apiErrors = new List<ResultApiError>();

        foreach (var error in errors)
        {
            var apiError = CreateResultApiError(error);
            apiErrors.Add(apiError);
        }

        return apiErrors;
    }

    private static ResultApiError CreateResultApiError(KeyValuePair<string, IReadOnlyCollection<string>> error)
    {
        var apiError = new ResultApiError
        {
            Messages = error.Value
        };

        if (error.Key is ResultErrorKeys.NotFound)
        {
            apiError.Code = error.Key;

            return apiError;
        }

        if (error.Key is ResultErrorKeys.Conflict)
        {
            apiError.Code = error.Key;

            return apiError;
        }

        apiError.Code = ResultErrorKeys.InvalidParameter;
        apiError.Property = error.Key.ToFirstCharLower();

        return apiError;
    }
}
