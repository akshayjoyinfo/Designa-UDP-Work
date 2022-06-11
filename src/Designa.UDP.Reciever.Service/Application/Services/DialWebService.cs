using System;
using System.Collections.Generic;
using System.Text;



[System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.0")]
[System.ServiceModel.ServiceContractAttribute(Namespace = "http://dynamicverticals.com/", ConfigurationName = "DialSoap")]
public interface DialSoap
{

    [System.ServiceModel.OperationContractAttribute(Action = "http://dynamicverticals.com/CabTrackingDetails", ReplyAction = "*")]
    [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults = true)]
    System.Threading.Tasks.Task<string> CabTrackingDetailsAsync(string strXML);

    [System.ServiceModel.OperationContractAttribute(Action = "http://dynamicverticals.com/SaveTransaction", ReplyAction = "*")]
    [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults = true)]
    System.Threading.Tasks.Task<string> SaveTransactionAsync(string strXML);
}

[System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.0")]
public interface DialSoapChannel : DialSoap, System.ServiceModel.IClientChannel
{
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.0")]
public partial class DialSoapClient : System.ServiceModel.ClientBase<DialSoap>, DialSoap
{

    /// <summary>
    /// Implement this partial method to configure the service endpoint.
    /// </summary>
    /// <param name="serviceEndpoint">The endpoint to configure</param>
    /// <param name="clientCredentials">The client credentials</param>
    static partial void ConfigureEndpoint(System.ServiceModel.Description.ServiceEndpoint serviceEndpoint, System.ServiceModel.Description.ClientCredentials clientCredentials);

    public DialSoapClient(EndpointConfiguration endpointConfiguration) :
            base(DialSoapClient.GetBindingForEndpoint(endpointConfiguration), DialSoapClient.GetEndpointAddress(endpointConfiguration))
    {
        this.Endpoint.Name = endpointConfiguration.ToString();
        ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
    }

    public DialSoapClient(EndpointConfiguration endpointConfiguration, string remoteAddress) :
            base(DialSoapClient.GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress))
    {
        this.Endpoint.Name = endpointConfiguration.ToString();
        ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
    }

    public DialSoapClient(EndpointConfiguration endpointConfiguration, System.ServiceModel.EndpointAddress remoteAddress) :
            base(DialSoapClient.GetBindingForEndpoint(endpointConfiguration), remoteAddress)
    {
        this.Endpoint.Name = endpointConfiguration.ToString();
        ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
    }

    public DialSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
            base(binding, remoteAddress)
    {
    }

    public System.Threading.Tasks.Task<string> CabTrackingDetailsAsync(string strXML)
    {
        return base.Channel.CabTrackingDetailsAsync(strXML);
    }

    public System.Threading.Tasks.Task<string> SaveTransactionAsync(string strXML)
    {
        return base.Channel.SaveTransactionAsync(strXML);
    }

    public virtual System.Threading.Tasks.Task OpenAsync()
    {
        return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndOpen));
    }

    public virtual System.Threading.Tasks.Task CloseAsync()
    {
        return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginClose(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndClose));
    }

    private static System.ServiceModel.Channels.Binding GetBindingForEndpoint(EndpointConfiguration endpointConfiguration)
    {
        if ((endpointConfiguration == EndpointConfiguration.DialSoap))
        {
            System.ServiceModel.BasicHttpBinding result = new System.ServiceModel.BasicHttpBinding();
            result.MaxBufferSize = int.MaxValue;
            result.ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max;
            result.MaxReceivedMessageSize = int.MaxValue;
            result.AllowCookies = true;
            return result;
        }
        if ((endpointConfiguration == EndpointConfiguration.DialSoap12))
        {
            System.ServiceModel.Channels.CustomBinding result = new System.ServiceModel.Channels.CustomBinding();
            System.ServiceModel.Channels.TextMessageEncodingBindingElement textBindingElement = new System.ServiceModel.Channels.TextMessageEncodingBindingElement();
            textBindingElement.MessageVersion = System.ServiceModel.Channels.MessageVersion.CreateVersion(System.ServiceModel.EnvelopeVersion.Soap12, System.ServiceModel.Channels.AddressingVersion.None);
            result.Elements.Add(textBindingElement);
            System.ServiceModel.Channels.HttpTransportBindingElement httpBindingElement = new System.ServiceModel.Channels.HttpTransportBindingElement();
            httpBindingElement.AllowCookies = true;
            httpBindingElement.MaxBufferSize = int.MaxValue;
            httpBindingElement.MaxReceivedMessageSize = int.MaxValue;
            result.Elements.Add(httpBindingElement);
            return result;
        }
        throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
    }

    private static System.ServiceModel.EndpointAddress GetEndpointAddress(EndpointConfiguration endpointConfiguration)
    {
        if ((endpointConfiguration == EndpointConfiguration.DialSoap))
        {
            return new System.ServiceModel.EndpointAddress("http://10.248.16.35/DialServiceSequence/Dial.asmx");
        }
        if ((endpointConfiguration == EndpointConfiguration.DialSoap12))
        {
            return new System.ServiceModel.EndpointAddress("http://10.248.16.35/DialServiceSequence/Dial.asmx");
        }
        throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
    }

    public enum EndpointConfiguration
    {

        DialSoap,

        DialSoap12,
    }
}
