using Gybs;
using Gybs.Results;
using Microsoft.AspNetCore.Mvc;
using NasaHacka1on.Cqrs;
using NasaHacka1on.Models.Errors;
using IResult = Gybs.IResult;

namespace NasaHacka1on.Models;

[ApiController]
public abstract class CommunityCodeHubController : ControllerBase
{
    protected IActionResult Error(IResult result)
    {
        var groups = result.Errors.GroupBy(r => r.Key).ToList();

        var forbiddenErrorsGroup = groups.SingleOrDefault(f => f.Key == ResultErrorKeys.Forbidden);

        if (forbiddenErrorsGroup is not null)
        {
            return StatusCode(StatusCodes.Status403Forbidden);
        }

        var notFoundErrorsGroup = groups.SingleOrDefault(f => f.Key == ResultErrorKeys.NotFound);

        if (notFoundErrorsGroup is not null)
        {
            return NotFound(GetErrors(Result.Failure(notFoundErrorsGroup.ToDictionary(f => f.Key, f => f.Value.ToList() as IReadOnlyCollection<string>))));
        }

        var conflictErrorsGroup = groups.SingleOrDefault(f => f.Key == ResultErrorKeys.Conflict);

        if (conflictErrorsGroup is not null)
        {
            return Conflict(Result.Failure(conflictErrorsGroup.ToDictionary(f => f.Key, f => f.Value.ToList() as IReadOnlyCollection<string>)));
        }

        return BadRequest(groups.SelectMany(x => x).ToList().ToApiErrors());
    }

    private static IResult<ResultApiError> GetErrors(IResult errorsGroup)
    {
        return errorsGroup.Map<ResultApiError>();
    }
}
