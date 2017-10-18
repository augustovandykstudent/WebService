using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Configuration;
using MySql.Data.MySqlClient;

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
        public string Insert(string sName, string sType, string sHash, byte[] bData, int iUserID)
        {
            string msg = string.Empty;

            con.Open();
            MySqlCommand cmd = new MySqlCommand("INSERT INTO Documents(Name,Type,Hash,Data,User_ID) VALUES ('" + sName + "', '" +sType + "', '" + sHash + "'," + bData+ "," + iUserID +")", con);
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
        public List<string[]> GetDocuments(int iUserID)
        {
            List<string[]> list = new List<string[]>();
            con.Open();
            MySqlCommand command = new MySqlCommand("SELECT User_ID, FileName, Type, Hash FROM Documents WHERE UserID = " + iUserID, con);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string[] sResult = new string[] { (string)reader["User_ID"], (string)reader["FileName"], (string)reader["Type"], (string)reader["Hash"] };
                list.Add(sResult);
            }
            return list;
        }

        [WebMethod]
        public string Createuser(string sName, string sPassword, string sEmail)
        {
            string msg = string.Empty;
            con.Open();
            MySqlCommand cmd = new MySqlCommand("INSERT INTO Users(Username,Password,Email) VALUES ('" + sName +"','" + sPassword +"','" + sEmail +"',)", con);
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
    }
}
