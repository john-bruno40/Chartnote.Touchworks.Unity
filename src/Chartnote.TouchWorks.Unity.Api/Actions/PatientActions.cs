using System.Data;
using Chartnote.TouchWorks.Unity.Api.Auth;
using Chartnote.TouchWorks.Unity.Api.Config;
using Chartnote.TouchWorks.Unity.Api.Services;

namespace Chartnote.TouchWorks.Unity.Api.Actions;

/// <summary>
/// Unity Magic actions for patient data.
/// Stateless — receives a UnitySession per call, safe for concurrent physician use.
/// </summary>
public class PatientActions
{
    private readonly IUnityClient _client;
    private readonly UnityConfig _config;

    public PatientActions(IUnityClient client, UnityConfig config)
    {
        _client = client;
        _config = config;
    }

    public async Task<DataSet> SearchPatientsAsync(UnitySession session, string searchTerm)
    {
        session.RequireAuthenticated();
        return await _client.MagicAsync(
            action: "SearchPatients",
            ehrUsername: session.EhrUsername,
            appName: _config.AppName,
            patientId: "",
            token: session.SecurityToken!,
            param1: searchTerm);
    }

    public async Task<DataSet> GetPatientAsync(UnitySession session, string patientId)
    {
        session.RequireAuthenticated();
        return await _client.MagicAsync(
            action: "GetPatient",
            ehrUsername: session.EhrUsername,
            appName: _config.AppName,
            patientId: patientId,
            token: session.SecurityToken!);
    }

    public async Task<DataSet> GetPatientListAsync(UnitySession session)
    {
        session.RequireAuthenticated();
        return await _client.MagicAsync(
            action: "GetPatientList",
            ehrUsername: session.EhrUsername,
            appName: _config.AppName,
            patientId: "",
            token: session.SecurityToken!);
    }

    public async Task<DataSet> GetPatientByVisitAsync(UnitySession session, string visitId)
    {
        session.RequireAuthenticated();
        return await _client.MagicAsync(
            action: "GetPatientByVisit",
            ehrUsername: session.EhrUsername,
            appName: _config.AppName,
            patientId: "",
            token: session.SecurityToken!,
            param1: visitId);
    }
}