namespace Chartnote.TouchWorks.Unity.Api.Actions;

/// <summary>
/// Standard Chartnote note sections written to a focused encounter note.
/// Empty/null sections are skipped when saving.
/// </summary>
public class EncounterNoteSections
{
    public string ChiefComplaint { get; set; } = string.Empty;
    public string Hpi { get; set; } = string.Empty;
    public string Ros { get; set; } = string.Empty;
    public string PhysicalExam { get; set; } = string.Empty;
    public string Assessment { get; set; } = string.Empty;
    public string Plan { get; set; } = string.Empty;
    public string Vitals { get; set; } = string.Empty;
}
