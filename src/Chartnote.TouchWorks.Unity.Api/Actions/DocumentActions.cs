using System.Data;
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
}