using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Derby.ConnectionClass;
using MySql.Data.MySqlClient;

namespace Derby
{
    public partial class Choice : Form
    {
        Connection con = new Connection();
        public Choice()
        {
            InitializeComponent();
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            if(txtmin.Text == "" || txtmax.Text == "")
            {
                MessageBox.Show("Text Field should not be empty","Empty Fields",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
            else
            {
                int min = Int32.Parse(txtmin.Text);
                int max = Int32.Parse(txtmax.Text);
                if(min<max)
                {
                    if (saveWeight())
                    {
                        MessageBox.Show("Saved");
                        Home home = new Home();
                        home.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Not Saved");
                    }
                }
                else
                {
                    MessageBox.Show("Minimum Weight should not be higher than Maximum weight", "Invalid weight", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            


        }


        private Boolean saveWeight()
        {
            try
            {
                con.connectDB.Open();
                string min = txtmin.Text;
                string max = txtmax.Text;
                MySqlCommand cmds = new MySqlCommand();
                cmds.CommandText = "truncate table inititalweight ";
                cmds.Connection = con.connectDB;
                cmds.ExecuteNonQuery();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "insert into inititalweight values(@min,@max)";
                cmd.Connection = con.connectDB;
                cmd.Parameters.Add("@min", MySqlDbType.VarChar).Value = min;
                cmd.Parameters.Add("@max", MySqlDbType.VarChar).Value = max;
               
                cmd.ExecuteNonQuery();
                return true;
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

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            //Home.form1.label1.Text = "Stag";
            
        }

        private void Choice_Load(object sender, EventArgs e)
        {
            setWeight();
        }
        private void setWeight()
        {
            con.connectDB.Open();
            MySqlCommand cmd6 = new MySqlCommand();
            cmd6.CommandText = "select * from inititalweight";
            cmd6.Connection = con.connectDB;
            
            DataTable dts = new DataTable();
            dts.Load(cmd6.ExecuteReader());
            foreach (DataRow rows in dts.Rows)
            {
                txtmin.Text = rows["minimum"].ToString();
                txtmax.Text = rows["maximum"].ToString();
            }
            con.connectDB.Close ();
        }
    }
}
