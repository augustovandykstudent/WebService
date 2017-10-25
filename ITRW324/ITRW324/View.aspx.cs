<<<<<<< Updated upstream
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Data;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace ITRW324
{
    

    public partial class View : System.Web.UI.Page
    {
        BlockChain chain = new BlockChain();
        string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        ServiceReference2.ServiceSoapClient webservice = new ServiceReference2.ServiceSoapClient();

        protected void OnMenuItemDataBound(object sender, MenuEventArgs e)
        {
            if (SiteMap.CurrentNode != null)
            {
                if (e.Item.Text == SiteMap.CurrentNode.Title)
=======
﻿using System;
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
        ServiceReference2.ServiceSoapClient webservice = new ServiceReference2.ServiceSoapClient();

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
>>>>>>> Stashed changes
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
                    string suserid = Convert.ToString(userid);
                    byte[] binfo = webservice.GetUserBlockChainInfo(suserid);
                    BlockChain ochain = Deserialize(binfo);
                    List<string> llist = ochain.GetBlockValuesList();
                    List<string> ltimestamp = new List<string>(), lhash = new List<string>(); 
                    foreach (string sstring in llist)
                    {
                        string[] sdata = sstring.Split(',');
                        ltimestamp.Add(sdata[0]);
                        lhash.Add(sdata[1]);
                    }

                    DataTable dtable = new DataTable();
                    DataColumn dusers = new DataColumn();
                    dusers.ColumnName = "User ID";
                    DataColumn dtimestamp = new DataColumn();
                    dtimestamp.ColumnName = "Time Stamp";
                    DataColumn dhash = new DataColumn();
                    dhash.ColumnName = "Hash";
                    dtable.Columns.Add(dusers);// add three columns to table
                    dtable.Columns.Add(dtimestamp);
                    dtable.Columns.Add(dhash);

                    int icounter = 0; // counter to move the row amount
                    foreach (string slist in ltimestamp)// add new rows for table
                    {
                        dtable.Rows[icounter]["User ID"] = suserid;
                        dtable.Rows[icounter]["Time Stamp"] = slist;
                        icounter++;
                    }
                    icounter = 0;
                    foreach (string slist in lhash)// add new rows for table
                    {
                        dtable.Rows[icounter]["Hash"] = slist;
                        icounter++;
                    }

                    grid.DataSource = dtable;
                    grid.DataBind();
                }
            }

        }
 
        protected void grid_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        private BlockChain Deserialize(byte[] param)
        {
            MemoryStream stream = new MemoryStream(param);
            BinaryFormatter bformat = new BinaryFormatter();
            ObjectToSerialize objectSerialize = new ObjectToSerialize();
            objectSerialize = (ObjectToSerialize)bformat.Deserialize(stream);
            BlockChain newChain = objectSerialize.BlockChain;
            return newChain;    
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

        protected void Menu_MenuItemClick(object sender, MenuEventArgs e)
        {

        }
    }
}