using System.Data;

namespace Chartnote.TouchWorks.Unity.Api.Services;

/// <summary>
/// Contract for the Altera TouchWorks Unity SOAP client.
/// All clinical calls route through the Magic method.
/// </summary>
public interface IUnityClient
{
    /// <summary>
    /// Obtain an app-level security token using service account credentials.
    /// </summary>
    Task<string> GetSecurityTokenAsync(string serviceUsername, string servicePassword);

    /// <summary>
    /// Execute any Unity Magic action.
    /// </summary>
    Task<DataSet> MagicAsync(
        string action,
        string ehrUsername,
        string appName,
        string patientId,
        string token,
        string param1 = "",
        string param2 = "",
        string param3 = "",
        string param4 = "",
        string param5 = "",
        string param6 = "",
        DataSet? data = null);

    /// <summary>
    /// Retires the security token at the end of a session.
    /// Always call this on session end to clean up server-side resources.
    /// </summary>
    Task RetireSecurityTokenAsync(string token, string appName);

}