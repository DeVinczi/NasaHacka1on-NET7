namespace NasaHacka1on.Models.Errors;

internal class ResultApiError
{
    public string Code { get; set; }
    public IReadOnlyCollection<string> Messages { get; init; }
    public string Property { get; set; }
}

