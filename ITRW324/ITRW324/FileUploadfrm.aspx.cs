using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using System.Data;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Net;
using ITRW324.ServiceReference1;
using ITRW324.ServiceReference2;


namespace ITRW324
{
    public partial class FileUploadfrm : System.Web.UI.Page
    {
       
        public string hash;
        public string file, type ;
        public int length, userid;
        byte[] myData;
        ServiceReference1.ServiceSoapClient webservice = new ServiceReference1.ServiceSoapClient();
        ServiceReference2.ServiceSoapClient webservice2 = new ServiceReference2.ServiceSoapClient();
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
            if (Session["user"] == null)
                Label1.Text = "please login";
            else
            {
                userid = Convert.ToInt32(Session["ID"]);
                String username = Session["User"].ToString();
                Label1.Text = "ID: " + userid + " Name: " + username;

            }
           

        }

        protected void btnVerify_Click(object sender, EventArgs e)
        {
            Label1.Text = "";
            StringBuilder sb = new StringBuilder();
            var Sha = new SHA256Managed();
            if (FileUploadVerify.HasFile)
            {

                try
                {
                    file = FileUploadVerify.PostedFile.FileName;
                    sb.AppendFormat("<br/> Save As: {0}", file);
                    type = FileUploadVerify.PostedFile.ContentType;
                    sb.AppendFormat("<br/> File type: {0}", type);
                    length = FileUploadVerify.PostedFile.ContentLength;
                    sb.AppendFormat("<br/> File length: {0}", length);
                    sb.AppendFormat("<br/> File name: {0}", file);

                    HttpPostedFile myFile = FileUploadVerify.PostedFile;


                    string date = DateTime.Now.ToString("dd-mm-yy");
                    Stream fs = FileUploadVerify.PostedFile.InputStream;
                    BinaryReader br = new BinaryReader(fs);
                    myData = br.ReadBytes((Int32)fs.Length);
                    hash = BitConverter.ToString(Sha.ComputeHash(myData));
                    sb.AppendFormat("<br/> File hashcode: {0}", hash);
                    Label1.Text = sb.ToString();

                    if (webservice2.Validate(hash) == false)
                    {
                        if (type == "application/pdf")
                        {
                            Label1.Text = "PDF is not original, or is not yet added to our database";
                        }
                        else
                        {
                            Label1.Text = "Only PDF allowed";
                        }

                    }
                    else
                    {
                        Label1.Text = "Exists";

                    }

                }
                catch (Exception ex)
                {
                    Label1.Text = "Upload status: The file could not be Validated. The following error occured: " + ex.Message;
                }

            }
            else
            {
                Label1.Text = "Please select file to Validate";
            }
        }

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            Label1.Text = "";
            StringBuilder sb = new StringBuilder();
            var Sha = new SHA256Managed();
            if (FileUploadVerify.HasFile)
            {

                try
                {
                    file = FileUploadVerify.PostedFile.FileName;
                    sb.AppendFormat("<br/> Save As: {0}", file);
                    type = FileUploadVerify.PostedFile.ContentType;
                    sb.AppendFormat("<br/> File type: {0}", type);
                    length = FileUploadVerify.PostedFile.ContentLength;
                    sb.AppendFormat("<br/> File length: {0}", length);
                    sb.AppendFormat("<br/> File name: {0}", file);

                    HttpPostedFile myFile = FileUploadVerify.PostedFile;

                    
                    string date = DateTime.Now.ToString("dd-mm-yy");
                    Stream fs = FileUploadVerify.PostedFile.InputStream;
                    BinaryReader br = new BinaryReader(fs);
                    myData = br.ReadBytes((Int32)fs.Length);
                    hash = BitConverter.ToString(Sha.ComputeHash(myData));
                    sb.AppendFormat("<br/> File hashcode: {0}", hash);
                    Label1.Text = sb.ToString();
                    bool bvalid = webservice2.Validate(hash);

                    if (bvalid==false)// if the document has not been added
                    {
                        webservice2.AddToBlockChain(hash, Convert.ToString(userid));

                        if (type == "application/pdf")
                        {
                            webservice2.Insert(file, type, date, hash, myData, userid);
                        }
                        else
                        {
                            Label1.Text = "Only PDF allowed";
                        }

                    }
                    else
                    {
                        Label1.Text = "Uploaded";

                    }

                }
                catch (Exception ex)
                {
                    Label1.Text = "Upload status: The file could not be uploaded. The following error occured: " + ex.Message;
                }

            }
            else
            {
                Label1.Text = "Please select file to upload";
            }

        }
    }

    
}











    
                        
                          
                            