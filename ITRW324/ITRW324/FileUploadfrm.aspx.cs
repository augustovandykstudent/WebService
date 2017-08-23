using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.IO;
using System.Text;


namespace ITRW324
{
    public partial class FileUploadfrm : System.Web.UI.Page
    {
        string hash;
        string file;
        
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnsubmit_Click(object sender, EventArgs e)
        {

            StringBuilder sb = new StringBuilder();
            var Sha = new SHA256Managed();
            if (FileUploadVerify.HasFile)
            {

                try
                {
                    file = FileUploadVerify.FileName;
                    //  FileUploadVerify.SaveAs(("C:/Users/deWit777/Desktop/upload/") + file);
                    sb.AppendFormat("<br/> Save As: {0}", FileUploadVerify.PostedFile.FileName);
                    sb.AppendFormat("<br/> File type: {0}", FileUploadVerify.PostedFile.ContentType);
                    sb.AppendFormat("<br/> File length: {0}", FileUploadVerify.PostedFile.ContentLength);
                    sb.AppendFormat("<br/> File name: {0}", FileUploadVerify.PostedFile.FileName);

                    HttpPostedFile myFile = FileUploadVerify.PostedFile;
                    int nfilelen = myFile.ContentLength;
                    byte[] myData = new byte[nfilelen];
                    myFile.InputStream.Read(myData, 0, nfilelen);

                    hash = BitConverter.ToString(Sha.ComputeHash(myData));
                    sb.AppendFormat("<br/> File hashcode: {0}", hash);

                    Label1.Text = sb.ToString();

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