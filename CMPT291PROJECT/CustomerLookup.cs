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
            custLookup.View = View.Details;

            custLookup.Columns.Add("Customer ID", 145);
            custLookup.Columns.Add("First Name", 200);
            custLookup.Columns.Add("Last Name", 200);
            custLookup.Columns.Add("Street", 175);
            custLookup.Columns.Add("City", 168);
            custLookup.Columns.Add("Province", 106);
            custLookup.Columns.Add("Gold Status", 140);

        }

        private void LookupSubmit(object sender, EventArgs e)
        {
            custLookup.Items.Clear();
            string[] aCust = new string[8];
            string[] cols = { "cust_ID", "FName", "LName", "Street", "City", "Province", "Gold_status" };
            ListViewItem anItem;
            mycommand.CommandText = "SELECT * FROM customer WHERE ";
            mycommand.CommandText += "FName like '" + firstName.Text + "%'";
            mycommand.CommandText += " AND LName like '" + LastName.Text + "%'";
            mycommand.CommandText += " AND Street like '" + Street.Text + "%'";
            mycommand.CommandText += " AND City like '" + City.Text + "%' AND (";

            if (!province.Text.Equals("Any"))
                mycommand.CommandText += "Province = '" + province.SelectedItem.ToString() + "')";
            else
            {
                for (int i = 1; i < provinces.Length; i++)
                {
                    mycommand.CommandText += "Province = '";
                    mycommand.CommandText += provinces[i] + "'";
                    if (i + 1 == provinces.Length)
                    {
                        mycommand.CommandText += ")";
                        break;
                    }
                    else
                    {
                        mycommand.CommandText += " OR ";
                    }
                }
            }

            try
            {
                myreader = mycommand.ExecuteReader();
                while (myreader.Read())
                {
                    aCust[0] = myreader["cust_id"].ToString();
                    aCust[1] = myreader["FName"].ToString();
                    aCust[2] = myreader["LName"].ToString();
                    aCust[3] = myreader["Street"].ToString();
                    aCust[4] = myreader["City"].ToString();
                    aCust[5] = myreader["Province"].ToString();
                    aCust[6] = myreader["Gold_status"].ToString();

                    anItem = new ListViewItem(aCust);
                    custLookup.Items.Add(anItem);
                }
            } catch (SqlException e1) { MessageBox.Show(e1.Message); }

            myreader.Close();

            if (custLookup.Items.Count == 0)
                custLookup.Items.Add("No Results");
        }

        private void customerSelected(object sender, MouseEventArgs e)
        {
            string custID;
            string[] aCust = new string[8];
            if (custLookup.SelectedItems != null)
            {

                ListViewItem anItem = custLookup.SelectedItems[0];

                aCust[0] = anItem.SubItems[0].Text;
                aCust[1] = anItem.SubItems[1].Text;
                aCust[2] = anItem.SubItems[2].Text;
                aCust[3] = anItem.SubItems[3].Text;
                aCust[4] = anItem.SubItems[4].Text;
                aCust[5] = anItem.SubItems[5].Text;
                aCust[6] = anItem.SubItems[6].Text;

            }
            else
                return;

            custID = aCust[0]; 
            custid.Text = custID;

            DialogResult dialogResult = MessageBox.Show("Is '" + aCust[1] + " " + aCust[2] + "' the customer you're looking for?", "Confirm", MessageBoxButtons.YesNo);
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
