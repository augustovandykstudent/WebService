using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Data;

using ITRW324.ServiceReference1;

namespace ITRW324
{
    

    public partial class View : System.Web.UI.Page
    {
        string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        ServiceReference1.Service1Client webservice = new ServiceReference1.Service1Client();

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
            if (Session["User"] == null)
                Response.Redirect("Login.aspx");
            else
            {
                int userid = Convert.ToInt32(Session["ID"]);
                string username = Session["User"].ToString();
                //   Label1.Text = "ID: " + userid + " Name: " + username;
                ServiceReference1.fileData Udata = new ServiceReference1.fileData(); 
               
                Udata.Userid = userid;
                if (!IsPostBack)
                {

                    List<ServiceReference1.fileData> files = webservice.GetDocuments(userid);


                    ViewState["Userid"] = Udata.Userid;
                    DataSet ds = new DataSet();
                    
                    
                    DataTable dt = new DataTable();
                    dt.Columns.Add("User_ID");
                    dt.Columns.Add("FileName");
                    dt.Columns.Add("Type");
                    dt.Columns.Add("Hash");

                    foreach (var item in files)
                    {
                        DataRow row = dt.NewRow();
                        row["User_ID"] = item.Userid;
                        row["FileName"] = item.Name;
                        row["Type"] = item.Type;
                        row["Hash"] = item.Hash;
                        dt.Rows.Add(row);
                    }
                    grid.DataSource = dt;                  
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