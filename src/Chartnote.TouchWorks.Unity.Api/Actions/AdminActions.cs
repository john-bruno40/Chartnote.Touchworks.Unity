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
}