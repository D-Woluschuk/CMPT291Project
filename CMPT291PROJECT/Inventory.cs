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

        public Inventory(Employee e1, string branchID, string dateFrom, string dateTo)
        {
            InitializeComponent();


            //Converting datetimepicker values to 'yyyy-MM-dd' format for query
            string convertDateFrom;
            string convertDateTo;
            TypeConverter converter = new TypeConverter();           
            DateTime date_from = (DateTime)TypeDescriptor.GetConverter(typeof(DateTime)).ConvertFromString(dateFrom);
            DateTime date_to = (DateTime)TypeDescriptor.GetConverter(typeof(DateTime)).ConvertFromString(dateTo);
            convertDateFrom = date_from.ToString("yyyy-MM-dd");
            convertDateTo = date_to.ToString("yyyy-MM-dd");



            parent = e1;
            myconnection = e1.myconnection;
            myreader = e1.myreader;
            mycommand = e1.mycommand;
            label1.Text = "Branch: " + branchID;
            label2.Text = "Date From: " + convertDateFrom.ToString();
            label3.Text = "Date To: " + convertDateTo.ToString();
            CurrentInventory.View = View.Details;
            CurrentInventory.GridLines = true;
            CurrentInventory.FullRowSelect = true;
            
            myreader.Close();
            CurrentInventory.Columns.Add("Car ID", 175);
            CurrentInventory.Columns.Add("Type", 250);
            CurrentInventory.Columns.Add("Model", 250);
            CurrentInventory.Columns.Add("Year",190);
            CurrentInventory.Columns.Add("Licence Plate", 256);

            mycommand.CommandText = "SELECT C.car_id, T.description, C.model, C.year, C.plate_num FROM car C, type T, branch B1 WHERE (C.car_type = T.type_id AND C.car_branch = B1.branch_id) AND B1.city = '" + branchID + "' AND C.car_id not in (SELECT B.car_id FROM booking B WHERE B.date_from <= '" + convertDateFrom + "' AND B.date_to >= '" + convertDateTo + "')";

            string[] carInfo = new string[6];
            ListViewItem anItem;
            try
            { 
                myreader = mycommand.ExecuteReader();
                while (myreader.Read())
                {
                    carInfo[0] = myreader["car_id"].ToString();
                    carInfo[1] = myreader["description"].ToString();
                    carInfo[2] = myreader["model"].ToString();
                    carInfo[3] = myreader["year"].ToString();
                    carInfo[4] = myreader["plate_num"].ToString();
                    anItem = new ListViewItem(carInfo);
                    CurrentInventory.Items.Add(anItem);
                }

            } catch(Exception e) { MessageBox.Show(e.ToString()); }

            myreader.Close();

        }

        private void InventoryClose(object sender, FormClosedEventArgs e)
        {
            this.parent.Show();
        }

        private void BackInv(object sender, EventArgs e)
        {
            this.Close();
            this.parent.Show();
        }

    }
}
