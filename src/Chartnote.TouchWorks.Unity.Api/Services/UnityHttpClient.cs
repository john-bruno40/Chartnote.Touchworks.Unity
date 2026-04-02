using System.Data;
using System.ServiceModel;
using Chartnote.TouchWorks.Unity.Api.Config;
using Chartnote.TouchWorks.Unity.Api.Unity;

namespace Chartnote.TouchWorks.Unity.Api.Services;

/// <summary>
/// WCF/SOAP client for the Altera TouchWorks Unity API.
/// Uses the UnityServiceClient proxy from ServiceReference.
/// </summary>
public class UnityHttpClient : IUnityClient
{
    private readonly UnityConfig _config;
    private readonly UnityServiceClient _client;

    public UnityHttpClient(UnityConfig config)
    {
        _config = config;

        var binding = new BasicHttpBinding
        {
            MaxReceivedMessageSize = 10 * 1024 * 1024, // 10 MB
            SendTimeout = TimeSpan.FromSeconds(30),
            ReceiveTimeout = TimeSpan.FromSeconds(30),
            Security = new BasicHttpSecurity
            {
                Mode = BasicHttpSecurityMode.Transport // HTTPS
            }
        };

        var endpoint = new EndpointAddress(_config.UnityEndpointUrl);
        _client = new UnityServiceClient(binding, endpoint);
    }

    public async Task<string> GetSecurityTokenAsync(string serviceUsername, string servicePassword)
    {
        return await Task.Run(() =>
            _client.GetSecurityToken(serviceUsername, servicePassword));
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
        byte[]? data = null)
    {
        return await Task.Run(() =>
            _client.Magic(action, ehrUsername, appName, patientId, token,
                param1, param2, param3, param4, param5, param6,
                data ?? Array.Empty<byte>()));
    }

    public async Task RetireSecurityTokenAsync(string token, string appName)
    {
        await Task.Run(() =>
            _client.RetireSecurityToken(token, appName));
    }
}