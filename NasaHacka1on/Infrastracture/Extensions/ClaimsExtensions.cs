using System.Security.Claims;

namespace NasaHacka1on.Infrastracture.Extensions;

public static class ClaimsExtensions
{
    public static Guid? GetSubject(this IEnumerable<Claim> claims)
    {
        var subject = claims.SingleOrDefault(x => x.Type == "sub");

        if (subject is null || !Guid.TryParse(subject.Value, out var parsedSubject))
        {
            return null;
        }

        return parsedSubject;
    }

    public static Guid GetSubjectOrDefault(this IEnumerable<Claim> claims)
    {
        return claims.GetSubject() ?? Guid.Empty;
    }

    public static string GetEmail(this IEnumerable<Claim> claims)
    {
        return GetClaim(claims, ClaimTypes.Email);
    }

    public static string GetName(this IEnumerable<Claim> claims)
    {
        return GetClaim(claims, ClaimTypes.Name);
    }

    public static string[] GetRoles(this IEnumerable<Claim> claims)
    {
        return GetClaims(claims, ClaimTypes.Role);
    }

    public static string GetSurname(this IEnumerable<Claim> claims)
    {
        return GetClaim(claims, ClaimTypes.Surname);
    }

    public static string GetGivenName(this IEnumerable<Claim> claims)
    {
        return GetClaim(claims, ClaimTypes.GivenName);
    }

    public static string GetCountry(this IEnumerable<Claim> claims)
    {
        return GetClaim(claims, ClaimTypes.Country);
    }

    public static string GetLocality(this IEnumerable<Claim> claims)
    {
        return GetClaim(claims, ClaimTypes.Locality);
    }

    private static string GetClaim(IEnumerable<Claim> claims, string type)
    {
        var claim = claims.SingleOrDefault(x => x.Type == type);

        if (claim is null || string.IsNullOrWhiteSpace(claim.Value))
        {
            return null;
        }

        return claim.Value;
    }

    private static string[] GetClaims(IEnumerable<Claim> claims, string type)
    {
        var filteredClaims = claims
            .Where(x => x.Type == type)
            .Where(x => !string.IsNullOrWhiteSpace(x.Value))
            .ToList();

        if (filteredClaims.Count is 0)
        {
            return Array.Empty<string>();
        }

        return filteredClaims.Select(x => x.Value).ToArray();
    }
}
