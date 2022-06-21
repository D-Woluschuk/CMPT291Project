using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace CMPT291PROJECT
{
    public partial class UserSignUp : Form
    {
        public Login login;
        public SqlDataReader myreader;
        public SqlConnection myconnection;
        public SqlCommand mycommand;
        public string[] ids;
        public string[] provinces = { "AB", "BC", "SK", "MB", "ON", "QC", "NB", "NS", "PE", "NL", "YT", "NT", "NU" };
        public UserSignUp(Login temp)
        {
            InitializeComponent();
            login = temp;
            signup_branch.Visible = false;
            signup_branch_label.Visible = false;
            branch_error.Visible = false;
            id_error.Visible = false;
            myconnection = temp.myconnection;
            myreader = temp.myreader;
            mycommand = temp.mycommand;
            List<string> list = new List<string>();

            mycommand.CommandText = "Select distinct city, branch_id from branch";
            try
            {
                myreader = mycommand.ExecuteReader();
                while (myreader.Read())
                {
                    signup_branch.Items.Add(myreader["city"].ToString());
                    list.Add(myreader["branch_id"].ToString());
                }
            }
            catch(SqlException ex)
            {
                MessageBox.Show("Cannot Get Cities From DataBase" + ex.Message);
            }
            myreader.Close();
            ids = list.ToArray();
            Signup_Prov.DataSource = provinces;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void UserSignUp_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void submit_Click(object sender, EventArgs e)
        {
            if (signup_id.Text.Length == 0)
            {
                id_error.Text = "Please Enter ID";
                id_error.ForeColor = Color.Red;
                id_error.Visible = true;
                return;
            }
            if (employee_check.Checked)
            {
                if (signup_branch.Text.Length == 0)
                {
                    branch_error.Text = "Please Select Branch";
                    branch_error.ForeColor = Color.Red;
                    branch_error.Visible = true;
                    return;
                }
            }
            //string[] param;
            string command_string = "INSERT INTO ";
            if (employee_check.Checked)
            {
                command_string += "employee Values(";
                string[] param = { signup_id.Text, ids[signup_branch.SelectedIndex], signup_Fname.Text, signup_Lname.Text };
                for (int i = 0; i < param.Length; i++)
                {


                    if (param[i].Length != 0)
                    {
                        command_string += "'" + param[i] + "'";

                    }
                    else
                    {
                        command_string += "Null";
                    }
                    command_string += ", ";
                }
                command_string += "'0')";

            }
            else
            {
                string[] param = { signup_id.Text, signup_Fname.Text, signup_Lname.Text, signup_street.Text, signup_city.Text, Signup_Prov.SelectedItem.ToString() };
                command_string += "customer VALUES (";
                for (int i = 0; i < param.Length; i++)
                {


                    if (param[i].Length != 0)
                    {
                        command_string += "'" + param[i] + "'";

                    }
                    else
                    {
                        command_string += "Null";
                    }
                    command_string += ", ";
                }
                command_string += "0)";

            }
            try
            {
                mycommand.CommandText = command_string;
                mycommand.ExecuteNonQuery();
                MessageBox.Show("Account Created");
            }
            catch (SqlException e1)
            {
                //MessageBox.Show(e1.Message);
                if (e1.Number == 2627)
                {
                    id_error.Text = "User ID Taken";
                    id_error.ForeColor = Color.Red;
                    id_error.Visible = true;
                }
            }
        }

        private void UserSignUp_FormClosing(object sender, FormClosingEventArgs e)
        {
            login.Visible = true;
        }

        private void employee_check_CheckedChanged(object sender, EventArgs e)
        {
            if (employee_check.Checked)
            {
                signup_branch.Visible = true;
                signup_branch_label.Visible = true;
            }
            else
            {
                signup_branch.Visible = false;
                signup_branch_label.Visible = false;
            }
        }

        private void signup_back_Click(object sender, EventArgs e)
        {
            login.Visible = true;
            this.Close();
        }
    }
}
