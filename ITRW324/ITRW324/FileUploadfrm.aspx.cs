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
using ITRW324.Webservice;
namespace ITRW324
{
    public partial class FileUploadfrm : System.Web.UI.Page
    {
        public string hash;
        public string file, type ;
        public int length, userid;
        byte[] myData;
        Webservice.WebServiceClient webservice = new Webservice.WebServiceClient();

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
                // Response.Redirect("Login.aspx");
                Label1.Text = "please login";
            else
            {
                userid = Convert.ToInt32(Session["ID"]);
                String username = Session["User"].ToString();
                Label1.Text = "ID: " + userid + " Name: " + username;

            }
           

        }

        protected void btnsubmit_Click(object sender, EventArgs e)
        {

            StringBuilder sb = new StringBuilder();
            var Sha = new SHA256Managed();
            if (FileUploadVerify.HasFile)
            {

                try
                {

                    //  FileUploadVerify.SaveAs(("C:/Users/deWit777/Desktop/upload/") + file);
                    file = FileUploadVerify.PostedFile.FileName;
                    sb.AppendFormat("<br/> Save As: {0}", file);
                    type = FileUploadVerify.PostedFile.ContentType;
                    sb.AppendFormat("<br/> File type: {0}", type);
                    length = FileUploadVerify.PostedFile.ContentLength;
                    sb.AppendFormat("<br/> File length: {0}", length);
                    sb.AppendFormat("<br/> File name: {0}", file);

                    HttpPostedFile myFile = FileUploadVerify.PostedFile;



                    Stream fs = FileUploadVerify.PostedFile.InputStream;
                    BinaryReader br = new BinaryReader(fs);
                    myData = br.ReadBytes((Int32)fs.Length);
                    hash = BitConverter.ToString(Sha.ComputeHash(myData));
                    sb.AppendFormat("<br/> File hashcode: {0}", hash);
                    //  sb.AppendFormat("<br/> File data: {0}", data);
                    Label1.Text = sb.ToString();
                    if (checkifexist()==false)
                    {
                        if (type == "application/pdf")
                        {
                            Webservice.fileData data = new Webservice.fileData();
                            data.Name = file;
                            data.Type = type;
                            data.Hash = hash;
                           data.Data= myData;
                            data.Userid = userid;


                          // upload();
                          webservice.Insert(data);
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


                    
                    //    Response.Redirect(Request.Url.AbsoluteUri);



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

        //Check if pdf hash exists in database
        public bool checkifexist()
        {
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;

            MySqlConnection conn = new MySqlConnection(constr);
            MySqlCommand cmd = new MySqlCommand("Select * from Documents where Hash = @Hash", conn);
            cmd.Parameters.AddWithValue("@Hash", hash);
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

        //Upload pdf to database
        public void upload()
        {
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            try
            {
                MySqlConnection conn = new MySqlConnection(constr);

                string time = DateTime.Now.ToString("yyyy-MM-dd"); ;

                

                string insert = "Insert into Documents (Name,Type,Creation_date,Hash, Data) values (@Name,@Type,@Creationdate,@Hash,@Data)";
                using (MySqlCommand cmd = new MySqlCommand(insert, conn))
                {
                    cmd.Connection = conn;

                    
                    using (MySqlDataAdapter adpt = new MySqlDataAdapter())
                    {
                        adpt.SelectCommand = cmd;
                        //   cmd.Parameters.AddWithValue("@ID", test);
                        cmd.Parameters.AddWithValue("@Name", file);
                        cmd.Parameters.AddWithValue("@Type", type);
                        cmd.Parameters.AddWithValue("@Creationdate", time);

                        cmd.Parameters.AddWithValue("@Hash", hash);
                        // cmd.Parameters.AddWithValue("@Previoushash", test2);
                        cmd.Parameters.AddWithValue("@Data", myData);


                        conn.Open();
                        int result = cmd.ExecuteNonQuery();
                        conn.Close();

                    }
                }

            }
            catch (Exception ex)
            {
                Label1.Text = "not entered " + ex;
            }
        }


    
      

        }

    
    

    }











    