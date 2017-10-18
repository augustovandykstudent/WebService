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
    

    public partial class View : System.Web.UI.Page
    {
        string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        ServiceReference1.ServiceSoapClient webservice = new ServiceReference1.ServiceSoapClient();

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
            MySqlConnection con = new MySqlConnection(constr);
            if (Session["User"] == null)
                Response.Redirect("Login.aspx");
            else
            {
                int userid = Convert.ToInt32(Session["ID"]);
                string username = Session["User"].ToString();
                //   Label1.Text = "ID: " + userid + " Name: " + username;
                if (!IsPostBack)
                {
                    DataTable oDataTable = new DataTable();
                    con.Open();
                    MySqlCommand command = new MySqlCommand("SELECT ID, FileName, Type, Hash FROM Documents WHERE ID = " + userid, con);
                    MySqlDataReader reader = command.ExecuteReader();
                    reader.Close();
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = command;
                    adapter.Fill(oDataTable);

                    grid.DataSource = oDataTable;
                    grid.DataBind();
                }
            }

        }
 
        protected void grid_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void view(object sender, EventArgs e)
        {

            string id = (sender as LinkButton).CommandArgument;
            string embed = "<object data=\"{0}{1}\" type=\"application/pdf\" width=\"80%\" height=\"1000px\">";
            embed += "If you are unable to view file, you can download from <a href = \"{0}{1}&download=1\">here</a>";
            embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
            embed += "</object>";
            Label1.Text = string.Format(embed, ResolveUrl("~/PDFHandler.ashx?Hash="), id);  //Call Generic Handler
        }
    }
}