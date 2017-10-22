using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Runtime.Serialization.Formatters.Binary;
using BlockChainTcpServer;

namespace Die_Validator
{
    
    public partial class Form1 : Form
    {
        
       
        public Form1()
        {
            InitializeComponent();
        }
        OpenFileDialog openPDF = new OpenFileDialog();
        ServiceReference1.ServiceSoapClient webservice = new ServiceReference1.ServiceSoapClient();
        BlockChain _chain;

        private void btnOpen_Click(object sender, EventArgs e)
             
        {
            string sSourceData;
            byte[] tmpSource;
            byte[] tmpHash;


           
            openPDF.Filter = "pdf files (*.pdf)|*.pdf;";
            openPDF.ShowDialog();
            if(openPDF.FileName!= null)
            {
                btnUpload.Enabled = true;
                axAcroPDF1.LoadFile(openPDF.FileName);
                sSourceData = openPDF.FileName;
                tmpSource = ASCIIEncoding.ASCII.GetBytes(sSourceData);
                tmpHash = new MD5CryptoServiceProvider().ComputeHash(tmpSource);

                txtBxHash.Clear();

                txtBxHash.AppendText(ByteArrayToString(tmpHash));
              
                
               
            }
            


        }
        public static string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private static bool SaveBlockChain()
        {

            try
            {
                ObjectToSerialize objectToSerialize = new ObjectToSerialize();
                objectToSerialize.BlockChain = _chain;
                Serializer serializer = new Serializer();
                string sPath = Application.ExecutablePath;
                int iIndex = sPath.Length - 17;
                sPath = sPath.Substring(0, iIndex);
                sPath += "Sales Total Per Year.accdb";
                serializer.SerializeObject(sPath, objectToSerialize);
            }
            catch { return false; }

            return true;
        }
    }
}
