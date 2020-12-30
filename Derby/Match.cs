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
    public partial class Matches : Form
    {
        Connection con = new Connection();
        internal static Matches form1;
        public Matches()
        {
            InitializeComponent();
            load_fight();
            form1 = this;
        }

        /*void match()
        {
            try
            {
                con.connectDB.Open();
                MySqlCommand cmd = new MySqlCommand();

                cmd.CommandText = "select * from entry";
                cmd.Connection = con.connectDB;
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                //int limit = Int32.Parse(dropdown1.selectedValue);
                MySqlCommand cmd2;
                MySqlCommand cmd3;
                MySqlCommand cmd4;
                MySqlCommand cmd5;
                foreach (DataRow row in dt.Rows)
                {
                    DataTable dt2 = new DataTable();
                    DataTable dt3;
                    DataTable dt4;
                    if (Int32.Parse(row["weight"].ToString()) < 2400)
                    {
                        cmd2 = new MySqlCommand();
                        cmd2.CommandText = "SELECT * FROM `entry` WHERE (weight BETWEEN @weight1 and @weight2) and name != @name and weight <=2400 ";
                        cmd2.Connection = con.connectDB;
                        cmd2.Parameters.Add("@weight1", MySqlDbType.Int32).Value = Int32.Parse(row["weight"].ToString()) - limit;
                        cmd2.Parameters.Add("@weight2", MySqlDbType.Int32).Value = Int32.Parse(row["weight"].ToString()) + limit;
                        cmd2.Parameters.Add("@name", MySqlDbType.VarChar).Value = row["name"].ToString();
                        dt2.Load(cmd2.ExecuteReader());
                    }
                    else if (Int32.Parse(row["weight"].ToString()) > 2400)
                    {
                        cmd2 = new MySqlCommand();
                        cmd2.CommandText = "SELECT * FROM `entry` WHERE  name != @name and weight > 2400 ";
                        cmd2.Connection = con.connectDB;
                        cmd2.Parameters.Add("@name", MySqlDbType.VarChar).Value = row["name"].ToString();
                        dt2.Load(cmd2.ExecuteReader());
                    }
                    foreach (DataRow row2 in dt2.Rows)
                    {
                        cmd3 = new MySqlCommand();
                        cmd4 = new MySqlCommand();
                        cmd5 = new MySqlCommand();
                        cmd5.CommandText = "SELECT * FROM no_fight WHERE (entry1 = '" + row["name"] + "' and entry2= '" + row2["name"] + "') or (entry2 = '" + row["name"] + "' and entry1 = '" + row2["name"] + "')";
                        cmd5.Connection = con.connectDB;
                        cmd4.CommandText = "select * from matches where entry1 = @name or entry2 = @name";
                        cmd4.Parameters.Add("@name", MySqlDbType.VarChar).Value = row["name"];
                        cmd4.Connection = con.connectDB;
                        dt3 = new DataTable();
                        dt4 = new DataTable();
                        dt3.Load(cmd4.ExecuteReader());
                        dt4.Load(cmd5.ExecuteReader());
                        if (dt3.Rows.Count == 0)
                        {
                            //MessageBox.Show(row["name"].ToString());
                            //MessageBox.Show(row2["name"].ToString());
                            if (dt4.Rows.Count <= 0)
                            {
                                cmd3.CommandText = "insert into matches values(null,@entry1,@entry2)";
                                cmd3.Parameters.Add("@entry1", MySqlDbType.VarChar).Value = row["name"];
                                cmd3.Parameters.Add("@entry2", MySqlDbType.VarChar).Value = row2["name"];
                                cmd3.Connection = con.connectDB;
                                cmd3.ExecuteNonQuery();
                            }
                        }
                    }

                }

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
        */
        public void load_fight()
        {
            try
            {
                con.connectDB.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "SELECT DISTINCT matches.id as 'Match ID', (SELECT entry.weight from entry WHERE entry.id = matches.entry1) AS 'Weight', (SELECT entry.wing_band from entry WHERE entry.id = matches.entry1) AS 'Wingband' , (SELECT entry.color from entry WHERE entry.id = matches.entry1) AS 'Color',(SELECT entry.name from entry WHERE entry.id = matches.entry1) AS 'Entry Name', 'VS',(SELECT entry.name from entry WHERE entry.id = matches.entry2) AS 'Entry Name',(SELECT entry.color from entry WHERE entry.id = matches.entry2) AS 'Color',(SELECT entry.wing_band from entry WHERE entry.id = matches.entry2) AS 'Wingband' ,(SELECT entry.weight from entry WHERE entry.id = matches.entry2) AS 'Weight' from matches as matches order by matches.id ASC";
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
        public void load_unmatch()
        {
            try
            {
                con.connectDB.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "select * from entry where id not in (select entry1 from matches) and id not in (SELECT entry2 from matches)";

                cmd.Connection = con.connectDB;
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                datagrid2.DataSource = dt;
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
        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            //match();
            load_fight();
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            try
            {
                con.connectDB.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "truncate table matches";
                cmd.Connection = con.connectDB;
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Something Error Found" + ex.ToString());

            }
            finally
            {
                con.connectDB.Close();
                load_fight();
            }
        }

        private void Match_Load(object sender, EventArgs e)
        {
            load_unmatch();
        }

        private void datagrid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void datagrid1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void bunifuDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void datagrid1_CellContentClick_2(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}
