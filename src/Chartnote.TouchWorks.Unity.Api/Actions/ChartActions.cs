using System.Data;
using Chartnote.TouchWorks.Unity.Api.Auth;
using Chartnote.TouchWorks.Unity.Api.Config;
using Chartnote.TouchWorks.Unity.Api.Services;

namespace Chartnote.TouchWorks.Unity.Api.Actions;

/// <summary>
/// Unity Magic actions for clinical chart data.
/// Stateless — receives a UnitySession per call.
/// These feed the Chartnote AI context prior to note generation.
/// Requires SetEncounterFocus before visit-scoped calls.
/// </summary>
public class ChartActions
{
    private readonly IUnityClient _client;
    private readonly UnityConfig _config;

    public ChartActions(IUnityClient client, UnityConfig config)
    {
        _client = client;
        _config = config;
    }

    public async Task<DataSet> GetClinicalSummaryAsync(UnitySession session, string patientId)
    {
        session.RequireAuthenticated();
        return await _client.MagicAsync(
            action: "GetClinicalSummary",
            ehrUsername: session.EhrUsername,
            appName: _config.AppName,
            patientId: patientId,
            token: session.SecurityToken!);
    }

    public async Task<DataSet> GetResultsAsync(UnitySession session, string patientId)
    {
        session.RequireAuthenticated();
        return await _client.MagicAsync(
            action: "GetResults",
            ehrUsername: session.EhrUsername,
            appName: _config.AppName,
            patientId: patientId,
            token: session.SecurityToken!);
    }

    public async Task<DataSet> GetMedicationsAsync(UnitySession session, string patientId)
    {
        session.RequireAuthenticated();
        return await _client.MagicAsync(
            action: "GetMedications",
            ehrUsername: session.EhrUsername,
            appName: _config.AppName,
            patientId: patientId,
            token: session.SecurityToken!);
    }

    public async Task<DataSet> GetAllergiesAsync(UnitySession session, string patientId)
    {
        session.RequireAuthenticated();
        return await _client.MagicAsync(
            action: "GetAllergies",
            ehrUsername: session.EhrUsername,
            appName: _config.AppName,
            patientId: patientId,
            token: session.SecurityToken!);
    }

    public async Task<DataSet> GetProblemsAsync(UnitySession session, string patientId)
    {
        session.RequireAuthenticated();
        return await _client.MagicAsync(
            action: "GetProblems",
            ehrUsername: session.EhrUsername,
            appName: _config.AppName,
            patientId: patientId,
            token: session.SecurityToken!);
    }

    public async Task<DataSet> GetCommentsAsync(UnitySession session, string patientId)
    {
        session.RequireAuthenticated();
        return await _client.MagicAsync(
            action: "GetComments",
            ehrUsername: session.EhrUsername,
            appName: _config.AppName,
            patientId: patientId,
            token: session.SecurityToken!);
    }

    public async Task<DataSet> GetOrdersAsync(UnitySession session, string patientId)
    {
        session.RequireAuthenticated();
        return await _client.MagicAsync(
            action: "GetOrders",
            ehrUsername: session.EhrUsername,
            appName: _config.AppName,
            patientId: patientId,
            token: session.SecurityToken!);
    }
}