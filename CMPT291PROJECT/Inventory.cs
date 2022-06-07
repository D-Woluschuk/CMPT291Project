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
    public partial class Inventory : Form
    {
        public Employee parent;
        public SqlConnection myconnection;
        public SqlCommand mycommand;
        public SqlDataReader myreader;

        public Inventory(Employee e1, string branchID, DateTime dateFrom, DateTime dateTo)
        {
            InitializeComponent();

            parent = e1;
            myconnection = e1.myconnection;
            myreader = e1.myreader;
            mycommand = e1.mycommand;
            label1.Text = "Branch: " + branchID;
            label2.Text = "Date From: " + dateFrom.ToString();
            label3.Text = "Date To: " + dateTo.ToString();
            mycommand.CommandText = "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'car'";
            myreader.Close();
            myreader = mycommand.ExecuteReader();
            CarInfo.Text = "";
            while (myreader.Read())
            {
                CarInfo.Text += myreader.GetString(0) + "                                 ";
                
            }


        }

        private void InventoryClose(object sender, FormClosedEventArgs e)
        {
            this.parent.Show();
        }
    }
}
