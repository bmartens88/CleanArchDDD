using System.Runtime.CompilerServices;

namespace Ardalis.GuardClauses;

/// <summary>
/// Extension for GuardClauses.
/// </summary>
public static class LengthGuard
{
    /// <summary>
    /// Guard for a string type which guards against a defined length of the given string.
    /// </summary>
    /// <param name="guardClause"><see cref="IGuardClause"> to extend.</param>
    /// <param name="input">The input string.</param>
    /// <param name="length">The length parameter for the Guard Clause.</param>
    /// <param name="parameterName">Optional name of the parameter which is validated.</param>
    public static string Length(this IGuardClause guardClause, string input, int length, [CallerArgumentExpression("input")] string? parameterName = null)
    {
        if (input.Length >= length)
            throw new ArgumentException($"Should not exceed length of {length}", parameterName);
        return input;
    }
}
