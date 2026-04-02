namespace Chartnote.TouchWorks.Unity.Api.Auth;

/// <summary>
/// Holds all state for a single physician's Unity session.
/// One instance per physician per session — never shared across physicians.
///
/// Lifetime: created when a physician authenticates, discarded when the session ends.
/// In production, the backend creates one UnitySession per incoming physician request context.
/// </summary>
public class UnitySession
{
    public string EhrUsername { get; }
    public string EhrPassword { get; }
    public string? SecurityToken { get; internal set; }
    public bool IsAuthenticated { get; internal set; }
    public DateTime? AuthenticatedAt { get; internal set; }

    public UnitySession(string ehrUsername, string ehrPassword)
    {
        if (string.IsNullOrWhiteSpace(ehrUsername))
            throw new ArgumentException("EHR username is required.", nameof(ehrUsername));
        if (string.IsNullOrWhiteSpace(ehrPassword))
            throw new ArgumentException("EHR password is required.", nameof(ehrPassword));

        EhrUsername = ehrUsername;
        EhrPassword = ehrPassword;
    }

    /// <summary>
    /// Throws if the session is not fully authenticated.
    /// Call before any clinical Magic action.
    /// </summary>
    public void RequireAuthenticated()
    {
        if (string.IsNullOrEmpty(SecurityToken))
            throw new InvalidOperationException(
                "No security token. Call UnityTokenService.GetSecurityTokenAsync first.");

        if (!IsAuthenticated)
            throw new InvalidOperationException(
                "Physician not authenticated. Call UnityTokenService.AuthenticatePhysicianAsync first.");
    }
}