using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Data;
using System.Configuration;
using ITRW324.ServiceReference1;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ITRW324
{
    public partial class CreateNewUser : System.Web.UI.Page
    {

     
        ServiceReference1.ServiceSoapClient webservice = new ServiceSoapClient();
        string name = string.Empty;
        string pwd = string.Empty;
        string email = string.Empty;
        protected void OnMenuItemDataBound(object sender, MenuEventArgs e)
        {
            if (SiteMap.CurrentNode != null)
            {
                if (e.Item.Text == SiteMap.CurrentNode.Title)
                {
                    if (e.Item.Parent != null)
                    {
                        e.Item.Parent.Selected = true;
                    }
                    else
                    {
                        e.Item.Selected = true;
                    }
                }
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LoginStatus1_LoggingOut(object sender, LoginCancelEventArgs e)
        {

        }


        protected void Button1_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                name = TextBox1.Text;
                 pwd = TextBox2.Text;
                email = TextBox4.Text;
                 
                if (checkifexist(name) == false)
                {
                    try
                    {                     
                        Label1.Text = webservice.Createuser(name, pwd, email);
                    }
                    catch (Exception ex)
                    {
                        Label1.Text = "failed " + ex;
                    }

                }
                else
                    Label1.Text = "Usersname already in use.";
            }
        }

        public bool checkifexist(string user)
        {
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;

            MySqlConnection conn = new MySqlConnection(constr);
            MySqlCommand cmd = new MySqlCommand("Select * from Users where Username = @user", conn);
            cmd.Parameters.AddWithValue("@user", user);
            conn.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (dr.HasRows == true)
                {

                    return true;
                }

            }
            return false;
        }
    

    }
   

      

        }
    
