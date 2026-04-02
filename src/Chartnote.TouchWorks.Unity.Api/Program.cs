using Chartnote.TouchWorks.Unity.Api.Actions;
using Chartnote.TouchWorks.Unity.Api.Auth;
using Chartnote.TouchWorks.Unity.Api.Config;
using Chartnote.TouchWorks.Unity.Api.Services;
using Microsoft.Extensions.Configuration;

// ─── Configuration (app-level, shared) ───────────────────────────────────────

var config = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false)
    .Build();

var unityConfig = config.GetSection(UnityConfig.SectionName).Get<UnityConfig>()
    ?? throw new InvalidOperationException("TouchWorksUnity configuration section is missing.");

if (string.IsNullOrWhiteSpace(unityConfig.UnityEndpointUrl))
    throw new InvalidOperationException("UnityEndpointUrl is not set in appsettings.json.");

// ─── Services (stateless singletons) ─────────────────────────────────────────

var httpClient = new UnityHttpClient(unityConfig);
var tokenService = new UnityTokenService(httpClient, unityConfig);
var admin = new AdminActions(httpClient, unityConfig);
var patient = new PatientActions(httpClient, unityConfig);

// ─── Per-physician session (one per physician, never shared) ──────────────────

var ehrUsername = Environment.GetEnvironmentVariable("TW_EHR_USERNAME")
    ?? throw new InvalidOperationException("Set TW_EHR_USERNAME environment variable for testing.");
var ehrPassword = Environment.GetEnvironmentVariable("TW_EHR_PASSWORD")
    ?? throw new InvalidOperationException("Set TW_EHR_PASSWORD environment variable for testing.");

var session = new UnitySession(ehrUsername, ehrPassword);

// ─── Hello World Test Sequence ────────────────────────────────────────────────

Console.WriteLine("=== Chartnote.TouchWorks.Unity — Hello World ===");
Console.WriteLine();

try
{
    // Step 1 — Echo (no credentials needed)
    Console.WriteLine("[1/5] Echo...");
    var echoResult = await admin.EchoAsync("Chartnote-TouchWorks-Test");
    Console.WriteLine($"      Echo OK — tables returned: {echoResult.Tables.Count}");

    // Step 2 — GetSecurityToken (app-level)
    Console.WriteLine("[2/5] GetSecurityToken...");
    await tokenService.GetSecurityTokenAsync(session);
    Console.WriteLine($"      Token obtained.");

    // Step 3 — GetUserAuthentication (physician-level)
    Console.WriteLine("[3/5] GetUserAuthentication...");
    await tokenService.AuthenticatePhysicianAsync(session);
    Console.WriteLine($"      Physician authenticated at {session.AuthenticatedAt:u}");

    // Step 4 — GetServerInfo
    Console.WriteLine("[4/5] GetServerInfo...");
    var serverInfo = await admin.GetServerInfoAsync(session);
    Console.WriteLine($"      Server info OK — tables returned: {serverInfo.Tables.Count}");

    // Step 5 — GetPatientList
    Console.WriteLine("[5/5] GetPatientList...");
    var patientList = await patient.GetPatientListAsync(session);
    Console.WriteLine($"      Patient list OK — tables returned: {patientList.Tables.Count}");

    Console.WriteLine();
    Console.WriteLine("=== All steps completed successfully ===");

    // Always retire the token at session end
    await tokenService.RetireAsync(session);
}
catch (NotImplementedException ex)
{
    Console.WriteLine();
    Console.WriteLine($"[BLOCKED] {ex.Message}");
    Console.WriteLine("Add the TouchWorks Unity service reference (WSDL) to proceed.");
}
catch (Exception ex)
{
    Console.WriteLine();
    Console.WriteLine($"[ERROR] {ex.GetType().Name}: {ex.Message}");
}