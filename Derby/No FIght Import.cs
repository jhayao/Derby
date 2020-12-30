using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using MySql.Data.MySqlClient;
using Derby.ConnectionClass;

namespace Derby
{
    public partial class No_FIght_Import : Form
    {
        Connection con = new Connection();
        public No_FIght_Import()
        {
            InitializeComponent();
        }

        private void No_FIght_Import_Load(object sender, EventArgs e)
        {

        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            OpenFileDialog fdlg = new OpenFileDialog();
            fdlg.Title = "Select File";
            fdlg.FileName = "Excel.xls";
            fdlg.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm | All Files (*.*) | *.*";
            fdlg.FilterIndex = 1;
            fdlg.RestoreDirectory = true;
            if (fdlg.ShowDialog() == DialogResult.OK)
            {
                txtPath.Text = fdlg.FileName;
                OleDbConnection my_con = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + txtPath.Text + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"");
                my_con.Open();
                OleDbDataAdapter adapt = new OleDbDataAdapter("SELECT * FROM[Sheet2$]", my_con);
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                adapt.Fill(dt);
                datagrid1.DataSource = dt.DefaultView;
            }
        }

        private void bunifuButton4_Click(object sender, EventArgs e)
        {
            try
            {
                con.connectDB.Open();
                MySqlCommand cmd;

                for (int i = 0; i < datagrid1.Rows.Count - 1; i++)
                {
                    cmd = new MySqlCommand();
                    cmd.CommandText = "INSERT INTO `no_fight`(`id`, `entry1`, `entry2`) VALUES (null,@entry1,@entry2)";
                    cmd.Connection = con.connectDB;
                    cmd.Parameters.Add("@entry1", MySqlDbType.VarChar).Value = datagrid1.Rows[i].Cells[1].Value;
                    cmd.Parameters.Add("@entry2", MySqlDbType.VarChar).Value = datagrid1.Rows[i].Cells[2].Value;
                    cmd.ExecuteNonQuery();

                }
                MessageBox.Show("Imported Successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.connectDB.Close();
                datagrid1.DataSource = null;
                no_f.form1.load_fight();

            }
        }
    }
}
