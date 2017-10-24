using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Die_Validator
{
    public partial class Login : Form
    {
        private int UserID = 0;

        public Login()
        {
            InitializeComponent();
        }

        ServiceReference1.ServiceSoapClient webservice = new ServiceReference1.ServiceSoapClient();

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string userid = txtBxUID.Text;
            string pass = txtBxPW.Text;
            if(webservice.Login(userid,pass) != 0)
                {
                UserID = webservice.Login(userid, pass);
                Form1 tmp = new Form1();
                tmp.Visible = true;
                }
        }
        public int getUser()
        {
            return UserID;
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
