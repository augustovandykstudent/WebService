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

namespace ITRW324
{
    public partial class FileUploadfrm : System.Web.UI.Page
    {
        public string hash;
        public string file, type;
        public int length;
        byte[] myData,bytes;




        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                display();
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

                    if (type == "application/pdf")
                    {
                        upload();
                    }
                    else
                    {
                        Label1.Text = "Only PDF allowed";
                    }

            
                   

                   

                    display();
                    Response.Redirect(Request.Url.AbsoluteUri);



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




    public void upload()
        {
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            try
            {
                MySqlConnection conn = new MySqlConnection(constr);

                string time = DateTime.Now.ToString("yyyy-MM-dd"); ;
              
            
              
                string insert = "Insert into Documents (Name,Type,Creation_date,Hash, Data) values (@Name,@Type,@Creationdate,@Hash,@Data)";
                using (MySqlCommand cmd = new MySqlCommand(insert,conn))
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
                      int result =  cmd.ExecuteNonQuery();
                        conn.Close();
                        
                    }
                }

            }
            catch(Exception ex)
            {
                Label1.Text = "not entered "+ex;
            }
}



        public void display()
        {

                string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
                using (MySqlConnection con = new MySqlConnection(constr))
                {
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.CommandText = "select Id,Name From Documents";
                        cmd.Connection = con;
                        con.Open();
                        grid.DataSource = cmd.ExecuteReader();
                        grid.DataBind();
                        con.Close();
                    }
                }
                
           


        }
        protected void View(object sender, EventArgs e)
        {
            int id = int.Parse((sender as LinkButton).CommandArgument);
            string embed = "<object data=\"{0}{1}\" type=\"application/pdf\" width=\"500px\" height=\"600px\">";
            embed += "If you are unable to view file, you can download from <a href = \"{0}{1}&download=1\">here</a>";
            embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
            embed += "</object>";
            Label2.Text = string.Format(embed, ResolveUrl("~/FileCS.ashx?Id="), id);
        }

    }
}








    