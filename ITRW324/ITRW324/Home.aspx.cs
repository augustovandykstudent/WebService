using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Configuration;
using MySql.Data.MySqlClient;
using System.Data;

namespace ITRW324
{
    public partial class Home : System.Web.UI.Page
    {
        string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection(constr);
            conn.Open();
            if (conn.State == ConnectionState.Open)
            {
                Label1.Text = "Open";
            }
            else
                Label1.Text = "Nein";

            if (Session["user"] == null)
            {
               // Label1.Text = "Please login";
            }
            else
            {
                String userid = Convert.ToString((int)Session["ID"]);
                String username = Session["User"].ToString();
                Label1.Text = "Logged in as: " + username;
             
            }
        }
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

        protected void Menu_MenuItemClick(object sender, MenuEventArgs e)
        {

        }
    }

}