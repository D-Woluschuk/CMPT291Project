using System;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace CMPT291PROJECT
{
    public partial class Login : Form
    {
        
        public SqlConnection myconnection;
        public SqlCommand mycommand;
        public SqlDataReader myreader;

        public Login()
        {
            InitializeComponent();
            error_text.Visible = false;
            user_id.Visible = false;
            enter_username.Visible = false;
            
            // Establish SQL Connection
            String connection_string = "Server = DESKTOP-OP195PG; Database = Project; Trusted_Connection = yes;";

            SqlConnection myconnection = new SqlConnection(connection_string);
            try
            {
                myconnection.Open();
                mycommand = new SqlCommand();
                mycommand.Connection = myconnection;
               
            } catch(Exception e)
            {
                MessageBox.Show("Cannot Connect To Database: " + e.Message);
                this.Close();
            }
        }



        private void new_user_Click(object sender, EventArgs e)
        {
            UserSignUp new_user = new UserSignUp(this);
            new_user.Show();
            this.Visible = false;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void existing_user_Click(object sender, EventArgs e)
        {
            // Check for no user input
            if (user_id.Text == "" && debug.Checked == false) { 
                error_text.Text = "Please Enter ID";
                error_text.Visible = true;
                return;
            }

            // Build Query based on whether user is an employee or customer
            mycommand.CommandText = "SELECT ";
            if (employee_check.Checked)
            {
                mycommand.CommandText += "emp_id as id FROM employee";
            }
            else if (employee_check.Checked == false)
            {
                mycommand.CommandText += "cust_id as id FROM customer";
            }

            error_text.Visible = false;
            bool logged_in = false;

            try
            {
                myreader = mycommand.ExecuteReader();
                while (myreader.Read())
                {//Check for user_id match 
                    if (myreader["id"].ToString() == user_id.Text || debug.Checked)
                    {
                        myreader.Close();
                        logged_in = true;
                        if (employee_check.Checked == false)
                        {

                            Customer c1 = new Customer(this);
                            c1.Show();
                            this.Visible = false;
                            return;
                        }
                        if (employee_check.Checked)
                        {
                            Employee employee = new Employee(this);
                            this.Visible = false;
                            employee.Show();
                            return;
                        }
                    }
                }
            }catch(Exception e3) { MessageBox.Show(e3.ToString()); }

            myreader.Close();

            if (logged_in == false) { error_text.Text = "Invalid Username"; error_text.Visible = true; }

        }

        private void employee_check_CheckedChanged(object sender, EventArgs e)
        {
            if (employee_check.Checked)
            {
                user_id.Visible = true;
                enter_username.Visible = true;
            }
            else
            {
                user_id.Visible = false;
                enter_username.Visible = false;
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
    }
}
