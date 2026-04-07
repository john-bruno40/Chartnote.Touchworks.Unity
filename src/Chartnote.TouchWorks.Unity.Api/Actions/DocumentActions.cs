using System.Data;
using System.Collections.Generic;
using Chartnote.TouchWorks.Unity.Api.Auth;
using Chartnote.TouchWorks.Unity.Api.Config;
using Chartnote.TouchWorks.Unity.Api.Services;

namespace Chartnote.TouchWorks.Unity.Api.Actions;

/// <summary>
/// Unity Magic actions for clinical document management.
/// Stateless — receives a UnitySession per call.
/// SaveDocument and SignDocument are the core Chartnote AI note write-back path.
/// </summary>
public class DocumentActions
{
    private readonly IUnityClient _client;
    private readonly UnityConfig _config;

    public DocumentActions(IUnityClient client, UnityConfig config)
    {
        _client = client;
        _config = config;
    }

    public async Task<DataSet> SaveDocumentAsync(UnitySession session, string patientId, string documentText, string documentTypeId = "")
    {
        session.RequireAuthenticated();
        return await _client.MagicAsync(
            action: "SaveDocument",
            ehrUsername: session.EhrUsername,
            appName: _config.AppName,
            patientId: patientId,
            token: session.SecurityToken!,
            param1: documentText,
            param2: documentTypeId);
    }

    public async Task<DataSet> SignDocumentAsync(UnitySession session, string patientId, string documentId)
    {
        session.RequireAuthenticated();
        return await _client.MagicAsync(
            action: "SignDocument",
            ehrUsername: session.EhrUsername,
            appName: _config.AppName,
            patientId: patientId,
            token: session.SecurityToken!,
            param1: documentId);
    }

    public async Task<DataSet> GetDocumentsAsync(UnitySession session, string patientId)
    {
        session.RequireAuthenticated();
        return await _client.MagicAsync(
            action: "GetDocuments",
            ehrUsername: session.EhrUsername,
            appName: _config.AppName,
            patientId: patientId,
            token: session.SecurityToken!);
    }

    public async Task<DataSet> GetDocumentTextAsync(UnitySession session, string patientId, string documentId)
    {
        session.RequireAuthenticated();
        return await _client.MagicAsync(
            action: "GetDocumentText",
            ehrUsername: session.EhrUsername,
            appName: _config.AppName,
            patientId: patientId,
            token: session.SecurityToken!,
            param1: documentId);
    }

    public async Task<DataSet> GetDocumentsToSignAsync(UnitySession session)
    {
        session.RequireAuthenticated();
        return await _client.MagicAsync(
            action: "GetDocumentsToSign",
            ehrUsername: session.EhrUsername,
            appName: _config.AppName,
            patientId: "",
            token: session.SecurityToken!);
    }

    public async Task<DataSet> SaveCommentAsync(UnitySession session, string patientId, string commentText)
    {
        session.RequireAuthenticated();
        return await _client.MagicAsync(
            action: "SaveComment",
            ehrUsername: session.EhrUsername,
            appName: _config.AppName,
            patientId: patientId,
            token: session.SecurityToken!,
            param1: commentText);
    }

    /// <summary>
    /// Appends text to an existing encounter note.
    /// Core Chartnote write-back action — replaces the Chrome extension approach.
    /// Requires SetEncounterFocus before calling.
    /// </summary>
    public async Task<DataSet> SaveEncounterNoteAsync(UnitySession session, string patientId, string noteText)
    {
        session.RequireAuthenticated();
        return await _client.MagicAsync(
            action: "SaveEncounterNote",
            ehrUsername: session.EhrUsername,
            appName: _config.AppName,
            patientId: patientId,
            token: session.SecurityToken!,
            param1: noteText);
    }

    /// <summary>
    /// Appends text to a specific note section in the focused encounter note.
    /// Parameter mapping should be validated against sandbox action details.
    /// </summary>
    public async Task<DataSet> SaveEncounterNoteSectionAsync(
        UnitySession session,
        string patientId,
        string sectionName,
        string sectionText)
    {
        session.RequireAuthenticated();
        return await _client.MagicAsync(
            action: "SaveEncounterNote",
            ehrUsername: session.EhrUsername,
            appName: _config.AppName,
            patientId: patientId,
            token: session.SecurityToken!,
            param1: sectionText,
            param2: sectionName);
    }

    /// <summary>
    /// Saves standard note sections to the focused encounter note in a stable order.
    /// Sections with empty text are skipped.
    /// </summary>
    public async Task<IReadOnlyList<DataSet>> SaveStandardEncounterNoteSectionsAsync(
        UnitySession session,
        string patientId,
        EncounterNoteSections sections)
    {
        if (sections is null)
            throw new ArgumentNullException(nameof(sections));

        var results = new List<DataSet>();

        async Task SaveIfPresentAsync(string sectionName, string sectionText)
        {
            if (string.IsNullOrWhiteSpace(sectionText))
                return;

            var result = await SaveEncounterNoteSectionAsync(session, patientId, sectionName, sectionText);
            results.Add(result);
        }

        await SaveIfPresentAsync("Chief Complaint", sections.ChiefComplaint);
        await SaveIfPresentAsync("HPI", sections.Hpi);
        await SaveIfPresentAsync("ROS", sections.Ros);
        await SaveIfPresentAsync("Physical Exam", sections.PhysicalExam);
        await SaveIfPresentAsync("Assessment", sections.Assessment);
        await SaveIfPresentAsync("Plan", sections.Plan);
        await SaveIfPresentAsync("Vitals", sections.Vitals);

        return results;
    }

}