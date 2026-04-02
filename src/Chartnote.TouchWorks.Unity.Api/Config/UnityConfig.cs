namespace Chartnote.TouchWorks.Unity.Api.Config;

/// <summary>
/// App-level configuration — shared singleton across all physician sessions.
/// Does NOT contain per-physician credentials.
/// </summary>
public class UnityConfig
{
    public const string SectionName = "TouchWorksUnity";

    public string UnityEndpointUrl { get; set; } = string.Empty;
    public string AppName { get; set; } = string.Empty;

    // Service account credentials (app-level auth, not per-physician)
    public string ServiceUsername { get; set; } = string.Empty;
    public string ServicePassword { get; set; } = string.Empty;
}