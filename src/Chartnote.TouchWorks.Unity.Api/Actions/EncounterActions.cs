using System.Data;
using Chartnote.TouchWorks.Unity.Api.Auth;
using Chartnote.TouchWorks.Unity.Api.Config;
using Chartnote.TouchWorks.Unity.Api.Services;

namespace Chartnote.TouchWorks.Unity.Api.Actions;

/// <summary>
/// Unity Magic actions for encounter/visit data.
/// Stateless — receives a UnitySession per call.
/// Note: SetEncounterFocus must be called before any visit-scoped actions.
/// </summary>
public class EncounterActions
{
    private readonly IUnityClient _client;
    private readonly UnityConfig _config;

    public EncounterActions(IUnityClient client, UnityConfig config)
    {
        _client = client;
        _config = config;
    }

    /// <summary>
    /// Required before visit-scoped calls. Sets the active visit context.
    /// </summary>
    public async Task<DataSet> SetEncounterFocusAsync(UnitySession session, string patientId, string visitId)
    {
        session.RequireAuthenticated();
        return await _client.MagicAsync(
            action: "SetEncounterFocus",
            ehrUsername: session.EhrUsername,
            appName: _config.AppName,
            patientId: patientId,
            token: session.SecurityToken!,
            param1: visitId);
    }

    public async Task<DataSet> GetVisitAsync(UnitySession session, string patientId)
    {
        session.RequireAuthenticated();
        return await _client.MagicAsync(
            action: "GetVisit",
            ehrUsername: session.EhrUsername,
            appName: _config.AppName,
            patientId: patientId,
            token: session.SecurityToken!);
    }

    public async Task<DataSet> GetVisitHistoryAsync(UnitySession session, string patientId)
    {
        session.RequireAuthenticated();
        return await _client.MagicAsync(
            action: "GetVisitHistory",
            ehrUsername: session.EhrUsername,
            appName: _config.AppName,
            patientId: patientId,
            token: session.SecurityToken!);
    }

    public async Task<DataSet> GetAppointmentsAsync(UnitySession session, string patientId)
    {
        session.RequireAuthenticated();
        return await _client.MagicAsync(
            action: "GetAppointments",
            ehrUsername: session.EhrUsername,
            appName: _config.AppName,
            patientId: patientId,
            token: session.SecurityToken!);
    }

    public async Task<DataSet> GetEncounterListAsync(UnitySession session, string patientId)
    {
        session.RequireAuthenticated();
        return await _client.MagicAsync(
            action: "GetEncounterList",
            ehrUsername: session.EhrUsername,
            appName: _config.AppName,
            patientId: patientId,
            token: session.SecurityToken!);
    }

    public async Task<DataSet> GetScheduleAsync(UnitySession session, string startDate, string endDate)
    {
        session.RequireAuthenticated();
        return await _client.MagicAsync(
            action: "GetSchedule",
            ehrUsername: session.EhrUsername,
            appName: _config.AppName,
            patientId: "",
            token: session.SecurityToken!,
            param1: startDate,
            param2: endDate);
    }

}