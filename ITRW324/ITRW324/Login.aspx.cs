using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Data;
using System.Configuration;

namespace ITRW324
{
    public partial class Login : System.Web.UI.Page
    {
        string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;

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

        protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
           
            MySqlConnection conn = new MySqlConnection(constr);

            string sUser = TextBox1.Text;
            string sPass = TextBox2.Text;
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM Users WHERE Username = '"+ sUser + "' AND Password = '" + sPass + "';", conn);
            
            conn.Open();
            MySqlDataReader rd = cmd.ExecuteReader();

            if(rd.Read())
            {
                Session["ID"] = rd["UserID"];
                Session["User"] = rd["Username"];
                rd.Close();
                cmd.Dispose();
                conn.Close();
                Response.Redirect("Home.aspx");
            }
            else
            {
                Label1.Text = "Username or Password is incorrect";
                conn.Close();
            }    
        }

        protected void TextBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}