﻿using System;
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
using System.Data.Sql;
using System.IO;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;

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
        string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;

        private void btnOpen_Click(object sender, EventArgs e)
             
        {
            string sSourceData;
            byte[] tmpSource;
            string tmpHash;
            
       

           
            openPDF.Filter = "pdf files (*.pdf)|*.pdf;";
            openPDF.ShowDialog();
            if(openPDF.FileName!= null)
            {
                /* 
                 
                 sSourceData = openPDF.FileName;
                 tmpSource = ASCIIEncoding.ASCII.GetBytes(sSourceData);
                 tmpHash = new SHA256Managed().ComputeHash(tmpSource);*/
               

                btnUpload.Enabled = true;
                axAcroPDF1.LoadFile(openPDF.FileName);
                var Sha = new SHA256Managed();
                FileStream _FileStream = new FileStream(openPDF.FileName, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                BinaryReader br = new BinaryReader(_FileStream);
                tmpSource = br.ReadBytes((Int32)_FileStream.Length);
                tmpHash = BitConverter.ToString(Sha.ComputeHash(tmpSource)); 
                  txtBxHash.Clear();
                txtBxHash.AppendText(tmpHash);

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
            bool doesExist;

           doesExist = webservice.Validate(txtBxHash.Text);
            if(doesExist == false)
            {
                SaveBlockChain();
                MessageBox.Show("File doesn't exists and was uploaded");
            }
            else
            {
                MessageBox.Show("File already Exists");
            }
           
          
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Login tmp = new Login();
            label2.Text = "Welcome User" + tmp.getUser();

        }

        private bool SaveBlockChain()
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

        public string Insert(string sName, string sType, string sCreationDate, string sHash, byte[] bData, int iUserID)
        {
            string msg = string.Empty;

            MySqlConnection con = new MySqlConnection(constr);
            con.Open();
            MySqlCommand cmd = new MySqlCommand("INSERT INTO Documents(FileName, Type, Creation_date, Hash, Data, User_ID) VALUES (@name,@type,@Creation_date,@hash,@data,@User_ID)", con);
            cmd.Parameters.AddWithValue("@name", sName);
            cmd.Parameters.AddWithValue("@type", sType);
            cmd.Parameters.AddWithValue("@Creation_date", sCreationDate);
            cmd.Parameters.AddWithValue("@hash", sHash);
            cmd.Parameters.AddWithValue("@data", bData);
            cmd.Parameters.AddWithValue("@User_ID", iUserID);

            cmd.CommandTimeout = 0;
            int result = cmd.ExecuteNonQuery();
            if (result == 1)
            {
                msg = sName + " Inserted successfully";
            }
            else
            {
                msg = "failed to insert file";
            }
            con.Close();
            return msg;
        }
    }
}
