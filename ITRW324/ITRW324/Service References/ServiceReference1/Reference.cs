﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ITRW324.ServiceReference1 {
    using System.Data;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.IWebService")]
    public interface IWebService {
        
        // CODEGEN: Parameter 'InsertResult' requires additional schema information that cannot be captured using the parameter mode. The specific attribute is 'System.Xml.Serialization.XmlElementAttribute'.
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWebService/Insert", ReplyAction="http://tempuri.org/IWebService/InsertResponse")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        ITRW324.ServiceReference1.InsertResponse Insert(ITRW324.ServiceReference1.InsertRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWebService/Insert", ReplyAction="http://tempuri.org/IWebService/InsertResponse")]
        System.Threading.Tasks.Task<ITRW324.ServiceReference1.InsertResponse> InsertAsync(ITRW324.ServiceReference1.InsertRequest request);
        
        // CODEGEN: Parameter 'DisplayResult' requires additional schema information that cannot be captured using the parameter mode. The specific attribute is 'System.Xml.Serialization.XmlElementAttribute'.
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWebService/Display", ReplyAction="http://tempuri.org/IWebService/DisplayResponse")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        ITRW324.ServiceReference1.DisplayResponse Display(ITRW324.ServiceReference1.DisplayRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWebService/Display", ReplyAction="http://tempuri.org/IWebService/DisplayResponse")]
        System.Threading.Tasks.Task<ITRW324.ServiceReference1.DisplayResponse> DisplayAsync(ITRW324.ServiceReference1.DisplayRequest request);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.datacontract.org/2004/07/ITRW324")]
    public partial class fileData : object, System.ComponentModel.INotifyPropertyChanged {
        
        private byte[] dataField;
        
        private string dateField;
        
        private string hashField;
        
        private string nameField;
        
        private string typeField;
        
        private int useridField;
        
        private bool useridFieldSpecified;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary", IsNullable=true, Order=0)]
        public byte[] Data {
            get {
                return this.dataField;
            }
            set {
                this.dataField = value;
                this.RaisePropertyChanged("Data");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=1)]
        public string Date {
            get {
                return this.dateField;
            }
            set {
                this.dateField = value;
                this.RaisePropertyChanged("Date");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=2)]
        public string Hash {
            get {
                return this.hashField;
            }
            set {
                this.hashField = value;
                this.RaisePropertyChanged("Hash");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=3)]
        public string Name {
            get {
                return this.nameField;
            }
            set {
                this.nameField = value;
                this.RaisePropertyChanged("Name");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=4)]
        public string Type {
            get {
                return this.typeField;
            }
            set {
                this.typeField = value;
                this.RaisePropertyChanged("Type");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=5)]
        public int Userid {
            get {
                return this.useridField;
            }
            set {
                this.useridField = value;
                this.RaisePropertyChanged("Userid");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool UseridSpecified {
            get {
                return this.useridFieldSpecified;
            }
            set {
                this.useridFieldSpecified = value;
                this.RaisePropertyChanged("UseridSpecified");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="Insert", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class InsertRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public ITRW324.ServiceReference1.fileData data;
        
        public InsertRequest() {
        }
        
        public InsertRequest(ITRW324.ServiceReference1.fileData data) {
            this.data = data;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="InsertResponse", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class InsertResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string InsertResult;
        
        public InsertResponse() {
        }
        
        public InsertResponse(string InsertResult) {
            this.InsertResult = InsertResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="Display", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class DisplayRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public ITRW324.ServiceReference1.fileData data;
        
        public DisplayRequest() {
        }
        
        public DisplayRequest(ITRW324.ServiceReference1.fileData data) {
            this.data = data;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="DisplayResponse", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class DisplayResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public System.Data.DataSet DisplayResult;
        
        public DisplayResponse() {
        }
        
        public DisplayResponse(System.Data.DataSet DisplayResult) {
            this.DisplayResult = DisplayResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IWebServiceChannel : ITRW324.ServiceReference1.IWebService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class WebServiceClient : System.ServiceModel.ClientBase<ITRW324.ServiceReference1.IWebService>, ITRW324.ServiceReference1.IWebService {
        
        public WebServiceClient() {
        }
        
        public WebServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public WebServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WebServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WebServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        ITRW324.ServiceReference1.InsertResponse ITRW324.ServiceReference1.IWebService.Insert(ITRW324.ServiceReference1.InsertRequest request) {
            return base.Channel.Insert(request);
        }
        
        public string Insert(ITRW324.ServiceReference1.fileData data) {
            ITRW324.ServiceReference1.InsertRequest inValue = new ITRW324.ServiceReference1.InsertRequest();
            inValue.data = data;
            ITRW324.ServiceReference1.InsertResponse retVal = ((ITRW324.ServiceReference1.IWebService)(this)).Insert(inValue);
            return retVal.InsertResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<ITRW324.ServiceReference1.InsertResponse> ITRW324.ServiceReference1.IWebService.InsertAsync(ITRW324.ServiceReference1.InsertRequest request) {
            return base.Channel.InsertAsync(request);
        }
        
        public System.Threading.Tasks.Task<ITRW324.ServiceReference1.InsertResponse> InsertAsync(ITRW324.ServiceReference1.fileData data) {
            ITRW324.ServiceReference1.InsertRequest inValue = new ITRW324.ServiceReference1.InsertRequest();
            inValue.data = data;
            return ((ITRW324.ServiceReference1.IWebService)(this)).InsertAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        ITRW324.ServiceReference1.DisplayResponse ITRW324.ServiceReference1.IWebService.Display(ITRW324.ServiceReference1.DisplayRequest request) {
            return base.Channel.Display(request);
        }
        
        public System.Data.DataSet Display(ITRW324.ServiceReference1.fileData data) {
            ITRW324.ServiceReference1.DisplayRequest inValue = new ITRW324.ServiceReference1.DisplayRequest();
            inValue.data = data;
            ITRW324.ServiceReference1.DisplayResponse retVal = ((ITRW324.ServiceReference1.IWebService)(this)).Display(inValue);
            return retVal.DisplayResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<ITRW324.ServiceReference1.DisplayResponse> ITRW324.ServiceReference1.IWebService.DisplayAsync(ITRW324.ServiceReference1.DisplayRequest request) {
            return base.Channel.DisplayAsync(request);
        }
        
        public System.Threading.Tasks.Task<ITRW324.ServiceReference1.DisplayResponse> DisplayAsync(ITRW324.ServiceReference1.fileData data) {
            ITRW324.ServiceReference1.DisplayRequest inValue = new ITRW324.ServiceReference1.DisplayRequest();
            inValue.data = data;
            return ((ITRW324.ServiceReference1.IWebService)(this)).DisplayAsync(inValue);
        }
    }
}
