using System.Data;
using Chartnote.TouchWorks.Unity.Api.Auth;
using Chartnote.TouchWorks.Unity.Api.Config;
using Chartnote.TouchWorks.Unity.Api.Services;

namespace Chartnote.TouchWorks.Unity.Api.Actions;

/// <summary>
/// Unity Magic actions for schedule and appointment queries.
/// Stateless — receives a UnitySession per call.
/// </summary>
public class ScheduleActions
{
    private readonly IUnityClient _client;
    private readonly UnityConfig _config;

    public ScheduleActions(IUnityClient client, UnityConfig config)
    {
        _client = client;
        _config = config;
    }

    /// <summary>
    /// Returns appointments for a user during a date range.
    /// Parameter mapping should be validated against sandbox action details.
    /// </summary>
    public async Task<DataSet> GetScheduleAsync(
        UnitySession session,
        string startDate,
        string endDate,
        string scheduleUser = "")
    {
        session.RequireAuthenticated();
        return await _client.MagicAsync(
            action: "GetSchedule",
            ehrUsername: session.EhrUsername,
            appName: _config.AppName,
            patientId: "",
            token: session.SecurityToken!,
            param1: startDate,
            param2: endDate,
            param3: scheduleUser);
    }

    public async Task<DataSet> GetScheduleBySpecialtyAsync(
        UnitySession session,
        string specialty,
        string startDate = "",
        string endDate = "")
    {
        session.RequireAuthenticated();
        return await _client.MagicAsync(
            action: "GetScheduleBySpecialty",
            ehrUsername: session.EhrUsername,
            appName: _config.AppName,
            patientId: "",
            token: session.SecurityToken!,
            param1: specialty,
            param2: startDate,
            param3: endDate);
    }

    public async Task<DataSet> GetPatientListAsync(
        UnitySession session,
        string scheduleDate,
        string location = "")
    {
        session.RequireAuthenticated();
        return await _client.MagicAsync(
            action: "GetPatientList",
            ehrUsername: session.EhrUsername,
            appName: _config.AppName,
            patientId: "",
            token: session.SecurityToken!,
            param1: scheduleDate,
            param2: location);
    }

    public async Task<DataSet> SetUserOutOfOfficeAsync(
        UnitySession session,
        string startDate,
        string endDate,
        string reason = "")
    {
        session.RequireAuthenticated();
        return await _client.MagicAsync(
            action: "SetUserOutOfOffice",
            ehrUsername: session.EhrUsername,
            appName: _config.AppName,
            patientId: "",
            token: session.SecurityToken!,
            param1: startDate,
            param2: endDate,
            param3: reason);
    }
}
