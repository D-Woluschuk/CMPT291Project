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
    public partial class CustomerLookup : Form
    {
        public Employee parent;
        public TextBox custid;
        public SqlConnection myconnection;
        public SqlCommand mycommand;
        public SqlDataReader myreader;
        public string[] provinces = { "Any", "AB", "BC", "SK", "MB", "ON", "QC", "NB", "NS", "PE", "NL", "YT", "NT", "NU"};

        public CustomerLookup(Employee e1, TextBox customerID)
        {
            InitializeComponent();
            parent = e1;
            custid = customerID;
            myconnection = e1.myconnection;
            myreader = e1.myreader;
            mycommand = e1.mycommand;
            province.DataSource = provinces;
            this.AcceptButton = CustLookupSubmit;
            
        }

        private void LookupSubmit(object sender, EventArgs e)
        {
            custLookupResults.Items.Clear();
            mycommand.CommandText = "SELECT * FROM customer WHERE FName like " + "'" + firstName.Text + "%'" + " AND LName like " + "'" + LastName.Text + "%'";

            try
            {
                myreader = mycommand.ExecuteReader();
                while (myreader.Read())
                    custLookupResults.Items.Add(myreader["cust_id"].ToString() + "\t" + myreader["FName"].ToString() + "\t" + myreader["LName"].ToString() + "\t" + myreader["Street"].ToString());
            } catch (SqlException e1) { MessageBox.Show(e1.Message); }

            myreader.Close();
        }

        private void customerSelected(object sender, MouseEventArgs e)
        {
            string[] customer;
            string custID;

            customer = custLookupResults.SelectedItem.ToString().Split('\t');

            custID = customer[0];
            custid.Text = custID;
            DialogResult dialogResult = MessageBox.Show("Is '" + customer[1] + " " + customer[2] + "' the customer you're looking for?", "Confirm", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                this.Close();
                parent.Visible = true;
            }
           
        }

        private void CustLookupCancel(object sender, EventArgs e)
        {
            this.Close();
            parent.Visible = true;
        }
    }
}
