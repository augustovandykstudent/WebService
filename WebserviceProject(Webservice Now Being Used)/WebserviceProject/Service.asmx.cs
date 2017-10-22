using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Data;
using SimpleTCP;
using System.Text;
using System.Threading;
using System.Net;

namespace WebserviceProject
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Service : System.Web.Services.WebService
    {
        MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);

        [WebMethod]
        public string Insert(string sName, string sType, string sCreationDate, string sHash, byte[] bData, int iUserID)
        {
            string msg = string.Empty;

            con.Open();
            MySqlCommand cmd = new MySqlCommand("INSERT INTO Documents(FileName, Type, Creation__date, Hash, Data, User_ID) VALUES ('" + sName + "', '" + sType + "',TO_DATE('" + sCreationDate + "', 'dd-mm-yy')" + sHash + "'," + bData + "," + iUserID + ")", con);
            cmd.CommandTimeout = 0;
            int result = cmd.ExecuteNonQuery();
            if (result == 1)
            {
                msg = sName + " Inserted successfully";
            }
            else
            {
                msg = "failed to insert file";
            }
            con.Close();
            return msg;
        }

        [WebMethod]
        public string CreateUser(string sName, string sPassword, string sEmail)
        {
            string msg = string.Empty;
            con.Open();
            MySqlCommand cmd = new MySqlCommand("INSERT INTO Users(Username,Password,Email) VALUES ('" + sName + "','" + sPassword + "','" + sEmail + "',)", con);
            cmd.CommandTimeout = 0;
            int result = cmd.ExecuteNonQuery();
            if (result == 1)
            {
                msg = sName + " Inserted successfully";
            }
            else
            {
                msg = "failed to create user";
            }
            con.Close();
            return msg;
        }

        [WebMethod]
        public bool AddToBlockChain(string sHash, string sUserID)
        {
            bool bValid = false;
            // create client instance
            SimpleTcpClient client = new SimpleTcpClient();
            client.StringEncoder = Encoding.UTF8;
            try
            {
                client.Connect("10.0.0.10", 4410);
                Message mMessage = client.WriteLineAndGetReply("AddToBlockChain," + sHash + "," + sUserID + ",", TimeSpan.FromSeconds(100));
                string sMessage = mMessage.MessageString;
                if (sMessage.Contains("True"))
                    bValid = true;
                else if (sMessage.Contains("False"))
                    bValid = false;
                client.Disconnect();
            }
            catch { }

            return bValid;
        }

        [WebMethod]
        public bool Validate(string sHash)
        {
            bool bValid = false;
            SimpleTcpClient client = new SimpleTcpClient();
            client.StringEncoder = Encoding.UTF8;
            try
            {
                client.Connect("10.0.0.10", 4410);
                Message mMessage = client.WriteLineAndGetReply("Validate," + sHash + ",", TimeSpan.FromSeconds(100));
                string sMessage = mMessage.MessageString;
                if (sMessage.Contains("True"))
                    bValid = true;
                else if (sMessage.Contains("False"))
                    bValid = false;
                client.Disconnect();

            }
            catch {}
            return bValid;
        }

        [WebMethod]
        public byte[] GetInfoOfDocument(string sHash)
        {
            byte[] bData = null;
            Message mMessage = null;
            SimpleTcpClient client = new SimpleTcpClient();
            client.StringEncoder = Encoding.UTF8;
            try
            {
                client.Connect("10.0.0.10", 4410);
                mMessage = client.WriteLineAndGetReply("GetDocumentInfo," + sHash + ",", TimeSpan.FromSeconds(100));
                bData = mMessage.Data;
                client.Disconnect();
            }
            catch { }
            return bData;
        }

        [WebMethod]
        public byte[] GetBlockChain()
        {
            byte[] bData = null;
            Message mMessage = null;
            SimpleTcpClient client = new SimpleTcpClient();
            client.StringEncoder = Encoding.UTF8;
            try
            {
                client.Connect("10.0.0.10", 4410);
                mMessage = client.WriteLineAndGetReply("GetBlockChain,", TimeSpan.FromSeconds(100));
                bData = mMessage.Data;
                client.Disconnect();
            }
            catch { }
            return bData;
        }
    }
}
