using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Derby.ConnectionClass;
namespace Derby
{
    public partial class login : Form
    {
        Connection con = new Connection();
        public login()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        public void connection()
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                con.connectDB.Open();
                MessageBox.Show("Connected Succesfully");
                con.connectDB.Close();
            }
            catch
            {
                MessageBox.Show("Error");
            }
        }

        private void BunifuCustomLabel2_Click(object sender, EventArgs e)
        {

        }

        private void BunifuButton1_Click(object sender, EventArgs e)
        {
            if(logins())
            {
               
                MessageBox.Show("Succesfully Login", "Login Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Hide();
                Choice dash = new Choice();
                dash.Show();
            }
            else
            {
                MessageBox.Show("Login Failed", "Login Failed", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }
        public Boolean logins()
        {
            try
            {
                con.connectDB.Open();
                string username = txtuname.Text;
                string password = txtpword.Text;
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "select * from user where username = @username  and password = @password";
                cmd.Connection = con.connectDB;
                cmd.Parameters.Add("@username", MySqlDbType.VarChar).Value = username;
                cmd.Parameters.Add("@password", MySqlDbType.VarChar).Value = password;
                MySqlDataReader row;
                row = cmd.ExecuteReader();
                if(row.HasRows)
                {
                    return true;
                }
                else
                {
                    return false;
                }
                
                

            }
            catch 
            {
                return false;
            }
            finally
            {
                con.connectDB.Close();
            }
        }

        private void BunifuButton2_Click(object sender, EventArgs e)
        {
            
        }
    }
}
