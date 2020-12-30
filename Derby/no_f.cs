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
using System.Data.OleDb;
namespace Derby
{
    public partial class no_f : Form
    {
        Connection con = new Connection();
        internal static no_f form1;
        public no_f()
        {
            InitializeComponent();
            load_data();
            load_data2();
            panel2.BringToFront();
            panel4.BringToFront();
            metroTabControl1.BringToFront();
            load_fight();
            form1 = this;
        }
        public void load_fight()
        {
            metroTabControl1.BringToFront();
            
            try
            {
                con.connectDB.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "select id as 'ID', entry1 as 'Entry 1 Name', entry2 as 'Entry 2 Name' from no_fight";
                cmd.Connection = con.connectDB;
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                datagrid1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something Error Found" + ex.ToString());

            }
            finally
            {
                con.connectDB.Close();
            }
        }
        public Boolean insert()
        {
            try
            {
                con.connectDB.Open();
                //string entry = txtname.Text;
                //string entry2 = txtweight.Text;
                var entry1 = this.drop11.GetItemText(this.drop11.SelectedItem);
                var entry2 = this.drop22.GetItemText(this.drop22.SelectedItem);

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "insert into no_fight values(null,@entry1,@entry2)";
                cmd.Connection = con.connectDB;
                cmd.Parameters.Add("@entry1", MySqlDbType.VarChar).Value = entry1;
                cmd.Parameters.Add("@entry2", MySqlDbType.VarChar).Value = entry2;
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
                load_data();
            }
        }
        private void load_data()
        {
            try
            {
                con.connectDB.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "SELECT DISTINCT name FROM `entry` WHERE 1";
                cmd.Connection = con.connectDB;
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                drop11.DataSource = dt;
                drop11.DisplayMember = "name";
                drop11.ValueMember = "name";
                
                

            }
            catch (Exception ex)
            {
                MessageBox.Show("Something Error Found" + ex.ToString());

            }
            finally
            {
                con.connectDB.Close();
            }
        }
        private void load_data2()
        {
            try
            {
                con.connectDB.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "SELECT DISTINCT name FROM `entry` WHERE 1";
                cmd.Connection = con.connectDB;
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                drop22.DataSource = dt;
                drop22.DisplayMember = "name";
                drop22.ValueMember = "name";



            }
            catch (Exception ex)
            {
                MessageBox.Show("Something Error Found" + ex.ToString());

            }
            finally
            {
                con.connectDB.Close();
            }
        }

        private void bunifuButton4_Click(object sender, EventArgs e)
        {
           
        }

        private void combo2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bunifuDropdown1_onItemSelected(object sender, EventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {

        }

        private void datagrid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bunifuButton1_Click_1(object sender, EventArgs e)
        {
            
        }

        private void bunifuButton4_Click_1(object sender, EventArgs e)
        {
            
        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            
        }

        private void bunifuButton5_Click(object sender, EventArgs e)
        {
           
        }

        private void bunifuButton4_Click_2(object sender, EventArgs e)
        {
            if (insert())
            {
                MessageBox.Show("Added", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                load_fight();
            }
            else
            {
                MessageBox.Show("Failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bunifuButton3_Click_1(object sender, EventArgs e)
        {
            drop11.DataSource = null;
            drop22.DataSource = null;
            load_data();
            load_data2();
        }

        private void bunifuButton1_Click_2(object sender, EventArgs e)
        {
            
        }

        private void bunifuButton5_Click_1(object sender, EventArgs e)
        {

        }

        private void datagrid1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
    
}
