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
    
    public partial class entry : Form
    {
        Connection con = new Connection();
        internal static entry form1;
        public entry()
        {
            InitializeComponent();
            form1 = this;
        }
        

        private void MetroTabPage1_Click(object sender, EventArgs e)
        {

        }

        private void Entry_Load(object sender, EventArgs e)
        {
            datagrid1.BringToFront();
            panel2.BringToFront();
            panel2.BringToFront();
            
            load_data();

        }

        public void load_data()
        {
            try
            {
                con.connectDB.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "select id as 'Entry ID', name as 'Entry Name', wing_band as 'Wing Band', weight as 'Entry Weight', color as 'Color' from entry";
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
        private DataTable getEntry()
        {

            try
            {
                con.connectDB.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "select * from entry";
                cmd.Connection = con.connectDB;
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                datagrid1.DataSource = dt;

            }
            catch
            {
                MessageBox.Show("Error");
            }
            finally
            {
                con.connectDB.Close();
            }

            DataTable tables = new DataTable();

            return tables;
        }

        private void BunifuButton1_Click(object sender, EventArgs e)
        {

        }

        private void MetroTabPage5_Click(object sender, EventArgs e)
        {

        }

        private void Label3_Click(object sender, EventArgs e)
        {

        }

        private void Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void BunifuButton1_Click_1(object sender, EventArgs e)
        {
            
        }
        public Boolean insert()
        {
            try
            {
                con.connectDB.Open();
                string name = txtname.Text;
                string weight = txtweight.Text;
                string color = txtcolor.Text;
                string wing = txtwing.Text;

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "insert into entry values(null,@name,@weight,@color,@wing)";
                cmd.Connection = con.connectDB;
                cmd.Parameters.Add("@name", MySqlDbType.VarChar).Value = name;
                cmd.Parameters.Add("@weight", MySqlDbType.VarChar).Value = weight;
                cmd.Parameters.Add("@color", MySqlDbType.VarChar).Value = wing;
                cmd.Parameters.Add("@wing", MySqlDbType.VarChar).Value = color;
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

        public Boolean edit()
        {
            try
            {
                con.connectDB.Open();
                string name = txtname.Text;
                string weight = txtweight.Text;
                string color = txtcolor.Text;
                string wing = txtwing.Text;
                int id = Int32.Parse(txtid.Text);
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "update entry set name=@name,weight=@weight,color = @color,wing_band = @wing where id = @id";
                cmd.Connection = con.connectDB;
                cmd.Parameters.Add("@name", MySqlDbType.VarChar).Value = name;
                cmd.Parameters.Add("@weight", MySqlDbType.VarChar).Value = weight;
                cmd.Parameters.Add("@color", MySqlDbType.VarChar).Value = color;
                cmd.Parameters.Add("@wing", MySqlDbType.VarChar).Value = wing;
                cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
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

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            

        }

        private void bunifuButton4_Click(object sender, EventArgs e)
        {
            
        }

        private void bunifuButton3_Click_1(object sender, EventArgs e)
        {
            
        }

        private void bunifuButton4_Click_1(object sender, EventArgs e)
        {
            
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bunifuButton5_Click(object sender, EventArgs e)
        {

        }

        private void bunifuButton5_Click_1(object sender, EventArgs e)
        {
            
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuButton1_Click_2(object sender, EventArgs e)
        {
           if(bunifuButton1.Text=="Save")
            {
                if (insert())
                {
                    MessageBox.Show("Added Succesfully", "Added", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Add failed", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
           else
            {
                if (edit())
                {
                    MessageBox.Show("Update Succesfully", "Added", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Update failed", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            reset();
            bunifuButton1.Text = "Save";
            Home.form1.ribbonButton10.Enabled = false;
            Home.form1.ribbonButton11.Enabled = false;
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            reset();
            Home.form1.ribbonButton10.Enabled = false;
            Home.form1.ribbonButton11.Enabled = false;
            bunifuButton1.Text = "Save";
        }
        void reset()
        {
            txtname.Text = "";
            txtweight.Text = "";
            txtwing.Text = "";
            txtcolor.Text = "";
        }

        private void datagrid1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Home.form1.ribbonButton10.Enabled = true;
            Home.form1.ribbonButton11.Enabled = true;
            Home.form1.update(e);


        }

        private void datagrid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }
        public void update(DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = entry.form1.datagrid1.Rows[e.RowIndex];
            txtname.Text = row.Cells[1].Value.ToString();
            txtweight.Text = row.Cells[3].Value.ToString();
            txtwing.Text = row.Cells[2].Value.ToString();
            txtcolor.Text = row.Cells[4].Value.ToString();
            bunifuButton1.Text = "Update";
            txtid.Text = row.Cells[0].Value.ToString();
        }

        private void txtwing_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
