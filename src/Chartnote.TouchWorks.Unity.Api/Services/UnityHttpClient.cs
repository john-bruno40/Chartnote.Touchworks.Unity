using System.Data;
using System.ServiceModel;
using Chartnote.TouchWorks.Unity.Api.Config;

namespace Chartnote.TouchWorks.Unity.Api.Services;

/// <summary>
/// Hand-crafted WCF/SOAP client for the Altera TouchWorks Unity API.
/// No svcutil generated proxies — matches the Chartnote.Altera.Unity approach.
/// </summary>
public class UnityHttpClient : IUnityClient
{
    private readonly UnityConfig _config;
    private readonly BasicHttpBinding _binding;
    private readonly EndpointAddress _endpoint;

    public UnityHttpClient(UnityConfig config)
    {
        _config = config;

        _binding = new BasicHttpBinding
        {
            MaxReceivedMessageSize = 10 * 1024 * 1024, // 10 MB
            SendTimeout = TimeSpan.FromSeconds(30),
            ReceiveTimeout = TimeSpan.FromSeconds(30),
            Security = new BasicHttpSecurity { Mode = BasicHttpSecurityMode.None }
        };

        _endpoint = new EndpointAddress(_config.UnityEndpointUrl);
    }

    public async Task<string> GetSecurityTokenAsync(string serviceUsername, string servicePassword)
    {
        // TODO: Replace with actual SOAP call once WSDL is available
        // unity.GetSecurityToken(serviceUsername, servicePassword)
        throw new NotImplementedException(
            "Awaiting TouchWorks Unity WSDL. " +
            "Replace with: unity.GetSecurityToken(serviceUsername, servicePassword)");
    }

    public async Task<DataSet> MagicAsync(
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
        DataSet? data = null)
    {
        // TODO: Replace with actual SOAP call once WSDL is available
        // unity.Magic(action, ehrUsername, appName, patientId, token,
        //             param1, param2, param3, param4, param5, param6, data)
        throw new NotImplementedException(
            "Awaiting TouchWorks Unity WSDL. " +
            "Replace with: unity.Magic(action, ...)");
    }

    public async Task RetireSecurityTokenAsync(string token, string appName)
    {
        // TODO: Replace with actual SOAP call once WSDL is available
        // unity.RetireSecurityToken(token, appName)
        throw new NotImplementedException(
            "Awaiting TouchWorks Unity WSDL. " +
            "Replace with: unity.RetireSecurityToken(token, appName)");
    }

}