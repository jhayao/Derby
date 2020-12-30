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
    public partial class Weight_Difference : Form
    {
        Connection con = new Connection();
        public Weight_Difference()
        {
            InitializeComponent();
        }

        private void bunifuLabel1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void unmatch()
        {
            try
            {
                con.connectDB.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "select * from entry where id not in (select entry1 from matches) and id not in (SELECT entry2 from matches)";
                
                cmd.Connection = con.connectDB;
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                Matches.form1.datagrid2.DataSource = dt;
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
        private void unmatch__load()
        {
            try
            {
                con.connectDB.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "SELECT entry.ID as 'ID' , entry.name as 'Entry Name',  entry.weight as 'Weight', entry.wing_band as 'Wing Band', entry.color as 'Color' from unmatch inner join entry on entry.id =unmatch.entry"; 
                cmd.Connection = con.connectDB;
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                Matches.form1.datagrid2.DataSource = dt;
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
        private void find_match()
        {
            try
            {
                con.connectDB.Open();

                MySqlCommand cmds = new MySqlCommand();
                cmds.CommandText = "SELECT COUNT(DISTINCT(name)) as 'name' FROM `entry`";
                cmds.Connection = con.connectDB;
                int count = Convert.ToInt32(cmds.ExecuteScalar());
                for (int x = 0; x < count; x++)
                {
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = "SELECT DISTINCT(name) FROM `entry`";
                    cmd.Connection = con.connectDB;
                    DataTable dt = new DataTable();
                    dt.Load(cmd.ExecuteReader());
                    int limit = Int32.Parse(this.dropdown1.GetItemText(this.dropdown1.SelectedItem));
                    MySqlCommand cmd2;
                    MySqlCommand cmd3;
                    MySqlCommand cmd4;
                    MySqlCommand cmd5;
                    MySqlCommand cmd6;

                    foreach (DataRow row in dt.Rows)
                    {
                        cmd6 = new MySqlCommand();
                        cmd6.CommandText = "select * from entry where id not in (select entry1 from matches) and id not in (SELECT entry2 from matches) and name =@name";
                        cmd6.Connection = con.connectDB;
                        cmd6.Parameters.Add("@name", MySqlDbType.String).Value = row["name"];
                        DataTable dts = new DataTable();
                        dts.Load(cmd6.ExecuteReader());
                        foreach (DataRow rows in dts.Rows)
                        {
                            DataTable dt2 = new DataTable();
                            DataTable dt3;
                            DataTable dt4;
                            string Options = Home.form1.Option;
                            

                            MySqlCommand weights = new MySqlCommand();
                            weights.CommandText = "SELECT maximum  FROM `inititalweight`";
                            weights.Connection = con.connectDB;
                            int maxWeight = Convert.ToInt32(weights.ExecuteScalar());
                            if (Int32.Parse(rows["weight"].ToString()) < maxWeight)
                            {
                                cmd2 = new MySqlCommand();


                                cmd2.CommandText = "SELECT * FROM `entry` WHERE (weight BETWEEN @weight1 and @weight2) and name != @name and weight <@maxWeight ";
                                cmd2.Connection = con.connectDB;
                                cmd2.Parameters.Add("@weight1", MySqlDbType.Int32).Value = Int32.Parse(rows["weight"].ToString()) - limit;
                                cmd2.Parameters.Add("@weight2", MySqlDbType.Int32).Value = Int32.Parse(rows["weight"].ToString()) + limit;
                                cmd2.Parameters.Add("@name", MySqlDbType.VarChar).Value = rows["name"].ToString();
                                cmd2.Parameters.Add("@maxWeight", MySqlDbType.Int32).Value = maxWeight;
                                dt2.Load(cmd2.ExecuteReader());
                            }
                            else if (Int32.Parse(rows["weight"].ToString()) >= maxWeight)
                            {
                                cmd2 = new MySqlCommand();
                                cmd2.CommandText = "SELECT * FROM `entry` WHERE  name != @name and weight > @maxWeight ";
                                cmd2.Connection = con.connectDB;
                                cmd2.Parameters.Add("@name", MySqlDbType.VarChar).Value = rows["name"].ToString();
                                cmd2.Parameters.Add("@maxWeight", MySqlDbType.Int32).Value = maxWeight;
                                dt2.Load(cmd2.ExecuteReader());
                            }
                            foreach (DataRow row2 in dt2.Rows)
                            {
                                cmd3 = new MySqlCommand();
                                cmd4 = new MySqlCommand();
                                cmd5 = new MySqlCommand();
                                cmd5.CommandText = "SELECT * FROM no_fight WHERE (entry1 = '" + rows["name"] + "' and entry2= '" + row2["name"] + "') or (entry2 = '" + rows["name"] + "' and entry1 = '" + row2["name"] + "')";
                                cmd5.Connection = con.connectDB;
                                cmd4.CommandText = "select * from matches where (entry1 = @name or entry2 = @name) or (entry1 = @name2 or entry2 = @name2)";
                                cmd4.Parameters.Add("@name", MySqlDbType.Int32).Value = rows["id"];
                                cmd4.Parameters.Add("@name2", MySqlDbType.Int32).Value = row2["id"];
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
                                        cmd3.Parameters.Add("@entry1", MySqlDbType.Int32).Value = rows["id"];
                                        cmd3.Parameters.Add("@entry2", MySqlDbType.Int32).Value = row2["id"];
                                        cmd3.Connection = con.connectDB;

                                        int rowsAffected = cmd3.ExecuteNonQuery();
                                        break;

                                    }
                                }

                            }
                            break;
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
                Matches.form1.load_fight();
                unmatch();
            }
        }
        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            try
            {
                con.connectDB.Open();
                DialogResult dialogResult = MessageBox.Show("Are you sure?", "Previus Matches will be cleared", MessageBoxButtons.YesNo,MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.Yes)
                {
                    MySqlCommand trun = new MySqlCommand();
                    trun.CommandText = "truncate table matches";
                    trun.Connection = con.connectDB;
                    trun.ExecuteNonQuery();
                    con.connectDB.Close();
                    /*MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = "select * from entry";
                    cmd.Connection = con.connectDB;
                    DataTable dt = new DataTable();
                    dt.Load(cmd.ExecuteReader());
                    int limit = Int32.Parse(this.dropdown1.GetItemText(this.dropdown1.SelectedItem));
                    MySqlCommand cmd2;
                    MySqlCommand cmd3;
                    MySqlCommand cmd4;
                    MySqlCommand cmd5;
                    foreach (DataRow row in dt.Rows)
                    {
                        DataTable dt2 = new DataTable();
                        DataTable dt3;
                        DataTable dt4;
                        string Options = Home.form1.Option;
                        int maxWeight = 0;

                        if (Options == "Cock")
                        {
                            maxWeight = 2400;
                        }
                        else
                        {
                            maxWeight = 2250;
                        }
                        if (Int32.Parse(row["weight"].ToString()) < maxWeight)
                        {
                            cmd2 = new MySqlCommand();


                            cmd2.CommandText = "SELECT * FROM `entry` WHERE (weight BETWEEN @weight1 and @weight2) and name != @name and weight <=@maxWeight ";
                            cmd2.Connection = con.connectDB;
                            cmd2.Parameters.Add("@weight1", MySqlDbType.Int32).Value = Int32.Parse(row["weight"].ToString()) - limit;
                            cmd2.Parameters.Add("@weight2", MySqlDbType.Int32).Value = Int32.Parse(row["weight"].ToString()) + limit;
                            cmd2.Parameters.Add("@name", MySqlDbType.VarChar).Value = row["name"].ToString();
                            cmd2.Parameters.Add("@maxWeight", MySqlDbType.Int32).Value = maxWeight;
                            dt2.Load(cmd2.ExecuteReader());
                        }
                        else if (Int32.Parse(row["weight"].ToString()) > maxWeight)
                        {
                            cmd2 = new MySqlCommand();
                            cmd2.CommandText = "SELECT * FROM `entry` WHERE  name != @name and weight > @maxWeight ";
                            cmd2.Connection = con.connectDB;
                            cmd2.Parameters.Add("@name", MySqlDbType.VarChar).Value = row["name"].ToString();
                            cmd2.Parameters.Add("@maxWeight", MySqlDbType.Int32).Value = maxWeight;
                            dt2.Load(cmd2.ExecuteReader());
                        }
                        foreach (DataRow row2 in dt2.Rows)
                        {
                            cmd3 = new MySqlCommand();
                            cmd4 = new MySqlCommand();
                            cmd5 = new MySqlCommand();
                            cmd5.CommandText = "SELECT * FROM no_fight WHERE (entry1 = '" + row["name"] + "' and entry2= '" + row2["name"] + "') or (entry2 = '" + row["name"] + "' and entry1 = '" + row2["name"] + "')";
                            cmd5.Connection = con.connectDB;
                            cmd4.CommandText = "select * from matches where (entry1 = @name or entry2 = @name) or (entry1 = @name2 or entry2 = @name2)";
                            cmd4.Parameters.Add("@name", MySqlDbType.Int32).Value = row["id"];
                            cmd4.Parameters.Add("@name2", MySqlDbType.Int32).Value = row2["id"];
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
                                    cmd3.Parameters.Add("@entry1", MySqlDbType.Int32).Value = row["id"];
                                    cmd3.Parameters.Add("@entry2", MySqlDbType.Int32).Value = row2["id"];
                                    cmd3.Connection = con.connectDB;

                                    int rowsAffected = cmd3.ExecuteNonQuery();

                                }
                            }

                        }

                    }
                    }
                    else
                    {
                        this.Hide();
                    }
                    */
                    find_match();
                    }
                }
            catch (Exception ex)
            {
                MessageBox.Show("Something Error Found" + ex.ToString());

            }
            finally
            {
                con.connectDB.Close();
                Matches.form1.load_fight();
                unmatch();
                this.Hide();
            }
        }
        
        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            find_match();
        }
    }
}
