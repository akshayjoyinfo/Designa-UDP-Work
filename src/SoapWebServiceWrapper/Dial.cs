using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoapWebServiceWrapper
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.8.3928.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name = "DialSoap", Namespace = "http://dynamicverticals.com/")]
    public partial class Dial : System.Web.Services.Protocols.SoapHttpClientProtocol
    {

        private System.Threading.SendOrPostCallback CabTrackingDetailsOperationCompleted;

        private System.Threading.SendOrPostCallback SaveTransactionOperationCompleted;

        /// <remarks/>
        public Dial(string url)
        {
            this.Url = url;
        }

        /// <remarks/>
        public event CabTrackingDetailsCompletedEventHandler CabTrackingDetailsCompleted;

        /// <remarks/>
        public event SaveTransactionCompletedEventHandler SaveTransactionCompleted;

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://dynamicverticals.com/CabTrackingDetails", RequestNamespace = "http://dynamicverticals.com/", ResponseNamespace = "http://dynamicverticals.com/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string CabTrackingDetails(string strXML)
        {
            object[] results = this.Invoke("CabTrackingDetails", new object[] {
                    strXML});
            return ((string)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginCabTrackingDetails(string strXML, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("CabTrackingDetails", new object[] {
                    strXML}, callback, asyncState);
        }

        /// <remarks/>
        public string EndCabTrackingDetails(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }

        /// <remarks/>
        public void CabTrackingDetailsAsync(string strXML)
        {
            this.CabTrackingDetailsAsync(strXML, null);
        }

        /// <remarks/>
        public void CabTrackingDetailsAsync(string strXML, object userState)
        {
            if ((this.CabTrackingDetailsOperationCompleted == null))
            {
                this.CabTrackingDetailsOperationCompleted = new System.Threading.SendOrPostCallback(this.OnCabTrackingDetailsOperationCompleted);
            }
            this.InvokeAsync("CabTrackingDetails", new object[] {
                    strXML}, this.CabTrackingDetailsOperationCompleted, userState);
        }

        private void OnCabTrackingDetailsOperationCompleted(object arg)
        {
            if ((this.CabTrackingDetailsCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.CabTrackingDetailsCompleted(this, new CabTrackingDetailsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://dynamicverticals.com/SaveTransaction", RequestNamespace = "http://dynamicverticals.com/", ResponseNamespace = "http://dynamicverticals.com/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string SaveTransaction(string strXML)
        {
            object[] results = this.Invoke("SaveTransaction", new object[] {
                    strXML});
            return ((string)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginSaveTransaction(string strXML, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("SaveTransaction", new object[] {
                    strXML}, callback, asyncState);
        }

        /// <remarks/>
        public string EndSaveTransaction(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }

        /// <remarks/>
        public void SaveTransactionAsync(string strXML)
        {
            this.SaveTransactionAsync(strXML, null);
        }

        /// <remarks/>
        public void SaveTransactionAsync(string strXML, object userState)
        {
            if ((this.SaveTransactionOperationCompleted == null))
            {
                this.SaveTransactionOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSaveTransactionOperationCompleted);
            }
            this.InvokeAsync("SaveTransaction", new object[] {
                    strXML}, this.SaveTransactionOperationCompleted, userState);
        }

        private void OnSaveTransactionOperationCompleted(object arg)
        {
            if ((this.SaveTransactionCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SaveTransactionCompleted(this, new SaveTransactionCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        public new void CancelAsync(object userState)
        {
            base.CancelAsync(userState);
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.8.3928.0")]
    public delegate void CabTrackingDetailsCompletedEventHandler(object sender, CabTrackingDetailsCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.8.3928.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class CabTrackingDetailsCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal CabTrackingDetailsCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public string Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.8.3928.0")]
    public delegate void SaveTransactionCompletedEventHandler(object sender, SaveTransactionCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.8.3928.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class SaveTransactionCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal SaveTransactionCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public string Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
}
