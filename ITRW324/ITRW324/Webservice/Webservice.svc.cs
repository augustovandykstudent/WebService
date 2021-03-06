﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace ITRW324
{
	// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "WebService" in code, svc and config file together.
	// NOTE: In order to launch WCF Test Client for testing this service, please select WebService.svc or WebService.svc.cs at the Solution Explorer and start debugging.
	public class WebService : IWebService
	{
        MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
        public string Insert(fileData data)
        {
            string msg = string.Empty;

            con.Open();
            MySqlCommand cmd = new MySqlCommand("insert into documents(Name,Type,Hash,Data,User_ID) values(@Name,@type,@hash,@data,@userid)", con);
            cmd.Parameters.AddWithValue("@Name", data.Name);
            cmd.Parameters.AddWithValue("@type", data.Type);
            cmd.Parameters.AddWithValue("@hash", data.Hash);
            cmd.Parameters.AddWithValue("@data", data.Data);
            cmd.Parameters.AddWithValue("@userid", data.Userid);
            int result = cmd.ExecuteNonQuery();
            if (result == 1)
            {
                msg = data.Name + " inserted successfully";
            }
            else
            {
                msg = "failed to insert file";
            }
            con.Close();
            return msg;
        }

    public DataSet Display(fileData data)
        {
          

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            MySqlCommand cmd = new MySqlCommand("select * from documents", con);
            //MySqlCommand cmd = new MySqlCommand("select * from documents where User_ID=@Userid", con);
            cmd.Parameters.AddWithValue("@Userid", data.Userid);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            cmd.ExecuteNonQuery();
            con.Close();
            return ds;
        }

    }
}
