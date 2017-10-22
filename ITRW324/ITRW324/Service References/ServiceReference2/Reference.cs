﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ITRW324.ServiceReference2 {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference2.ServiceSoap")]
    public interface ServiceSoap {
        
        // CODEGEN: Generating message contract since element name sName from namespace http://tempuri.org/ is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Insert", ReplyAction="*")]
        ITRW324.ServiceReference2.InsertResponse Insert(ITRW324.ServiceReference2.InsertRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Insert", ReplyAction="*")]
        System.Threading.Tasks.Task<ITRW324.ServiceReference2.InsertResponse> InsertAsync(ITRW324.ServiceReference2.InsertRequest request);
        
        // CODEGEN: Generating message contract since element name sName from namespace http://tempuri.org/ is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/CreateUser", ReplyAction="*")]
        ITRW324.ServiceReference2.CreateUserResponse CreateUser(ITRW324.ServiceReference2.CreateUserRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/CreateUser", ReplyAction="*")]
        System.Threading.Tasks.Task<ITRW324.ServiceReference2.CreateUserResponse> CreateUserAsync(ITRW324.ServiceReference2.CreateUserRequest request);
        
        // CODEGEN: Generating message contract since element name sHash from namespace http://tempuri.org/ is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/AddToBlockChain", ReplyAction="*")]
        ITRW324.ServiceReference2.AddToBlockChainResponse AddToBlockChain(ITRW324.ServiceReference2.AddToBlockChainRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/AddToBlockChain", ReplyAction="*")]
        System.Threading.Tasks.Task<ITRW324.ServiceReference2.AddToBlockChainResponse> AddToBlockChainAsync(ITRW324.ServiceReference2.AddToBlockChainRequest request);
        
        // CODEGEN: Generating message contract since element name sHash from namespace http://tempuri.org/ is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Validate", ReplyAction="*")]
        ITRW324.ServiceReference2.ValidateResponse Validate(ITRW324.ServiceReference2.ValidateRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Validate", ReplyAction="*")]
        System.Threading.Tasks.Task<ITRW324.ServiceReference2.ValidateResponse> ValidateAsync(ITRW324.ServiceReference2.ValidateRequest request);
        
        // CODEGEN: Generating message contract since element name sHash from namespace http://tempuri.org/ is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetInfoOfDocument", ReplyAction="*")]
        ITRW324.ServiceReference2.GetInfoOfDocumentResponse GetInfoOfDocument(ITRW324.ServiceReference2.GetInfoOfDocumentRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetInfoOfDocument", ReplyAction="*")]
        System.Threading.Tasks.Task<ITRW324.ServiceReference2.GetInfoOfDocumentResponse> GetInfoOfDocumentAsync(ITRW324.ServiceReference2.GetInfoOfDocumentRequest request);
        
        // CODEGEN: Generating message contract since element name GetBlockChainResult from namespace http://tempuri.org/ is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetBlockChain", ReplyAction="*")]
        ITRW324.ServiceReference2.GetBlockChainResponse GetBlockChain(ITRW324.ServiceReference2.GetBlockChainRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetBlockChain", ReplyAction="*")]
        System.Threading.Tasks.Task<ITRW324.ServiceReference2.GetBlockChainResponse> GetBlockChainAsync(ITRW324.ServiceReference2.GetBlockChainRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class InsertRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="Insert", Namespace="http://tempuri.org/", Order=0)]
        public ITRW324.ServiceReference2.InsertRequestBody Body;
        
        public InsertRequest() {
        }
        
        public InsertRequest(ITRW324.ServiceReference2.InsertRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class InsertRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string sName;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string sType;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string sCreationDate;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string sHash;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=4)]
        public byte[] bData;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=5)]
        public int iUserID;
        
        public InsertRequestBody() {
        }
        
        public InsertRequestBody(string sName, string sType, string sCreationDate, string sHash, byte[] bData, int iUserID) {
            this.sName = sName;
            this.sType = sType;
            this.sCreationDate = sCreationDate;
            this.sHash = sHash;
            this.bData = bData;
            this.iUserID = iUserID;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class InsertResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="InsertResponse", Namespace="http://tempuri.org/", Order=0)]
        public ITRW324.ServiceReference2.InsertResponseBody Body;
        
        public InsertResponse() {
        }
        
        public InsertResponse(ITRW324.ServiceReference2.InsertResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class InsertResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string InsertResult;
        
        public InsertResponseBody() {
        }
        
        public InsertResponseBody(string InsertResult) {
            this.InsertResult = InsertResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class CreateUserRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="CreateUser", Namespace="http://tempuri.org/", Order=0)]
        public ITRW324.ServiceReference2.CreateUserRequestBody Body;
        
        public CreateUserRequest() {
        }
        
        public CreateUserRequest(ITRW324.ServiceReference2.CreateUserRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class CreateUserRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string sName;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string sPassword;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string sEmail;
        
        public CreateUserRequestBody() {
        }
        
        public CreateUserRequestBody(string sName, string sPassword, string sEmail) {
            this.sName = sName;
            this.sPassword = sPassword;
            this.sEmail = sEmail;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class CreateUserResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="CreateUserResponse", Namespace="http://tempuri.org/", Order=0)]
        public ITRW324.ServiceReference2.CreateUserResponseBody Body;
        
        public CreateUserResponse() {
        }
        
        public CreateUserResponse(ITRW324.ServiceReference2.CreateUserResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class CreateUserResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string CreateUserResult;
        
        public CreateUserResponseBody() {
        }
        
        public CreateUserResponseBody(string CreateUserResult) {
            this.CreateUserResult = CreateUserResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class AddToBlockChainRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="AddToBlockChain", Namespace="http://tempuri.org/", Order=0)]
        public ITRW324.ServiceReference2.AddToBlockChainRequestBody Body;
        
        public AddToBlockChainRequest() {
        }
        
        public AddToBlockChainRequest(ITRW324.ServiceReference2.AddToBlockChainRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class AddToBlockChainRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string sHash;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string sUserID;
        
        public AddToBlockChainRequestBody() {
        }
        
        public AddToBlockChainRequestBody(string sHash, string sUserID) {
            this.sHash = sHash;
            this.sUserID = sUserID;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class AddToBlockChainResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="AddToBlockChainResponse", Namespace="http://tempuri.org/", Order=0)]
        public ITRW324.ServiceReference2.AddToBlockChainResponseBody Body;
        
        public AddToBlockChainResponse() {
        }
        
        public AddToBlockChainResponse(ITRW324.ServiceReference2.AddToBlockChainResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class AddToBlockChainResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=0)]
        public bool AddToBlockChainResult;
        
        public AddToBlockChainResponseBody() {
        }
        
        public AddToBlockChainResponseBody(bool AddToBlockChainResult) {
            this.AddToBlockChainResult = AddToBlockChainResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class ValidateRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="Validate", Namespace="http://tempuri.org/", Order=0)]
        public ITRW324.ServiceReference2.ValidateRequestBody Body;
        
        public ValidateRequest() {
        }
        
        public ValidateRequest(ITRW324.ServiceReference2.ValidateRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class ValidateRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string sHash;
        
        public ValidateRequestBody() {
        }
        
        public ValidateRequestBody(string sHash) {
            this.sHash = sHash;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class ValidateResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="ValidateResponse", Namespace="http://tempuri.org/", Order=0)]
        public ITRW324.ServiceReference2.ValidateResponseBody Body;
        
        public ValidateResponse() {
        }
        
        public ValidateResponse(ITRW324.ServiceReference2.ValidateResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class ValidateResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=0)]
        public bool ValidateResult;
        
        public ValidateResponseBody() {
        }
        
        public ValidateResponseBody(bool ValidateResult) {
            this.ValidateResult = ValidateResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetInfoOfDocumentRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetInfoOfDocument", Namespace="http://tempuri.org/", Order=0)]
        public ITRW324.ServiceReference2.GetInfoOfDocumentRequestBody Body;
        
        public GetInfoOfDocumentRequest() {
        }
        
        public GetInfoOfDocumentRequest(ITRW324.ServiceReference2.GetInfoOfDocumentRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class GetInfoOfDocumentRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string sHash;
        
        public GetInfoOfDocumentRequestBody() {
        }
        
        public GetInfoOfDocumentRequestBody(string sHash) {
            this.sHash = sHash;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetInfoOfDocumentResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetInfoOfDocumentResponse", Namespace="http://tempuri.org/", Order=0)]
        public ITRW324.ServiceReference2.GetInfoOfDocumentResponseBody Body;
        
        public GetInfoOfDocumentResponse() {
        }
        
        public GetInfoOfDocumentResponse(ITRW324.ServiceReference2.GetInfoOfDocumentResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class GetInfoOfDocumentResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public byte[] GetInfoOfDocumentResult;
        
        public GetInfoOfDocumentResponseBody() {
        }
        
        public GetInfoOfDocumentResponseBody(byte[] GetInfoOfDocumentResult) {
            this.GetInfoOfDocumentResult = GetInfoOfDocumentResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetBlockChainRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetBlockChain", Namespace="http://tempuri.org/", Order=0)]
        public ITRW324.ServiceReference2.GetBlockChainRequestBody Body;
        
        public GetBlockChainRequest() {
        }
        
        public GetBlockChainRequest(ITRW324.ServiceReference2.GetBlockChainRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute()]
    public partial class GetBlockChainRequestBody {
        
        public GetBlockChainRequestBody() {
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetBlockChainResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetBlockChainResponse", Namespace="http://tempuri.org/", Order=0)]
        public ITRW324.ServiceReference2.GetBlockChainResponseBody Body;
        
        public GetBlockChainResponse() {
        }
        
        public GetBlockChainResponse(ITRW324.ServiceReference2.GetBlockChainResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class GetBlockChainResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public byte[] GetBlockChainResult;
        
        public GetBlockChainResponseBody() {
        }
        
        public GetBlockChainResponseBody(byte[] GetBlockChainResult) {
            this.GetBlockChainResult = GetBlockChainResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ServiceSoapChannel : ITRW324.ServiceReference2.ServiceSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ServiceSoapClient : System.ServiceModel.ClientBase<ITRW324.ServiceReference2.ServiceSoap>, ITRW324.ServiceReference2.ServiceSoap {
        
        public ServiceSoapClient() {
        }
        
        public ServiceSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ServiceSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        ITRW324.ServiceReference2.InsertResponse ITRW324.ServiceReference2.ServiceSoap.Insert(ITRW324.ServiceReference2.InsertRequest request) {
            return base.Channel.Insert(request);
        }
        
        public string Insert(string sName, string sType, string sCreationDate, string sHash, byte[] bData, int iUserID) {
            ITRW324.ServiceReference2.InsertRequest inValue = new ITRW324.ServiceReference2.InsertRequest();
            inValue.Body = new ITRW324.ServiceReference2.InsertRequestBody();
            inValue.Body.sName = sName;
            inValue.Body.sType = sType;
            inValue.Body.sCreationDate = sCreationDate;
            inValue.Body.sHash = sHash;
            inValue.Body.bData = bData;
            inValue.Body.iUserID = iUserID;
            ITRW324.ServiceReference2.InsertResponse retVal = ((ITRW324.ServiceReference2.ServiceSoap)(this)).Insert(inValue);
            return retVal.Body.InsertResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<ITRW324.ServiceReference2.InsertResponse> ITRW324.ServiceReference2.ServiceSoap.InsertAsync(ITRW324.ServiceReference2.InsertRequest request) {
            return base.Channel.InsertAsync(request);
        }
        
        public System.Threading.Tasks.Task<ITRW324.ServiceReference2.InsertResponse> InsertAsync(string sName, string sType, string sCreationDate, string sHash, byte[] bData, int iUserID) {
            ITRW324.ServiceReference2.InsertRequest inValue = new ITRW324.ServiceReference2.InsertRequest();
            inValue.Body = new ITRW324.ServiceReference2.InsertRequestBody();
            inValue.Body.sName = sName;
            inValue.Body.sType = sType;
            inValue.Body.sCreationDate = sCreationDate;
            inValue.Body.sHash = sHash;
            inValue.Body.bData = bData;
            inValue.Body.iUserID = iUserID;
            return ((ITRW324.ServiceReference2.ServiceSoap)(this)).InsertAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        ITRW324.ServiceReference2.CreateUserResponse ITRW324.ServiceReference2.ServiceSoap.CreateUser(ITRW324.ServiceReference2.CreateUserRequest request) {
            return base.Channel.CreateUser(request);
        }
        
        public string CreateUser(string sName, string sPassword, string sEmail) {
            ITRW324.ServiceReference2.CreateUserRequest inValue = new ITRW324.ServiceReference2.CreateUserRequest();
            inValue.Body = new ITRW324.ServiceReference2.CreateUserRequestBody();
            inValue.Body.sName = sName;
            inValue.Body.sPassword = sPassword;
            inValue.Body.sEmail = sEmail;
            ITRW324.ServiceReference2.CreateUserResponse retVal = ((ITRW324.ServiceReference2.ServiceSoap)(this)).CreateUser(inValue);
            return retVal.Body.CreateUserResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<ITRW324.ServiceReference2.CreateUserResponse> ITRW324.ServiceReference2.ServiceSoap.CreateUserAsync(ITRW324.ServiceReference2.CreateUserRequest request) {
            return base.Channel.CreateUserAsync(request);
        }
        
        public System.Threading.Tasks.Task<ITRW324.ServiceReference2.CreateUserResponse> CreateUserAsync(string sName, string sPassword, string sEmail) {
            ITRW324.ServiceReference2.CreateUserRequest inValue = new ITRW324.ServiceReference2.CreateUserRequest();
            inValue.Body = new ITRW324.ServiceReference2.CreateUserRequestBody();
            inValue.Body.sName = sName;
            inValue.Body.sPassword = sPassword;
            inValue.Body.sEmail = sEmail;
            return ((ITRW324.ServiceReference2.ServiceSoap)(this)).CreateUserAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        ITRW324.ServiceReference2.AddToBlockChainResponse ITRW324.ServiceReference2.ServiceSoap.AddToBlockChain(ITRW324.ServiceReference2.AddToBlockChainRequest request) {
            return base.Channel.AddToBlockChain(request);
        }
        
        public bool AddToBlockChain(string sHash, string sUserID) {
            ITRW324.ServiceReference2.AddToBlockChainRequest inValue = new ITRW324.ServiceReference2.AddToBlockChainRequest();
            inValue.Body = new ITRW324.ServiceReference2.AddToBlockChainRequestBody();
            inValue.Body.sHash = sHash;
            inValue.Body.sUserID = sUserID;
            ITRW324.ServiceReference2.AddToBlockChainResponse retVal = ((ITRW324.ServiceReference2.ServiceSoap)(this)).AddToBlockChain(inValue);
            return retVal.Body.AddToBlockChainResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<ITRW324.ServiceReference2.AddToBlockChainResponse> ITRW324.ServiceReference2.ServiceSoap.AddToBlockChainAsync(ITRW324.ServiceReference2.AddToBlockChainRequest request) {
            return base.Channel.AddToBlockChainAsync(request);
        }
        
        public System.Threading.Tasks.Task<ITRW324.ServiceReference2.AddToBlockChainResponse> AddToBlockChainAsync(string sHash, string sUserID) {
            ITRW324.ServiceReference2.AddToBlockChainRequest inValue = new ITRW324.ServiceReference2.AddToBlockChainRequest();
            inValue.Body = new ITRW324.ServiceReference2.AddToBlockChainRequestBody();
            inValue.Body.sHash = sHash;
            inValue.Body.sUserID = sUserID;
            return ((ITRW324.ServiceReference2.ServiceSoap)(this)).AddToBlockChainAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        ITRW324.ServiceReference2.ValidateResponse ITRW324.ServiceReference2.ServiceSoap.Validate(ITRW324.ServiceReference2.ValidateRequest request) {
            return base.Channel.Validate(request);
        }
        
        public bool Validate(string sHash) {
            ITRW324.ServiceReference2.ValidateRequest inValue = new ITRW324.ServiceReference2.ValidateRequest();
            inValue.Body = new ITRW324.ServiceReference2.ValidateRequestBody();
            inValue.Body.sHash = sHash;
            ITRW324.ServiceReference2.ValidateResponse retVal = ((ITRW324.ServiceReference2.ServiceSoap)(this)).Validate(inValue);
            return retVal.Body.ValidateResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<ITRW324.ServiceReference2.ValidateResponse> ITRW324.ServiceReference2.ServiceSoap.ValidateAsync(ITRW324.ServiceReference2.ValidateRequest request) {
            return base.Channel.ValidateAsync(request);
        }
        
        public System.Threading.Tasks.Task<ITRW324.ServiceReference2.ValidateResponse> ValidateAsync(string sHash) {
            ITRW324.ServiceReference2.ValidateRequest inValue = new ITRW324.ServiceReference2.ValidateRequest();
            inValue.Body = new ITRW324.ServiceReference2.ValidateRequestBody();
            inValue.Body.sHash = sHash;
            return ((ITRW324.ServiceReference2.ServiceSoap)(this)).ValidateAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        ITRW324.ServiceReference2.GetInfoOfDocumentResponse ITRW324.ServiceReference2.ServiceSoap.GetInfoOfDocument(ITRW324.ServiceReference2.GetInfoOfDocumentRequest request) {
            return base.Channel.GetInfoOfDocument(request);
        }
        
        public byte[] GetInfoOfDocument(string sHash) {
            ITRW324.ServiceReference2.GetInfoOfDocumentRequest inValue = new ITRW324.ServiceReference2.GetInfoOfDocumentRequest();
            inValue.Body = new ITRW324.ServiceReference2.GetInfoOfDocumentRequestBody();
            inValue.Body.sHash = sHash;
            ITRW324.ServiceReference2.GetInfoOfDocumentResponse retVal = ((ITRW324.ServiceReference2.ServiceSoap)(this)).GetInfoOfDocument(inValue);
            return retVal.Body.GetInfoOfDocumentResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<ITRW324.ServiceReference2.GetInfoOfDocumentResponse> ITRW324.ServiceReference2.ServiceSoap.GetInfoOfDocumentAsync(ITRW324.ServiceReference2.GetInfoOfDocumentRequest request) {
            return base.Channel.GetInfoOfDocumentAsync(request);
        }
        
        public System.Threading.Tasks.Task<ITRW324.ServiceReference2.GetInfoOfDocumentResponse> GetInfoOfDocumentAsync(string sHash) {
            ITRW324.ServiceReference2.GetInfoOfDocumentRequest inValue = new ITRW324.ServiceReference2.GetInfoOfDocumentRequest();
            inValue.Body = new ITRW324.ServiceReference2.GetInfoOfDocumentRequestBody();
            inValue.Body.sHash = sHash;
            return ((ITRW324.ServiceReference2.ServiceSoap)(this)).GetInfoOfDocumentAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        ITRW324.ServiceReference2.GetBlockChainResponse ITRW324.ServiceReference2.ServiceSoap.GetBlockChain(ITRW324.ServiceReference2.GetBlockChainRequest request) {
            return base.Channel.GetBlockChain(request);
        }
        
        public byte[] GetBlockChain() {
            ITRW324.ServiceReference2.GetBlockChainRequest inValue = new ITRW324.ServiceReference2.GetBlockChainRequest();
            inValue.Body = new ITRW324.ServiceReference2.GetBlockChainRequestBody();
            ITRW324.ServiceReference2.GetBlockChainResponse retVal = ((ITRW324.ServiceReference2.ServiceSoap)(this)).GetBlockChain(inValue);
            return retVal.Body.GetBlockChainResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<ITRW324.ServiceReference2.GetBlockChainResponse> ITRW324.ServiceReference2.ServiceSoap.GetBlockChainAsync(ITRW324.ServiceReference2.GetBlockChainRequest request) {
            return base.Channel.GetBlockChainAsync(request);
        }
        
        public System.Threading.Tasks.Task<ITRW324.ServiceReference2.GetBlockChainResponse> GetBlockChainAsync() {
            ITRW324.ServiceReference2.GetBlockChainRequest inValue = new ITRW324.ServiceReference2.GetBlockChainRequest();
            inValue.Body = new ITRW324.ServiceReference2.GetBlockChainRequestBody();
            return ((ITRW324.ServiceReference2.ServiceSoap)(this)).GetBlockChainAsync(inValue);
        }
    }
}
