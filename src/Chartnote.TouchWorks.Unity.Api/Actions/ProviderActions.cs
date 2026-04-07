using System.Data;
using Chartnote.TouchWorks.Unity.Api.Auth;
using Chartnote.TouchWorks.Unity.Api.Config;
using Chartnote.TouchWorks.Unity.Api.Services;

namespace Chartnote.TouchWorks.Unity.Api.Actions;

/// <summary>
/// Unity Magic actions for provider/user lookups.
/// Stateless — receives a UnitySession per call.
/// </summary>
public class ProviderActions
{
    private readonly IUnityClient _client;
    private readonly UnityConfig _config;

    public ProviderActions(IUnityClient client, UnityConfig config)
    {
        _client = client;
        _config = config;
    }

    /// <summary>
    /// Returns provider details by one or more identifiers.
    /// Parameter mapping should be validated against sandbox action details.
    /// </summary>
    public async Task<DataSet> GetProviderAsync(
        UnitySession session,
        string providerId = "",
        string userName = "",
        string npi = "")
    {
        session.RequireAuthenticated();
        return await _client.MagicAsync(
            action: "GetProvider",
            ehrUsername: session.EhrUsername,
            appName: _config.AppName,
            patientId: "",
            token: session.SecurityToken!,
            param1: providerId,
            param2: userName,
            param3: npi);
    }

    public async Task<DataSet> GetProviderInfoAsync(UnitySession session)
    {
        session.RequireAuthenticated();
        return await _client.MagicAsync(
            action: "GetProviderInfo",
            ehrUsername: session.EhrUsername,
            appName: _config.AppName,
            patientId: "",
            token: session.SecurityToken!);
    }

    public async Task<DataSet> GetProvidersAsync(UnitySession session, string providerFilter = "")
    {
        session.RequireAuthenticated();
        return await _client.MagicAsync(
            action: "GetProviders",
            ehrUsername: session.EhrUsername,
            appName: _config.AppName,
            patientId: "",
            token: session.SecurityToken!,
            param1: providerFilter);
    }

    public async Task<DataSet> GetUserIdAsync(UnitySession session, string userName)
    {
        session.RequireAuthenticated();
        return await _client.MagicAsync(
            action: "GetUserID",
            ehrUsername: session.EhrUsername,
            appName: _config.AppName,
            patientId: "",
            token: session.SecurityToken!,
            param1: userName);
    }

    public async Task<DataSet> GetUserPreferencesAsync(UnitySession session)
    {
        session.RequireAuthenticated();
        return await _client.MagicAsync(
            action: "GetUserPreferences",
            ehrUsername: session.EhrUsername,
            appName: _config.AppName,
            patientId: "",
            token: session.SecurityToken!);
    }

    public async Task<DataSet> GetUserSiteInfoAsync(UnitySession session)
    {
        session.RequireAuthenticated();
        return await _client.MagicAsync(
            action: "GetUserSiteInfo",
            ehrUsername: session.EhrUsername,
            appName: _config.AppName,
            patientId: "",
            token: session.SecurityToken!);
    }
}
