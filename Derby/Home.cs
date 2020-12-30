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
using Microsoft.Office.Interop;
using System.IO;
using System.Reflection;

namespace Derby
{
    
    public partial class Home : Form
    {
        Connection con = new Connection();
        DataGridViewCellEventArgs ex;
        internal static Home form1;
        public string Option;
        public Home()
        {
            InitializeComponent();
           
           
            form1 = this;
        }

        private void ribbonButton1_Click(object sender, EventArgs e)
        {
            if (this.MdiChildren.Count() == 0)
            {
                entry en = new entry();
                en.MdiParent = this;
                en.Dock = DockStyle.Fill;
                en.Show();
            }
            else
            {
                closeAll();
                entry en = new entry();
                en.MdiParent = this;
                en.Dock = DockStyle.Fill;
                en.Show();
            }
           
            ribbonButton3.Enabled = true;
           
            ribbonButton6.Enabled = false;
           
            ribbonButton9.Enabled = false;

        }
        void closeAll()
        {
            foreach (Form frm in this.MdiChildren)
            {
                if (!frm.Focused)
                {
                    frm.Visible = false;
                    frm.Dispose();
                }
            }
        }
        private void ribbonButton2_Click(object sender, EventArgs e)
        {
            if (this.MdiChildren.Count() == 0)
            {
                entry ens = new entry();
                ens.MdiParent = this;
                ens.Dock = DockStyle.Fill;
                ens.Show();
            }
            else
            {
                closeAll();
                entry ens = new entry();
                ens.MdiParent = this;
                ens.Dock = DockStyle.Fill;
                ens.Show();
            }

            Entry_Import en = new Entry_Import();
            
            en.ShowDialog();
            ribbonButton3.Enabled = true;

            ribbonButton6.Enabled = false;

            ribbonButton9.Enabled = false;

        }

        private void ribbonButton3_Click(object sender, EventArgs e)
        {
            try
            {
                con.connectDB.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "truncate table entry";
                cmd.Connection = con.connectDB;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.connectDB.Close();
                entry.form1.load_data();
            }
        }

        private void ribbonButton4_Click(object sender, EventArgs e)
        {
            if (this.MdiChildren.Count() == 0)
            {
                no_f en = new no_f();
                en.MdiParent = this;
                en.Dock = DockStyle.Fill;
                en.Show();
            }
            else
            {
                closeAll();
                no_f en = new no_f();
                en.MdiParent = this;
                en.Dock = DockStyle.Fill;
                en.Show();
            }
            //ribbonButton2.Enabled = false;
            ribbonButton3.Enabled = false;
           // ribbonButton5.Enabled = true;
            ribbonButton6.Enabled = true;
            ribbonButton8.Enabled = false;
            ribbonButton9.Enabled = false;
        }

        private void Home_Load(object sender, EventArgs e)
        {
           
        }
        public void changeLabel(string Option)
        {
           
        }

        private void ribbonButton6_Click(object sender, EventArgs e)
        {
            try
            {
                con.connectDB.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "truncate table no_fight";
                cmd.Connection = con.connectDB;
                DataTable dt = new DataTable();
                cmd.ExecuteNonQuery();



            }
            catch (Exception ex)
            {
                MessageBox.Show("Something Error Found" + ex.ToString());

            }
            finally
            {
                con.connectDB.Close();
                no_f.form1.load_fight();
            }
        }

        private void ribbonButton5_Click(object sender, EventArgs e)
        {
            if (this.MdiChildren.Count() == 0)
            {
                no_f ens = new no_f();
                ens.MdiParent = this;
                ens.Dock = DockStyle.Fill;
                ens.Show();
            }
            else
            {
                closeAll();
                no_f ens = new no_f();
                ens.MdiParent = this;
                ens.Dock = DockStyle.Fill;
                ens.Show();
            }
            ribbonButton3.Enabled = false;
            // ribbonButton5.Enabled = true;
            ribbonButton6.Enabled = true;
            ribbonButton8.Enabled = false;
            ribbonButton9.Enabled = false;
            No_FIght_Import en = new No_FIght_Import();
            en.ShowDialog();
        }

        private void ribbonButton7_Click(object sender, EventArgs e)
        {
           
            if (this.MdiChildren.Count() == 0)
            {
                Matches en = new Matches();
                en.MdiParent = this;
                en.Dock = DockStyle.Fill;
                en.Show();
            }
            else
            {
                closeAll();
                Matches en = new Matches();
                en.MdiParent = this;
                en.Dock = DockStyle.Fill;
                en.Show();
            }
            Weight_Difference wd = new Weight_Difference();
            wd.ShowDialog();
            //ribbonButton2.Enabled = false;
            ribbonButton3.Enabled = false;
            //ribbonButton5.Enabled = false;
            ribbonButton6.Enabled = false;
            ribbonButton8.Enabled = true;
            ribbonButton9.Enabled = true;
        }

