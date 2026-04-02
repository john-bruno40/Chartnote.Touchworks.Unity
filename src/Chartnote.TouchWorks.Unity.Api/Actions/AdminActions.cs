using System.Data;
using Chartnote.TouchWorks.Unity.Api.Auth;
using Chartnote.TouchWorks.Unity.Api.Config;
using Chartnote.TouchWorks.Unity.Api.Services;

namespace Chartnote.TouchWorks.Unity.Api.Actions;

/// <summary>
/// Unity Magic administrative and diagnostic actions.
/// Stateless — receives a UnitySession per call (except Echo, which needs no auth).
/// </summary>
public class AdminActions
{
    private readonly IUnityClient _client;
    private readonly UnityConfig _config;

    public AdminActions(IUnityClient client, UnityConfig config)
    {
        _client = client;
        _config = config;
    }

    /// <summary>
    /// Connection test — no security token or physician credentials needed.
    /// Always the first call to verify endpoint connectivity.
    /// </summary>
    public async Task<DataSet> EchoAsync(string testValue = "Chartnote-Echo-Test")
    {
        return await _client.MagicAsync(
            action: "Echo",
            ehrUsername: "",
            appName: _config.AppName,
            patientId: "",
            token: "",
            param1: testValue);
    }

    public async Task<DataSet> GetServerInfoAsync(UnitySession session)
    {
        session.RequireAuthenticated();
        return await _client.MagicAsync(
            action: "GetServerInfo",
            ehrUsername: session.EhrUsername,
            appName: _config.AppName,
            patientId: "",
            token: session.SecurityToken!);
    }

    public async Task<DataSet> GetLastLogsAsync(UnitySession session, string hoursBack = "1")
    {
        session.RequireAuthenticated();
        return await _client.MagicAsync(
            action: "LastLogs",
            ehrUsername: session.EhrUsername,
            appName: _config.AppName,
            patientId: "",
            token: session.SecurityToken!,
            param1: hoursBack);
    }

    /// <summary>
    /// Sets the user's site. A site is tied to an organization in TouchWorks.
    /// May need to be called before other actions in multi-org deployments.
    /// </summary>
    public async Task<DataSet> SetSiteFocusAsync(UnitySession session, string siteId)
    {
        session.RequireAuthenticated();
        return await _client.MagicAsync(
            action: "SetSiteFocus",
            ehrUsername: session.EhrUsername,
            appName: _config.AppName,
            patientId: "",
            token: session.SecurityToken!,
            param1: siteId);
    }

    /// <summary>
    /// Returns the internal organization ID linked to the organization name.
    /// Typically used by an application launched by TouchWorks EHR.
    /// </summary>
    public async Task<DataSet> GetOrganizationIDAsync(UnitySession session, string organizationName)
    {
        session.RequireAuthenticated();
        return await _client.MagicAsync(
            action: "GetOrganizationID",
            ehrUsername: session.EhrUsername,
            appName: _config.AppName,
            patientId: "",
            token: session.SecurityToken!,
            param1: organizationName);
    }

}