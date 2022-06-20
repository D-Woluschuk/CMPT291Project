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
    public partial class Customer : Form
    {
        public string s;
        public string default_date;
        public Login parent;
        public SqlConnection myconnection;
        public SqlCommand mycommand;
        public SqlDataReader myreader;
        public Customer(Login ftemp)
        {
            InitializeComponent();
            default_date = date_from.Value.ToString();
            parent = ftemp;


            myconnection = parent.myconnection;
            mycommand = parent.mycommand;
            myreader = parent.myreader;

            custOutput.View = View.Details;
            custOutput.GridLines = true;
            custOutput.Columns.Add("Branch", 130);
            custOutput.Columns.Add("Type", 150);
            custOutput.Columns.Add("Price", 100);

            // Populate combo boxes
            mycommand.CommandText = "SELECT b.city, t.description FROM car c, type t, branch b ";
            mycommand.CommandText += "WHERE c.car_type = t.type_id and c.car_branch = b.branch_id";
            try
            {
                IList<string> branches = new List<string>();
                IList<string> types = new List<string>();
                branches.Add("Any");
                types.Add("Any");

                myreader = mycommand.ExecuteReader();
                while (myreader.Read())
                {
                    branches.Add(myreader["city"].ToString());
                    types.Add(myreader["description"].ToString());
                }
                branches = branches.Distinct().ToList();
                types = types.Distinct().ToList();
                pickup_branch.DataSource = branches;
                vehicle_type.DataSource = types;
            }
            catch (Exception e3) { MessageBox.Show(e3.ToString()); }
            myreader.Close();
        }


        private void submit_Click(object sender, EventArgs e)
        {
            if (date_from.Value > date_to.Value && date_from.Value.ToString() != default_date && date_to.Value.ToString() != default_date)
            {
                custOutput.Items.Clear();
                //output.DataSource = null;
                MessageBox.Show("Date from must be before date to!");
                return;
            }

            // Build Query based off selections
            mycommand.CommandText = "SELECT * FROM car c, type t, branch b WHERE c.car_type = t.type_id and b.branch_id = c.car_branch ";

            if (pickup_branch.SelectedItem.ToString() != "Any")
            {   // If branch is selected add to commandtext
                mycommand.CommandText += "and b.city = '" + pickup_branch.SelectedItem.ToString() + "' ";

            }
            if (vehicle_type.SelectedItem.ToString() != "Any")
            {   //If only a type is selected
                mycommand.CommandText += "and t.description = '" + vehicle_type.SelectedItem.ToString() + "' ";
            }
            if (date_from.Value.ToString() != default_date || date_to.Value.ToString() != default_date)
            {

                // (existing start <= new end) and (new start <= eexisting end)///
                mycommand.CommandText += "and car_id not in (select car_id from Booking b where ";
                mycommand.CommandText += "(b.Date_From <= '" + date_to.Value.ToString() + "') and ('";
                mycommand.CommandText += date_from.Value.ToString() + "' <= b.Date_To))";

            }

            // Display all available vehicles to output

            IList<string> result = new List<string>();
            string temp;
            string[] info = new string[4];
            ListViewItem anItem;
            custOutput.Items.Clear();
            //MessageBox.Show(SqlCommand.CommandText.ToString());
            try
            {
                myreader = mycommand.ExecuteReader();
                while (myreader.Read())
                {
                    info[0] = myreader["city"].ToString();
                    info[1] = myreader["description"].ToString();
                    info[2] = myreader["daily"].ToString();
                    anItem = new ListViewItem(info);
                    custOutput.Items.Add(anItem);
                    //temp = myreader["city"].ToString().PadRight(15) + myreader["description"].ToString().PadRight(15);
                    //temp += myreader["daily"].ToString(); // TODO CALCULATE ACTUAL PRICE
                    //result.Add(temp);
                }
            }
            catch (Exception e3) { MessageBox.Show(e3.ToString()); }

            myreader.Close();
            //output.DataSource = result;
            //output.Text = String.Join("", result.Distinct().ToList());
            //output.Visible = true;


        }

        private void results_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void log_out_Click(object sender, EventArgs e)
        {
            parent.Visible = true;
            this.Close();
        }

        private void Customer_FormClosing(object sender, FormClosingEventArgs e)
        {
            parent.Visible = true;
        }
    }
}
