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
    public partial class Entry_Import : Form
    {
        Connection con = new Connection();
       
        public Entry_Import()
        {
            InitializeComponent();
            
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
                OleDbDataAdapter adapt = new OleDbDataAdapter("SELECT * FROM[Sheet1$]", my_con);
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
                MySqlCommand cmd2;
                cmd2 = new MySqlCommand();
                DialogResult dialogResult = MessageBox.Show("All Weight below minimum weight will be adjusted to the minimum weight set earlier", "Adjust?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                
                if (dialogResult == DialogResult.Yes)
                {
                    MySqlCommand cmds = new MySqlCommand();
                    cmds.CommandText = "SELECT minimum  FROM `inititalweight`";
                    cmds.Connection = con.connectDB;
                    int count = Convert.ToInt32(cmds.ExecuteScalar());
                    MessageBox.Show(count.ToString());
                    for (int i = 0; i < datagrid1.Rows.Count - 1; i++)
                    {


                        cmd = new MySqlCommand();
                        cmd.CommandText = "insert into entry(`id`, `name`, `weight`, `wing_band`, `color`) values(null,@name,@weight,@wing,@color)";
                        cmd.Connection = con.connectDB;
                        cmd.Parameters.Add("@name", MySqlDbType.VarChar).Value = datagrid1.Rows[i].Cells[1].Value;
                        if((Int32.Parse(datagrid1.Rows[i].Cells[2].Value.ToString()) > count))
                        {
                            cmd.Parameters.Add("@weight", MySqlDbType.VarChar).Value = datagrid1.Rows[i].Cells[2].Value;
                        }
                        else
                        {
                            cmd.Parameters.Add("@weight", MySqlDbType.VarChar).Value = count;

                        }
                        cmd.Parameters.Add("@color", MySqlDbType.VarChar).Value = datagrid1.Rows[i].Cells[4].Value;
                        cmd.Parameters.Add("@wing", MySqlDbType.Int32).Value = Int32.Parse(datagrid1.Rows[i].Cells[3].Value.ToString());
                        cmd.ExecuteNonQuery();

                    }
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
                entry.form1.load_data();
                
            }
        }

        private void Entry_Import_Load(object sender, EventArgs e)
        {

        }
    }
}