        private void ribbonButton8_Click(object sender, EventArgs e)
        {
            try
            {
                con.connectDB.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "truncate table matches";
                cmd.Connection = con.connectDB;
                DataTable dt = new DataTable();
                cmd.ExecuteNonQuery();
                MySqlCommand cmd2 = new MySqlCommand();
                cmd2.CommandText = "truncate table unmatch";
                cmd2.Connection = con.connectDB;
                
                cmd2.ExecuteNonQuery();


            }
            catch (Exception ex)
            {
                MessageBox.Show("Something Error Found" + ex.ToString());

            }
            finally
            {
                con.connectDB.Close();
                Matches.form1.load_fight();
                Matches.form1.load_unmatch();
            }
        }

        private void ribbonButton9_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.InitialDirectory = @"C:\";      
            saveFileDialog1.Title = "Save text Files";
            //saveFileDialog1.CheckFileExists = true;
            //saveFileDialog1.CheckPathExists = true;
            saveFileDialog1.DefaultExt = "xlsx";
            saveFileDialog1.Filter = "Excel files (*.xlsx)|*.xlsx";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
            object missing = Type.Missing;
            Microsoft.Office.Interop.Excel.Application oXL = new Microsoft.Office.Interop.Excel.Application();
            oXL.Visible = true;
            Microsoft.Office.Interop.Excel.Workbook oWB = oXL.Workbooks.Add(missing);
            Microsoft.Office.Interop.Excel.Worksheet oSheet = oWB.ActiveSheet as Microsoft.Office.Interop.Excel.Worksheet;
            oSheet.Name = "Have Match";
            //oSheet.Cells[1, 1] = "Something";
            for (int i = 1; i < Matches.form1.datagrid1.Columns.Count + 1; i++)
            {
                oSheet.Cells[1, i] = Matches.form1.datagrid1.Columns[i - 1].HeaderText;
            }
            // storing Each row and column value to excel sheet  
            for (int i = 0; i < Matches.form1.datagrid1.Rows.Count; i++)
            {
                for (int j = 0; j < Matches.form1.datagrid1.Columns.Count; j++)
                {
                    oSheet.Cells[i + 2, j + 1] = Matches.form1.datagrid1.Rows[i].Cells[j].Value.ToString();
                }
            }
            Microsoft.Office.Interop.Excel.Worksheet oSheet2 = oWB.Sheets.Add(missing, missing, 1, missing)
                            as Microsoft.Office.Interop.Excel.Worksheet;
            oSheet2.Name = "Don't Have Match";
            for (int i = 1; i < Matches.form1.datagrid2.Columns.Count + 1; i++)
            {
                oSheet2.Cells[1, i] = Matches.form1.datagrid2.Columns[i - 1].HeaderCell.Value;
            }


            // storing Each row and column value to excel sheet


            for (int i = 0; i < Matches.form1.datagrid2.Rows.Count; i++)
            {
                for (int j = 0; j < Matches.form1.datagrid2.Columns.Count; j++)
                {

                    oSheet2.Cells[i + 2, j + 1] = Matches.form1.datagrid2.Rows[i].Cells[j].Value.ToString();

                }
            }
            string fileName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
                                    + "\\SoSample.xlsx";
            oWB.SaveAs(saveFileDialog1.FileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlOpenXMLWorkbook,
                missing, missing, missing, missing,
                Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
                missing, missing, missing, missing, missing);
            //oWB.Close(missing, missing, missing);
            //oXL.UserControl = true;
            //oXL.Quit();
        }
        }

        private void Home_FormClosing(object sender, FormClosingEventArgs e)
        {
            Choice c = new Choice();
            c.Show();
        }

        private void ribbonButton10_Click(object sender, EventArgs e)
        {
            entry.form1.update(ex);
                
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        public void update(DataGridViewCellEventArgs e)
        {
            this.ex = e;
        }

        private void ribbonButton11_Click(object sender, EventArgs e)
        {
           
            DataGridViewRow row = entry.form1.datagrid1.Rows[ex.RowIndex];
            int id= Int32.Parse(row.Cells[0].Value.ToString());
            try
            {
                con.connectDB.Open();
                
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "delete from entry where id = @id";
                cmd.Connection = con.connectDB;
                cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
                cmd.ExecuteNonQuery();
                MessageBox.Show("Deleted Succesfully!!","Success",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show("Delete Failed!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            finally
            {
                con.connectDB.Close();
                entry.form1.load_data();
            }
        }

        private void ribbonTab1_PressedChanged(object sender, EventArgs e)
        {
            
        }

        private void ribbonTab1_ActiveChanged(object sender, EventArgs e)
        {
           
        }

        private void Home_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
