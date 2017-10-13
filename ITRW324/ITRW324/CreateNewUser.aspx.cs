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
    public partial class CreateNewUser : System.Web.UI.Page
    {
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
                string name = TextBox1.Text;
                string pwd = TextBox2.Text;
                string email = TextBox4.Text;

                if (checkifexist(name) == false)
                {
                    try
                    {
                        string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;

                        MySqlConnection conn = new MySqlConnection(constr);
                        string insert = "Insert into Users (Username,Password,Email) values (@Name,@Pass,@email)";
                        using (MySqlCommand cmd2 = new MySqlCommand(insert, conn))
                        {
                            using (MySqlDataAdapter adpt = new MySqlDataAdapter())
                            {
                                adpt.SelectCommand = cmd2;
                                cmd2.Connection = conn;
                                cmd2.Parameters.Add("@Name", MySqlDbType.VarChar, 50).Value = name;
                                cmd2.Parameters.Add("@Pass", MySqlDbType.VarChar, 50).Value = pwd;
                                cmd2.Parameters.Add("@email", MySqlDbType.VarChar, 50).Value = email;

                                conn.Open();
                                cmd2.ExecuteNonQuery();
                                conn.Close();

                            }

                        }
                        Response.Redirect("Home.aspx");
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
    
