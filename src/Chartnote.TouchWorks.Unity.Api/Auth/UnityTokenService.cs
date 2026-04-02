using System.Data;
using Chartnote.TouchWorks.Unity.Api.Config;
using Chartnote.TouchWorks.Unity.Api.Services;

namespace Chartnote.TouchWorks.Unity.Api.Auth;

/// <summary>
/// Manages the two-layer Unity authentication flow.
/// Stateless — all session state lives in UnitySession.
/// Safe to register as a singleton and share concurrently across physician sessions.
///
///   Layer 1 — App credentials → GetSecurityToken → token stored on session
///   Layer 2 — Physician EHR credentials → GetUserAuthentication → session marked authenticated
/// </summary>
public class UnityTokenService
{
    private readonly IUnityClient _client;
    private readonly UnityConfig _config;

    public UnityTokenService(IUnityClient client, UnityConfig config)
    {
        _client = client;
        _config = config;
    }

    /// <summary>
    /// Layer 1: Obtain app-level security token and store it on the session.
    /// </summary>
    public async Task GetSecurityTokenAsync(UnitySession session)
    {
        var token = await _client.GetSecurityTokenAsync(
            _config.ServiceUsername,
            _config.ServicePassword);

        if (token.StartsWith("error:", StringComparison.OrdinalIgnoreCase))
            throw new InvalidOperationException(
                $"Unity returned an error getting security token: {token}");

        session.SecurityToken = token;
        Console.WriteLine($"[Auth:{session.EhrUsername}] Security token obtained.");
    }
    /// <summary>
    /// Layer 2: Authenticate the physician against the TouchWorks EHR.
    /// Must be called after GetSecurityTokenAsync for this session.
    /// </summary>
    public async Task<DataSet> AuthenticatePhysicianAsync(UnitySession session)
    {
        if (string.IsNullOrEmpty(session.SecurityToken))
            throw new InvalidOperationException(
                "No security token on session. Call GetSecurityTokenAsync first.");

        var result = await _client.MagicAsync(
            action: "GetUserAuthentication",
            ehrUsername: session.EhrUsername,
            appName: _config.AppName,
            patientId: "",
            token: session.SecurityToken,
            param1: session.EhrPassword);

        session.IsAuthenticated = true;
        session.AuthenticatedAt = DateTime.UtcNow;
        Console.WriteLine($"[Auth:{session.EhrUsername}] Physician authentication complete.");
        return result;
    }

    /// <summary>
    /// Retires the security token at the end of a physician session.
    /// Always call this when the session ends to clean up server-side resources.
    /// </summary>
    public async Task RetireAsync(UnitySession session)
    {
        if (string.IsNullOrEmpty(session.SecurityToken))
            return;

        await _client.RetireSecurityTokenAsync(session.SecurityToken, _config.AppName);
        session.SecurityToken = null;
        session.IsAuthenticated = false;
        Console.WriteLine($"[Auth:{session.EhrUsername}] Security token retired.");
    }

}