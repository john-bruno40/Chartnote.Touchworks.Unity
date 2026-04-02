using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace Chartnote.TouchWorks.Unity.Api.Unity
{
    [DataContract(Name = "UnityFault", 
        Namespace = "http://schemas.datacontract.org/2004/07/Allscripts.UAI.Unity.UnityService")]
    public partial class UnityFault
    {
        [DataMember]
        public string ErrorMessage { get; set; } = string.Empty;
    }

    [ServiceContract(Namespace = "http://www.allscripts.com/Unity", 
        ConfigurationName = "Unity.IUnityService")]
    public interface IUnityService
    {
        [OperationContract(
            Action = "http://www.allscripts.com/Unity/IUnityService/Magic",
            ReplyAction = "http://www.allscripts.com/Unity/IUnityService/MagicResponse")]
        [FaultContract(typeof(UnityFault),
            Action = "http://www.allscripts.com/Unity/IUnityService/MagicUnityFaultFault",
            Name = "UnityFault",
            Namespace = "http://schemas.datacontract.org/2004/07/Allscripts.UAI.Unity.UnityService")]
        System.Data.DataSet Magic(
            string Action, string UserID, string Appname, string PatientID, string Token,
            string Parameter1, string Parameter2, string Parameter3,
            string Parameter4, string Parameter5, string Parameter6,
            byte[] data);

        [OperationContract(
            Action = "http://www.allscripts.com/Unity/IUnityService/GetSecurityToken",
            ReplyAction = "http://www.allscripts.com/Unity/IUnityService/GetSecurityTokenResponse")]
        [FaultContract(typeof(UnityFault),
            Action = "http://www.allscripts.com/Unity/IUnityService/GetSecurityTokenUnityFaultFault",
            Name = "UnityFault",
            Namespace = "http://schemas.datacontract.org/2004/07/Allscripts.UAI.Unity.UnityService")]
        string GetSecurityToken(string Username, string Password);

        [OperationContract(
            Action = "http://www.allscripts.com/Unity/IUnityService/GetValidSecurityToken",
            ReplyAction = "http://www.allscripts.com/Unity/IUnityService/GetValidSecurityTokenResponse")]
        string GetValidSecurityToken(string Username, string Password, string originalToken);

        [OperationContract(
            Action = "http://www.allscripts.com/Unity/IUnityService/RetireSecurityToken",
            ReplyAction = "http://www.allscripts.com/Unity/IUnityService/RetireSecurityTokenResponse")]
        void RetireSecurityToken(string Token, string Appname);
    }

    public partial class UnityServiceClient :
        ClientBase<IUnityService>, IUnityService
    {
        public UnityServiceClient(Binding binding, EndpointAddress remoteAddress)
            : base(binding, remoteAddress) { }

        public System.Data.DataSet Magic(
            string Action, string UserID, string Appname, string PatientID, string Token,
            string Parameter1, string Parameter2, string Parameter3,
            string Parameter4, string Parameter5, string Parameter6,
            byte[] data)
        {
            return Channel.Magic(Action, UserID, Appname, PatientID, Token,
                Parameter1, Parameter2, Parameter3, Parameter4, Parameter5, Parameter6, data);
        }

        public string GetSecurityToken(string Username, string Password)
        {
            return Channel.GetSecurityToken(Username, Password);
        }

        public string GetValidSecurityToken(string Username, string Password, string originalToken)
        {
            return Channel.GetValidSecurityToken(Username, Password, originalToken);
        }

        public void RetireSecurityToken(string Token, string Appname)
        {
            Channel.RetireSecurityToken(Token, Appname);
        }
    }
}