using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;
namespace ITRW324
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IWebService" in both code and config file together.
    [ServiceContract]
    public interface IWebService
    {
       

        [OperationContract]
        string Insert(fileData data);
        [OperationContract]
        DataSet Display(fileData data);

        
    }


    [DataContract]
    public class fileData
    {
        string name = string.Empty;
        string type = string.Empty;
        string date = string.Empty;
        string hash = string.Empty;
       
        int userid;
        byte fdata;
        [DataMember]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        [DataMember]
        public string Type
        {
            get { return type; }
            set { type = value; }
        }
        [DataMember]
        public string Hash
        {
            get { return hash; }
            set { hash = value; }
        }
       
        [DataMember]
        public string Date
        {
            get { return date; }
            set { date = value; }
        }
        [DataMember]
        public int Userid
        {
            get { return userid; }
            set { userid = value; }
        }
        [DataMember]
        public byte filData
        {
            get { return fdata; }
            set { fdata = value; }
        }

    }
}
