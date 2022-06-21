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
using System.Globalization;

namespace CMPT291PROJECT
{
    public partial class Employee : Form
    {
        public Login parent;
        public SqlConnection myconnection;
        public SqlCommand mycommand;
        public SqlDataReader myreader;
        private Rectangle myTabRect;
        public IAsyncResult asyncResult;
        IList<string> arguments = new List<string>();
        public string default_date;
        public DateTime default_date_to;
        public string[] tabNames = { "Booking", "Return", "Inventory", "Reports", "Add Car", "Remove Car", "Edit Car" };
        public Employee(Login f1)
        {
            //booking id, car id, description, platenum, date from, date to, branch from, type requested, price
            InitializeComponent();
            //myTabRect = tabControl1.GetTabRect(0);
            //tabControl1.TabPages[0].Text = "Booking";
            DoubleBuffered = true;
            default_date = date_from.Text;
            date_to.Value = (date_from.Value.AddDays(1));
            default_date_to = date_to.Value;
            user_info.Visible = false;

            invDateTo.Value = default_date_to;
            // Inherit SQL Connection
            parent = f1;
            myconnection = f1.myconnection;
            myreader = f1.myreader;
            mycommand = f1.mycommand;
            //editOutput.GridLines = true;
            //editOutput.FullRowSelect = true;
            returnOutputBox.View = View.Details;

            returnOutputBox.Columns.Add("Booking ID");
            returnOutputBox.Columns.Add("Car ID");
            returnOutputBox.Columns.Add("Description");
            returnOutputBox.Columns.Add("Licence Plate");
            returnOutputBox.Columns.Add("Date From");
            returnOutputBox.Columns.Add("Date To");
            returnOutputBox.Columns.Add("Branch From");
            returnOutputBox.Columns.Add("Type Requested");
            returnOutputBox.Columns.Add("Price");

            editOutput.View = View.Details;
            editOutput.Columns.Add("Car ID", 70);
            editOutput.Columns.Add("Type", 105);
            editOutput.Columns.Add("Branch", 120);
            editOutput.Columns.Add("Model", 132);
            editOutput.Columns.Add("Year", 70);
            editOutput.Columns.Add("Licence Plate", 130);


            removeOutput.View = View.Details;

            removeOutput.Columns.Add("Car ID", 70);
            removeOutput.Columns.Add("Type", 105);
            removeOutput.Columns.Add("Branch", 120);
            removeOutput.Columns.Add("Model", 132);
            removeOutput.Columns.Add("Year", 70);
            removeOutput.Columns.Add("Licence Plate", 130);

            bookingOutput.View = View.Details;
            bookingOutput.GridLines=true;
            bookingOutput.FullRowSelect = true;

            bookingOutput.Columns.Add("Branch", 120);
            bookingOutput.Columns.Add("Type", 105);
            bookingOutput.Columns.Add("Model", 132);
            bookingOutput.Columns.Add("Year", 70);
            bookingOutput.Columns.Add("Plate", 130);
            bookingOutput.Columns.Add("Price", 90);

            reportOutputBox.View = View.Details;
            report_branch.Visible = false;
            report_type.Visible = false;
            report_datefrom.Visible = false;
            report_dateto.Visible = false;
            report_submit.Visible = false;
            label9.Visible = false;
            label10.Visible = false;
            label11.Visible = false;
            label12.Visible = false;

            //report_filters.Click += new EventHandler(ReportFilters_Click);

            //returnHeading.Text = "Booking ID\t" + "\t" + "Customer ID" + "\t" + "Car ID";

            // Populate report branch combobox
            mycommand.CommandText = "SELECT DISTINCT city FROM branch";

            myreader.Close();
            myreader = mycommand.ExecuteReader();
            while (myreader.Read())
            {
                report_branch.Items.Add(myreader["city"].ToString());
            }

            // Populate report type combobox
            myreader.Close();
            mycommand.CommandText = "SELECT DISTINCT description from type";
            myreader = mycommand.ExecuteReader();
            while (myreader.Read())
            {
                report_type.Items.Add(myreader["description"].ToString());
            }
            myreader.Close();

            // Populate combo boxes
            mycommand.CommandText = "SELECT B.city, T.description FROM branch B, type T";
            try
            {
                IList<string> branches = new List<string>();
                IList<string> types = new List<string>();
               

                myreader = mycommand.ExecuteReader();
                while (myreader.Read())
                {
                    branches.Add(myreader["city"].ToString());
                    types.Add(myreader["description"].ToString());
                }
                branches = branches.Distinct().ToList();
                types = types.Distinct().ToList();

                addcar_branch.DataSource = branches;
                InventoryBranch.DataSource = branches;
                addcar_type.DataSource = types;
                return_dropoff.DataSource = branches;



                branches.Insert(0, "Any");
                types.Insert(0, "Any");
                remcar_branch.DataSource = branches;
                remcar_type.DataSource = types;
                edit_type.DataSource = types;
                edit_branch.DataSource = branches;
                report_type.DataSource = types;
                report_branch.DataSource = branches;

                pickup.DataSource = branches;
                dropoff.DataSource = branches;
                
                vehicle_type.DataSource = types;
                
            }
            
            catch (Exception e3) { MessageBox.Show(e3.ToString()); }
            myreader.Close();
            
            
        
           
        }

        private void onDrawItem(object sender, DrawItemEventArgs e)
        {
            //string[] tabNames = { "Booking", "Return", "Inventory", "Reports", "Add Car", "Remove Car", "Edit Car" };
            Font font = e.Font;
            Graphics g = e.Graphics;
            Pen p = new Pen(Color.Black);
            Brush b = new SolidBrush(Color.Black);
            Brush b1 = new SolidBrush(Color.DarkGray);
            Brush b2 = new SolidBrush(Color.LightBlue);
            Brush b3 = new SolidBrush(Color.Brown);
            //sender.Equals(tabControl1.SelectedTab);

                
            myTabRect = tabControl1.GetTabRect(tabControl1.SelectedIndex);
            //if (myTabRect.Contains(PointToClient(Cursor.Position))){
                    //g.FillRectangle(b3 , myTabRect);

            //}
           // else
            //{
            resetTabs(tabControl1.SelectedIndex, g, font, p, b, b1, b2);
            //}
                //g.FillRectangle(b1, myTabRect);
                //g.DrawString(tabNames[i], font, b, myTabRect);
                //g.DrawRectangle(p, myTabRect);

                //if (tabControl1.SelectedTab == tabPage1)
                //{
               
                    //myTabRect = tabControl1.GetTabRect(tabControl1.SelectedIndex);
                    //myTabRect.Inflate(Width, Height);
                    //g.FillRectangle(b2, myTabRect);
                    //return;
                //}
            

        }

        private void resetTabs(int selectedIndex, Graphics graphics, Font font, Pen pen, Brush brush, Brush brush1, Brush brush2)
        {
            for (int i = 0; i < tabControl1.TabCount; i++)
            {
                if (i != selectedIndex)
                {
                    myTabRect = tabControl1.GetTabRect(i);

                    graphics.FillRectangle(brush1, myTabRect);
                    graphics.DrawString(tabNames[i], font, brush, myTabRect);
                    graphics.DrawRectangle(pen, myTabRect);
                }
                else
                {
                    myTabRect = tabControl1.GetTabRect(selectedIndex);
                    graphics.FillRectangle(brush2, myTabRect);
                    graphics.DrawString(tabNames[selectedIndex], font, brush, myTabRect);
                    graphics.DrawRectangle(pen, myTabRect);
                }

            }


        }

        //Displays the filters
        void ReportFilters_Click(object sender, EventArgs e)
        {
            report_branch.Visible = false;
            report_type.Visible = false;
            report_datefrom.Visible = false;
            report_dateto.Visible = false;
            report_submit.Visible = false;
            label9.Visible = false;
            label10.Visible = false;
            label11.Visible = false;
            label12.Visible = false;
            if (radioButton5.Checked == true)
            {
                reportOutputBox.Columns.Clear();
                reportOutputBox.Visible = false;
                report_submit.Visible = true;
                noFilters.Visible = true;
                //MessageBox.Show("No filters available");
            }
            else if (radioButton1.Checked == true)
            {
                reportOutputBox.Columns.Clear();
                reportOutputBox.Visible = false;
                report_branch.Visible = true;
                report_type.Visible = true;
                report_datefrom.Visible = true;
                report_dateto.Visible = true;
                report_submit.Visible = true;
                label9.Visible = true;
                label10.Visible = true;
                label11.Visible = true;
                label12.Visible = true;
                noFilters.Visible = false;
            }
            else if (radioButton2.Checked == true)
            {
                reportOutputBox.Columns.Clear();
                reportOutputBox.Visible = false;
                report_branch.Visible = true;
                label9.Visible = true;
                report_submit.Visible = true;
                noFilters.Visible = false;
            }
            else if (radioButton3.Checked == true)
            {
                reportOutputBox.Columns.Clear();
                reportOutputBox.Visible = false;
                report_branch.Visible = true;
                label9.Visible = true;
                report_submit.Visible = true;
                noFilters.Visible = false;
            }
            else if (radioButton4.Checked == true)
            {
                reportOutputBox.Columns.Clear();
                reportOutputBox.Visible = false;
                report_branch.Visible = true;
                report_type.Visible = true;
                label9.Visible = true;
                label10.Visible = true;
                report_submit.Visible = true;
                noFilters.Visible = false;

            }
        }


        private void Employee_FormClosing(object sender, FormClosingEventArgs e)
        {
            parent.Visible = true;
        }

        private void booking_submit_click(object sender, EventArgs e)
        {
            bool gold = false;
            bookingOutput.Items.Clear();
            if (date_from.Value > date_to.Value)
            {
                MessageBox.Show("Date From must be before Date To!");
                return;
            }

            // Verify customer ID input and display information on customer
            if (user_id.Text.Length == 0)
            {
                user_info.Text = "Please Enter Customer ID";
                user_info.ForeColor = Color.Red;
            }
            else
            {
                mycommand.CommandText = "SELECT * FROM customer WHERE cust_id = '" + user_id.Text.ToString() + "'";
                int i = 0;
                try
                {
                    myreader = mycommand.ExecuteReader();
                    while (myreader.Read())
                    {
                        i++;
                        user_info.ForeColor = Color.Black;
                        user_info.Text = myreader["fname"].ToString() + " " + myreader["Lname"].ToString() + "\n";
                        user_info.Text += myreader["street"].ToString() + " " + myreader["city"].ToString() + " " + myreader["province"];
                        if (myreader["Gold_status"].ToString() == "True") { user_info.Text += "\nGold Status"; gold = true; }
                    }
                }
                catch (Exception e3)
                {
                    MessageBox.Show(e3.Message.ToString());
                    user_info.ForeColor = Color.Red;
                    user_info.Text = "Customer Not Found";
                }
                myreader.Close();
            }
            user_info.Visible = true;

            // Get information on all cars fitting choices
            mycommand.CommandText = "SELECT * FROM car c, type t, branch b WHERE c.car_type = t.type_id and c.car_branch = b.branch_id ";
            if (pickup.SelectedItem.ToString() != "Any")
            {
                mycommand.CommandText += "and b.city = '" + pickup.SelectedItem.ToString() + "' ";
            }
            if (vehicle_type.SelectedItem.ToString() != "Any")
            {
                mycommand.CommandText += "and (t.description = '" + vehicle_type.SelectedItem.ToString() + "' ";

                if (gold)
                {
                    mycommand.CommandText += "or t.type_id >= '" + descToType(vehicle_type.SelectedItem.ToString()) + "') ";
                }
                else
                {
                    mycommand.CommandText += ") ";
                }
            }
            if (date_from.Value.ToString() != default_date || date_to.Value.ToString() != default_date)
            {
                //Get all bookings interfering with dates selected, get car_id not in that list
                // (existing start <= new end) and (new start <= existing end) Then they overlap //
                mycommand.CommandText += "and car_id not in (select car_id from Booking b where ";
                mycommand.CommandText += "(b.Date_From <= '" + date_to.Value.ToString() + "') and ('";
                mycommand.CommandText += date_from.Value.ToString() + "' <= b.Date_To)) ";              //TODO Check if date formats are compatible

            }
            mycommand.CommandText += "ORDER BY type_id";
            Clipboard.SetText(mycommand.CommandText);

           
            // Display all available vehicles to output
            //IList<string> result = new List<string>();
            //string temp;
            //float cost = 0;
            string[] aCar = new string[7];
            ListViewItem anItem;
            int o = 0;
            try
            {
                myreader = mycommand.ExecuteReader();
                while (myreader.Read())
                {
                    if (myreader["description"].ToString() != vehicle_type.SelectedItem.ToString() && vehicle_type.SelectedItem.ToString() != "Any")
                    {
                        if (o == 0)
                        {
                            MessageBox.Show("Free Upgrade Available");
                        }
                        else
                        {
                            continue;
                        }
                    }
                    
                    aCar[0] = myreader["city"].ToString();
                    aCar[1] = myreader["description"].ToString();
                    aCar[2] = myreader["model"].ToString();
                    aCar[3] = myreader["year"].ToString();
                    aCar[4] = myreader["plate_num"].ToString();
                    aCar[5] = calculate_price(date_from.Value, date_to.Value, myreader["daily"].ToString(), myreader["weekly"].ToString(), myreader["monthly"].ToString()).ToString();
                    
                    anItem = new ListViewItem(aCar);
                    bookingOutput.Items.Add(anItem);
                    o++;

                    //temp = myreader["city"].ToString() + "\t" + myreader["description"].ToString() + "\t";
                    //temp += myreader["model"].ToString() + "\t" + myreader["year"].ToString() + "\t";
                    //cost = calculate_price(date_from.Value, date_to.Value, myreader["daily"].ToString(), myreader["weekly"].ToString(), myreader["monthly"].ToString());

                    //temp += myreader["plate_num"].ToString() + "\t" + cost.ToString();
                    //result.Add(temp);
                }
            }
            catch (Exception e3) { MessageBox.Show(e3.ToString()); }

            myreader.Close();
           

        }

        private float calculate_price(DateTime date_from, DateTime date_to, string d, string w, string m, bool late = false)
        {
            float total_price = 0;
            int total_days = (date_to - date_from).Days + 1;
            float daily = float.Parse(d), weekly = float.Parse(w), monthly = float.Parse(m), late_fee = 0;
            total_price += ((total_days / 30) * monthly);
            total_days = total_days % 30;
            total_price += ((total_days / 7) * weekly);
            total_days = total_days % 7;
            total_price += (total_days * daily);

            if (late)
            {
                total_price += late_fee;
            }

            return total_price;
        }
        private void update_status(string userid)
        {
            bool gold = false;
            mycommand.CommandText = "SELECT count(*) as total FROM booking WHERE cust_id = '" + userid + "'";
            mycommand.CommandText += " And year(date_from) = " + DateTime.Now.Year.ToString();
            Clipboard.SetText(mycommand.CommandText);
            try
            {
                myreader = mycommand.ExecuteReader();
                while (myreader.Read())
                {
                    if (Int32.Parse(myreader["total"].ToString()) >= 3)
                    {
                        gold = true;
                    }
                }
            }
            catch (SqlException ex) { MessageBox.Show(ex.Message); }
            myreader.Close();

            if (gold)
            {
                mycommand.CommandText = "UPDATE customer SET gold_status = 1 WHERE cust_id = '" + userid + "'";
                
            }
            else
            {
                mycommand.CommandText = "UPDATE customer SET gold_status = 0 WHERE cust_id = '" + userid + "'";
            }
            try
            {
                mycommand.ExecuteNonQuery();
            }
            catch (SqlException ex) { MessageBox.Show(ex.Message); }
            myreader.Close();
        }
        private float calc_price(DateTime date_from, DateTime date_to, string type_id, bool late = false, bool change = false)
        {
            float total_price = 0;
            int total_days = (date_to - date_from).Days;
            float daily = 0, weekly = 0, monthly = 0, late_fee = 0, change_fee = 0;

            mycommand.CommandText = "SELECT daily, weekly, monthly, late_fee, change FROM type WHERE type_id = '" + type_id + "'";
            try
            {
                myreader = mycommand.ExecuteReader();
                while (myreader.Read())
                {
                    daily = float.Parse(myreader["daily"].ToString());
                    weekly = float.Parse(myreader["weekly"].ToString());
                    monthly = float.Parse(myreader["monthly"].ToString());
                    late_fee = float.Parse(myreader["late_fee"].ToString());
                    change_fee = float.Parse(myreader["change"].ToString());
                }

            }
            catch (SqlException ex) { MessageBox.Show("Cannot find price of " + type_id + ex.Message); }
            myreader.Close();


            total_price += ((total_days / 30) * monthly);
            total_days = total_days % 30;
            total_price += ((total_days / 7) * weekly);
            total_days = total_days % 7;
            total_price += (total_days * daily);


            if (late)
            {
                total_price += late_fee;
            }
            if (change)
            {
                total_price += change_fee;
            }

            return total_price;
        }



        private bool get_status(string userid)
        {
            bool status = false;
            mycommand.CommandText = "SELECT gold_status FROM customer WHERE cust_id = '" + userid + "'";
            try
            {
                myreader = mycommand.ExecuteReader();
                while (myreader.Read())
                {
                    if (myreader["gold_status"].ToString() == "1")
                    {
                        status = true;
                    }
                }
            }
            catch (SqlException ex) { MessageBox.Show(ex.Message); }
            myreader.Close();
            return status;
        }
        
        private void booking_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            // Verify customer ID input and display information on customer
            if (user_id.Text.Length == 0)
            {
                user_info.Text = "Please Enter Customer ID";
                user_info.ForeColor = Color.Red;
                return;
            }
            mycommand.CommandText = "SELECT * FROM customer WHERE cust_id = '" + user_id.Text.ToString() + "'";
            int i = 0;
            try
            {
                myreader = mycommand.ExecuteReader();
                while (myreader.Read())
                {
                    i++;
                    user_info.ForeColor = Color.Black;
                    user_info.Text = myreader["fname"].ToString() + " " + myreader["Lname"].ToString() + "\n";
                    user_info.Text += myreader["street"].ToString() + " " + myreader["city"].ToString() + " " + myreader["province"];
                    if (myreader["Gold_status"].ToString() == "True") { user_info.Text += "\nGold Status"; }
                }
            }
            catch (Exception e3)
            {
                MessageBox.Show(e3.Message.ToString());
                user_info.ForeColor = Color.Red;
                user_info.Text = "Customer Not Found";
                return;
            }
            myreader.Close();
            if (i == 0) { user_info.ForeColor = Color.Red; user_info.Text = "Customer Not Found"; return; }

            if (bookingOutput.SelectedItems != null)
            {
                string[] aCar = new string[6];

                ListViewItem aSelection = bookingOutput.SelectedItems[0];

                aCar[0] = (aSelection.SubItems[0].Text);
                aCar[1] = (aSelection.SubItems[1].Text);
                aCar[2] = (aSelection.SubItems[2].Text);
                aCar[3] = (aSelection.SubItems[3].Text);
                aCar[4] = (aSelection.SubItems[4].Text);
                aCar[5] = (aSelection.SubItems[5].Text);

                //string[] args = booking_output.SelectedItem.ToString().Split('\t');
                string selected_car_id = get_car_id_from_plate(aCar[4]);
                //MessageBox.Show(selected_car_id);
                string branch_id = "", type_id = "";

                // Get type_id and branch_id from selected text
                mycommand.CommandText = "SELECT DISTINCT type_id, branch_id FROM type, branch WHERE type.description = '" + aCar[1];
                mycommand.CommandText += "' and branch.city = '" + aCar[0] + "'";
                //MessageBox.Show(mycommand.CommandText.ToString());

                //mycommand.CommandText = "SELECT DISTINCT type_id, branch_id FROM type, branch WHERE type.description = '" + args[1];
                //mycommand.CommandText += "' and branch.city = '" + args[0] + "'";
                //Clipboard.SetText(mycommand.CommandText);
                try
                {
                    myreader = mycommand.ExecuteReader();
                    while (myreader.Read())
                    {
                        branch_id = myreader["branch_id"].ToString();
                        type_id = myreader["type_id"].ToString();
                    }
                    myreader.Close();
                }
                catch
                {
                    MessageBox.Show("Cannot Find Car In DB");
                }
                string message = "Create booking: " + aCar[1] + " From " + date_from.Text + " To " + date_to.Text + " For " + aCar[5];


                //string message = "Create booking: " + args[1].Trim() + " From " + date_from.Text + " To " + date_to.Text + " For " + args[5];
                DialogResult confirm = MessageBox.Show(message, "Confirm Booking", MessageBoxButtons.YesNo);
                if (confirm == DialogResult.Yes)
                {
                    // Create insertion command and commit to database
                    string new_id = get_new_id("booking");
                    mycommand.CommandText = "INSERT INTO booking VALUES('" + new_id + "', '";
                    mycommand.CommandText += user_id.Text + "', '" + selected_car_id + "', '" + date_from.Text + "', '";
                    mycommand.CommandText += date_to.Text + "', NULL, " + aCar[5] + ", '";
                    mycommand.CommandText += type_id + "', '" + branch_id + "', ";

                    
                    if (drop_off_check.Checked == true)
                    {
                        mycommand.CommandText += "'" + dropoff.SelectedText + "')";
                    }
                    else
                    {
                        mycommand.CommandText += "NULL)";
                    }
                    //Clipboard.SetText(mycommand.CommandText);
                    //MessageBox.Show(mycommand.CommandText.ToString());
                    try
                    {
                        mycommand.ExecuteNonQuery();
                        MessageBox.Show("Booking Created. ID: " + new_id);
                    }
                    catch
                    {
                        MessageBox.Show("Cannot Insert Booking Into Table");
                    }
                }
            }
            update_status(user_id.Text);

        }
        private string get_new_id(string table)
        {
            /*
             * Returns the incremental next id for given table as string formatted to 3 digits
             * Takes string table, exact name of table in database
             */
            string new_id = "";
            int max_id = 0;
            mycommand.CommandText = "SELECT count(*) as max_id FROM " + table;
            try
            {
                myreader = mycommand.ExecuteReader();
                while (myreader.Read()) { max_id = Int32.Parse(myreader["max_id"].ToString()); }
                myreader.Close();
                new_id = (max_id++).ToString();
                if (new_id.Length == 1)
                {
                    new_id = "00" + max_id.ToString();
                }
                else if (new_id.Length == 2)
                {
                    new_id = "0" + max_id.ToString();
                }
            }
            catch (Exception e) { MessageBox.Show(e.ToString()); }
            return new_id;
        }


        private bool get_cust_status(string cust_id)
        {
            bool ret = false;
            mycommand.CommandText = "SELECT gold_status FROM customer WHERE cust_id = '" + cust_id + "'";

            try
            {
                myreader = mycommand.ExecuteReader();
                while (myreader.Read())
                {
                    if (myreader["gold_status"].ToString() == "True")
                    {
                        ret = true;
                    }
                }
            }
            catch
            {
                MessageBox.Show("Cannot Find Customer Status");

            }
            myreader.Close();
            return ret;
        }




        public string get_car_id_from_plate(string plate_no)
        {
            string car_id = "";
            mycommand.CommandText = "SELECT car_id FROM car WHERE car.plate_num = '" + plate_no + "'";
            try
            {
                myreader = mycommand.ExecuteReader();
                while (myreader.Read())
                {
                    car_id = myreader["car_id"].ToString();
                }
                myreader.Close();
            }
            catch { MessageBox.Show("Cannot Find Car With Plate: " + plate_no); }
            if (car_id.Length == 0) { MessageBox.Show("No Car_id with plate: " + plate_no); }
            return car_id;
        }


        private float get_late_fee(string type_id)
        {
            float late_fee = 0;
            mycommand.CommandText = "SELECT late_fee FROM type WHERE type_id = '" + type_id + "'";
            try
            {
                myreader = mycommand.ExecuteReader();
                while (myreader.Read())
                {
                    late_fee = float.Parse(myreader["late_fee"].ToString());
                }
            }
            catch
            {
                MessageBox.Show("Cannot Find Late Fee");
            }
            myreader.Close();
            return late_fee;
        }




        private float get_change_fee(string type_id)
        {
            float fee = 0;
            mycommand.CommandText = "SELECT change FROM type WHERE type_id = '" + type_id + "'";
            try
            {
                myreader = mycommand.ExecuteReader();
                while (myreader.Read())
                {
                    fee = float.Parse(myreader["change"].ToString());
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Cannot Find Change Fee: " + ex.Message);
            }
            myreader.Close();
            return fee;
        }



        private void returnOutput_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string[] aCar = new string[10];

            if (removeOutput.SelectedItems != null)
            {
                ListViewItem aSelection = returnOutputBox.SelectedItems[0];
                aCar[0] = (aSelection.SubItems[0].Text); //bookingID
                aCar[1] = (aSelection.SubItems[1].Text); //carID
                aCar[2] = (aSelection.SubItems[2].Text); //description
                aCar[3] = (aSelection.SubItems[3].Text); //plateNum
                aCar[4] = (aSelection.SubItems[4].Text); //dateFrom
                aCar[5] = (aSelection.SubItems[5].Text); //dateTo
                aCar[6] = cityToBID((aSelection.SubItems[6].Text)); //branchFrom
                aCar[7] = (aSelection.SubItems[7].Text); //typeRequested
                aCar[8] = (aSelection.SubItems[8].Text); //Price


                string drop_off = cityToBID(return_dropoff.SelectedItem.ToString());
                float final_price = float.Parse(aCar[8]);
                bool late = false, change = false;
                string message = "Return Car: " + aCar[1] + " To " + return_dropoff.Text;


                //Check if dropped of at different branch - Only apply fee if cust not gold
                if (drop_off != aCar[6])
                {
                    if (get_cust_status(return_id.Text) == false)
                    {
                        change = true;
                        message += "\nChange Branch Fee Applied";
                    }
                }

                //Check if the drop off is late
                if (DateTime.Parse(aCar[5]) < dropoff_date.Value)
                {
                    late = true;
                    message += "\nLate Dropoff Fee Applied";
                }

                final_price += calc_price(DateTime.Parse(aCar[4]), dropoff_date.Value, aCar[7], late, change);

                DialogResult confirm = MessageBox.Show(message + "\nFinal Price: " + final_price.ToString(), "Confirm", MessageBoxButtons.OKCancel);
                if (confirm == DialogResult.OK)
                {
                    mycommand.CommandText += "UPDATE booking SET ";
                    mycommand.CommandText += "branchTo = '" + drop_off + "', ";
                    mycommand.CommandText += "[returned] = '" + dropoff_date.Text + "' ";
                    mycommand.CommandText += "WHERE booking_id = '" + aCar[0] + "'";
                    try
                    {
                        mycommand.ExecuteNonQuery();
                        MessageBox.Show("Return Processed");
                        returnOutputBox.Items.Remove(aSelection);
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Cannot Complete Return: " + ex.Message);
                        Clipboard.SetText(mycommand.CommandText);
                    }
                    myreader.Close();
                }
            }
            else
            {
                returnOutputBox.Items.Add("No Results Found");
            }
        }

        private void submit_return_Click(object sender, EventArgs e)
        {
            returnOutputBox.Items.Clear();

            if (return_id.Text.Length == 0)
            {
                return_id_error.Text = "Please Enter Customer ID";
                return_id_error.ForeColor = Color.Red;
                return_id_error.Visible = true;
                return;
            }
            else
            {
                string temp = "";
                bool found = false;
                mycommand.CommandText = "SELECT FName, Lname, gold_status FROM customer WHERE cust_id = '" + return_id.Text + "'";
                try
                {
                    myreader = mycommand.ExecuteReader();
                    while (myreader.Read())
                    {
                        found = true;
                        temp += myreader["Fname"].ToString() + " " + myreader["Lname"].ToString();

                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                myreader.Close();
                if (found)
                {
                    update_status(return_id.Text);
                    if (get_cust_status(return_id.Text))
                    {
                        temp += " - Gold Status";
                    }
                    else
                    {
                        temp += " - Standard Status";
                    }
                    return_id_error.Text = temp;
                    return_id_error.ForeColor = Color.Black;
                    return_id_error.Visible = true;
                }
                else
                {
                    return_id_error.Text = "Cannot Find User";
                    return_id_error.Visible = true;
                    return_id_error.ForeColor = Color.Red;
                    return;
                }
            }
            mycommand.CommandText = "SELECT * FROM booking b, type t, car c WHERE cust_id = '" + return_id.Text + "'";
            mycommand.CommandText += " and returned IS NULL and b.type_requested = t.type_id and b.car_id = c.car_id";
            Clipboard.SetText(mycommand.CommandText);


            //List <string> = new List<string>
            try
            {
                string[] carInfo = new string[10];
                ListViewItem anItem;
                string tempDate;
                string tempDate1;
                myreader = mycommand.ExecuteReader();
                while (myreader.Read())
                {
                    carInfo[0] = myreader["booking_id"].ToString(); //BookingID
                    carInfo[1] = myreader["car_id"].ToString(); //CarID
                    carInfo[2] = myreader["description"].ToString(); //Description
                    carInfo[3] = myreader["plate_num"].ToString(); //PlateNum

                    tempDate = DateTime.Parse(myreader["date_From"].ToString()).Date.ToString("d"); //DateFrom
                    carInfo[4] = tempDate; //DateFrom

                    tempDate1 = DateTime.Parse(myreader["date_to"].ToString()).Date.ToString("d"); //DateTo
                    carInfo[5] = tempDate1; //DateTo

                    carInfo[6] = myreader["branchFrom"].ToString(); //BranchFrom
                    carInfo[7] = myreader["type_requested"].ToString(); //TypeRequested
                    carInfo[8] = myreader["price"].ToString(); //Price

                    anItem = new ListViewItem(carInfo);
                    returnOutputBox.Items.Add(anItem);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            myreader.Close();
        }


        private void addcar_submit_Click(object sender, EventArgs e)
        {
            string confirm;
            confirm = "Add a '" + addcar_year.Text + "' '" + addcar_type.SelectedItem.ToString() + "' to the '" + addcar_branch.SelectedItem.ToString();
            confirm += "' branch with a licence plate number of '" + addcar_plate.Text + "'?";
            DialogResult addCar = MessageBox.Show(confirm, "Confirm", MessageBoxButtons.YesNo);
            mycommand.CommandText = "SELECT MAX(car_id) FROM car";
            myreader = mycommand.ExecuteReader();
            string numOfCars = "";
            string newID = "";
            while (myreader.Read())
            {
                numOfCars = myreader[0].ToString();
            }
            myreader.Close();
            
            
            int carID;
            carID = Convert.ToInt32(numOfCars);
            carID = carID + 1;
            newID = (carID).ToString();
            int a = newID.Length;
            if (a == 1)
                newID = "00" + carID.ToString();
            if (a == 2)
                newID = "0" + carID.ToString();
            string carToAdd;
            carToAdd = "Insert into car (car_id, car_type, car_branch, model, year, plate_num) values (" + "'" + newID + "'" + ", " + "'" +descToType(addcar_type.SelectedItem.ToString()) + "'" + ", " + "'" +cityToBID(addcar_branch.SelectedItem.ToString()) + "'" + ", " + "'" + addcar_model.Text.ToString() + "'" + ", " + "'" + addcar_year.Text.ToString() + "'" + ", " + "'" + addcar_plate.Text.Replace("-", "") + "'" + ")";
            mycommand.CommandText = carToAdd;
            int result;
            if (addCar == DialogResult.Yes)
            {
                asyncResult = mycommand.BeginExecuteNonQuery();
                result = mycommand.EndExecuteNonQuery(asyncResult);
                MessageBox.Show("Added " + result.ToString() + " car to inventory.");
            }

            addcar_branch.ResetText();
            addcar_type.ResetText();
            addcar_year.Clear();
            addcar_plate.Clear();
            addcar_model.Clear();

        }

        private void report_submit_Click(object sender, EventArgs e)
        {
            reportOutputBox.Visible = true;
            reportOutputBox.Items.Clear();
            reportOutputBox.Columns.Clear();
            string[] temp = new string[3];
            IList<string> report = new List<string>();
            ListViewItem anItem;
            if (radioButton5.Checked == true)
            {
                reportOutputBox.Columns.Add("First Name", 150);
                reportOutputBox.Columns.Add("Last Name", 150);
                reportOutputBox.Columns.Add("Customer ID", 150);

                mycommand.CommandText =
                    "select distinct c.Fname, c.Lname, c.cust_id from booking b, customer c where " +
                    "b.cust_id = c.cust_id and c.gold_status = 1 and c.cust_id not in " +
                    "(select b.cust_id from booking b, car c where b.type_requested != c.car_type " +
                    "and c.car_id = b.car_id) ";
                    
                try
                {
                    myreader = mycommand.ExecuteReader();
                    while (myreader.Read())
                    {
                        temp[0] = myreader[0].ToString();
                        temp[1] = myreader[1].ToString();
                        temp[2] = myreader[2].ToString();
                        anItem = new ListViewItem(temp);
                        reportOutputBox.Items.Add(anItem);
                    }
                } catch (Exception e1){MessageBox.Show(Text, e1.Message);}

                myreader.Close();
            }
            else if (radioButton1.Checked == true)
            {
                reportOutputBox.Columns.Add("Branch ID", 250);
                reportOutputBox.Columns.Add("Customer Spending ($)", 250);
                mycommand.CommandText = "select distinct branchFrom, sum(price) as [output] from booking where branchFrom = '"
                    + cityToBID(report_branch.SelectedItem.ToString())
                    + "' and type_requested = '" + descToType(report_type.SelectedItem.ToString()) + "'" +
                    "and date_from >= '" + report_datefrom.Text + "' and date_to <= '"
                    + report_dateto.Text + "' group by branchFrom;";
                myreader = mycommand.ExecuteReader();
                while (myreader.Read())
                {
                    temp[0] = myreader[0].ToString();
                    temp[1] = myreader[1].ToString();
                    //anItem = new ListViewItem(myreader[1].ToString());
                    anItem = new ListViewItem(temp);
                    reportOutputBox.Items.Add(anItem);

                    //reportbox.Text += myreader["output"].ToString();
                }
                myreader.Close();
            }
            else if (radioButton2.Checked == true)
            {
                reportOutputBox.Columns.Add("Branch", 200);
                reportOutputBox.Columns.Add("Total Late Dropoffs", 200);
                reportOutputBox.Columns.Add("Total Late Fees", 150);

                mycommand.CommandText = "Select * from branch br," +
                    "(select sum(late_fee) as total, branchFrom from type t, booking b where t.type_id = b.type_requested and b.date_to < b.returned group by b.branchFrom) as t1, " +
                    " (select count(*) as [count], branchFrom from booking b, type t WHERE t.type_id = b.type_requested and date_to < returned group by branchFrom) as t2 " +
                    "where t1.branchfrom = t2.branchfrom and br.branch_id = t1.branchFrom";
                if (report_branch.SelectedIndex != 0)
                {
                    mycommand.CommandText += " and t1.branchFrom = '" + cityToBID(report_branch.SelectedItem.ToString()) + "'";
                }
                try
                {
                    myreader = mycommand.ExecuteReader();
                    while (myreader.Read())
                    {
                        temp[0] = myreader["city"].ToString();
                        temp[1] = myreader["count"].ToString();
                        temp[2] = myreader["total"].ToString();
                        //anItem = new ListViewItem(myreader[1].ToString());
                        anItem = new ListViewItem(temp);
                        reportOutputBox.Items.Add(anItem);

                        //reportbox.Text += myreader["output"].ToString();
                    }
                }catch(SqlException ex) { MessageBox.Show(ex.Message); }
                myreader.Close();
                Clipboard.SetText(mycommand.CommandText);
            }
            else if (radioButton3.Checked == true)
            {
                reportOutputBox.Columns.Add("Type", 150);
                reportOutputBox.Columns.Add("Total Rentals", 150);
                mycommand.CommandText = "select description, max(num1) as total " +
                    "from( " +
                    "select description, count(*) as num1 " +
                    "from booking b, type t, car c " +
                    "where b.car_id = c.car_id and c.car_type = t.type_id ";
                if (report_branch.SelectedIndex != 0) {
                    mycommand.CommandText += "and branchFrom = '" +
                    cityToBID(report_branch.SelectedItem.ToString()) + "'";
                }
                mycommand.CommandText += " group by description) as tem " +
                    "group by description ";

                try { 
                    myreader = mycommand.ExecuteReader();
                    while (myreader.Read())
                    {
                        temp[0] = myreader[0].ToString();
                        temp[1] = myreader[1].ToString();
                        //anItem = new ListViewItem(myreader[1].ToString());
                        anItem = new ListViewItem(temp);
                        reportOutputBox.Items.Add(anItem);

                        //reportbox.Text += myreader["output"].ToString();
                    }
                }
                catch (SqlException ex){ MessageBox.Show(ex.Message); }
                myreader.Close();
            }
            else if (radioButton4.Checked == true)
            {
                reportOutputBox.Columns.Add("Average Rental Time (Days)", 250);
                
                mycommand.CommandText = "select sum([days]) as [output] " +
                    "from( " +
                    "select booking_id, DATEDIFF(day, date_from, date_to) as [days] " +
                    "from booking where branchFrom = '" +
                    cityToBID(report_branch.SelectedItem.ToString()) +
                    "' and type_requested = '" +
                    descToType(report_type.SelectedItem.ToString()) +
                    "') as Temp;";
                MessageBox.Show(mycommand.CommandText.ToString());
                myreader = mycommand.ExecuteReader();
                while (myreader.Read())
                {
                    temp[0] = myreader[0].ToString();
                    //anItem = new ListViewItem(myreader[1].ToString());
                    anItem = new ListViewItem(temp);
                    reportOutputBox.Items.Add(anItem);

                    //reportbox.Text += myreader["output"].ToString();
                }
                myreader.Close();


            }

            //while (myreader.Read())
            //{
                //anItem = new ListViewItem(myreader["output"].ToString());

               // reportOutputBox.Items.Add(anItem);
                //reportbox.Text += myreader["output"].ToString();
            //}
            myreader.Close();

            //MessageBox.Show(mycommand.CommandText);
            //temp string then report box
            //string temp = "";
            //while (myreader.Read())
            //{
                //temp += myreader["output"].ToString();
               // reportbox.Items.Add(myreader["output"].ToString() + "\n");
                
                //MessageBox.Show(temp);
            //}
            if (reportOutputBox.Items.Count == 0)
            {
                reportOutputBox.Items.Add("No Results");
            }
            //myreader.Close();
            //if (String.IsNullOrEmpty(reportbox.Text))
            //{
            //    MessageBox.Show("No data available");
            //}


            //reportbox.Visible = true;


            /*
            IList<string> report = new List<string>();

            if (radioButton1.Checked == true)
                mycommand.CommandText = "SELECT branchFrom, sum(price) as [sum] FROM booking WHERE branchFrom = " + report_branch.ValueMember.ToString() + " and type_requested = " + report_type.ValueMember.ToString() + " and date_from >= " + report_datefrom.ToString() + " and date_to <= " + report_dateto.ToString() + " GROUP BY branchFrom; ";
            myreader = mycommand.ExecuteReader();
            while (myreader.Read())
            {
                report.Add(myreader["sum"].ToString() + "\n");
            }
            myreader.Close();
            reportbox.Text = String.Join("", report.Distinct().ToList());
            reportbox.Visible = true;*/
        }

        private void drop_off_check_CheckedChanged(object sender, EventArgs e)
        {

        }

        /*private void results1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (booking_output.SelectedItem != null)
            {
                DialogResult dialogResult = MessageBox.Show("Book " + booking_output.SelectedItem.ToString(), "Book", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    //TODO INSERT INTO BOOKING TABLE
                }
            }
        }*/





        //---------------------------------------REMOVE CAR---------------------------------------------//
        private void remcar_id_TextChanged(object sender, EventArgs e)
        {
            removeOutput.Items.Clear();

            searchForCarsWithID(this, remcar_id.Text.ToString());
        }

        private void remtype_SelectedIndexChanged(object sender, EventArgs e)
        {
            removeOutput.Items.Clear();
        }

        private void rembranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            removeOutput.Items.Clear();
        }

        private void remSubmit_Click(object sender, EventArgs e)
        {
            string[] carInfo = new string[6];
            ListViewItem anItem;
            removeOutput.Items.Clear();
            if (remcar_id.Text.Length != 0)
            {
                searchForCarsWithID(this, remcar_id.Text);
            }

            else if (remcar_type.Text.Equals("Any") && remcar_branch.Text.Equals("Any"))
            {
                mycommand.CommandText = "SELECT * FROM car C, branch B, type T WHERE (C.car_branch = B.branch_id AND C.car_type = T.type_id)" + " AND C.model like '" + remcar_model.Text + "%' AND C.year like '" + remcar_year.Text + "%' AND C.plate_num like '" + remcar_plate.Text + "%'"; ;

                try
                {
                    myreader = mycommand.ExecuteReader();
                    while (myreader.Read())
                    {

                        carInfo[0] = myreader["car_id"].ToString();
                        carInfo[1] = myreader["description"].ToString();
                        carInfo[2] = myreader["city"].ToString();
                        carInfo[3] = myreader["model"].ToString();
                        carInfo[4] = myreader["year"].ToString();
                        carInfo[5] = myreader["plate_num"].ToString();
                        anItem = new ListViewItem(carInfo);
                        removeOutput.Items.Add(anItem);
                    }

                } catch (SqlException e2) { MessageBox.Show(e2.Message); }
                myreader.Close();

            }
            else if ((remcar_type.Text.Equals("Any")) && (!remcar_branch.Text.Equals("Any")))
            {
                mycommand.CommandText = "SELECT * FROM car C, branch B, type T WHERE (C.car_branch = B.branch_id AND C.car_type = T.type_id) AND B.city = " + "'" + remcar_branch.Text + "'" + " AND C.model like '" + remcar_model.Text + "%' AND C.year like '" + remcar_year.Text + "%' AND C.plate_num like '" + remcar_plate.Text + "%'";

                try
                {
                    myreader = mycommand.ExecuteReader();
                    while (myreader.Read())
                    {

                        carInfo[0] = myreader["car_id"].ToString();
                        carInfo[1] = myreader["description"].ToString();
                        carInfo[2] = myreader["city"].ToString();
                        carInfo[3] = myreader["model"].ToString();
                        carInfo[4] = myreader["year"].ToString();
                        carInfo[5] = myreader["plate_num"].ToString();
                        anItem = new ListViewItem(carInfo);
                        removeOutput.Items.Add(anItem);
                    }

                } catch (SqlException e3) { MessageBox.Show(e3.Message); }
                myreader.Close();
            }

            else if (!(remcar_type.Text.Equals("Any")) && remcar_branch.Text.Equals("Any"))
            {
                mycommand.CommandText = "SELECT * FROM car C, branch B, type T WHERE (C.car_branch = B.branch_id AND C.car_type = T.type_id) AND T.description = " + "'" + remcar_type.SelectedItem.ToString() + "'" + " AND C.model like '" + remcar_model.Text + "%' AND C.year like '" + remcar_year.Text + "%' AND C.plate_num like '" + remcar_plate.Text + "%'";

                try
                {
                    myreader = mycommand.ExecuteReader();
                    while (myreader.Read())
                    {

                        carInfo[0] = myreader["car_id"].ToString();
                        carInfo[1] = myreader["description"].ToString();
                        carInfo[2] = myreader["city"].ToString();
                        carInfo[3] = myreader["model"].ToString();
                        carInfo[4] = myreader["year"].ToString();
                        carInfo[5] = myreader["plate_num"].ToString();
                        anItem = new ListViewItem(carInfo);
                        removeOutput.Items.Add(anItem);
                    }

                } catch (SqlException e4) { MessageBox.Show(e4.Message); }
                myreader.Close();

            }

            else
            {
                mycommand.CommandText = "SELECT * FROM car C, branch B, type T WHERE (C.car_branch = B.branch_id AND C.car_type = T.type_id) AND T.description = " + "'" + remcar_type.SelectedItem.ToString() + "'" + " AND B.city = " + "'" + remcar_branch.SelectedItem.ToString() + "' AND C.model like '" + remcar_model.Text + "%' AND C.year like '" + remcar_year.Text + "%' AND C.plate_num like '" + remcar_plate.Text + "%'";

                try
                {
                    myreader = mycommand.ExecuteReader();
                    while (myreader.Read())
                    {

                        carInfo[0] = myreader["car_id"].ToString();
                        carInfo[1] = myreader["description"].ToString();
                        carInfo[2] = myreader["city"].ToString();
                        carInfo[3] = myreader["model"].ToString();
                        carInfo[4] = myreader["year"].ToString();
                        carInfo[5] = myreader["plate_num"].ToString();
                        anItem = new ListViewItem(carInfo);
                        removeOutput.Items.Add(anItem);
                    }

                } catch (SqlException e5) { MessageBox.Show(e5.Message); }
                myreader.Close();

            }

            if (removeOutput.Items.Count == 0)
                MessageBox.Show("No Vehicles were found");
        }

        private void Remove_Output_DoubleClick(object sender, MouseEventArgs e)
        {
            mycommand.CommandText = "";
            if (removeOutput.SelectedItems != null)
            {
                string[] aCar = new string[6];

                ListViewItem aSelection = removeOutput.SelectedItems[0];


                aCar[0] = (aSelection.SubItems[0].Text);
                aCar[1] = (aSelection.SubItems[1].Text);
                aCar[2] = (aSelection.SubItems[2].Text);
                aCar[3] = (aSelection.SubItems[3].Text);
                aCar[4] = (aSelection.SubItems[4].Text);
                aCar[5] = (aSelection.SubItems[5].Text);


                string message = "Are you sure you would like to remove this vehicle?\n";
                for (int i1 = 0; i1 < aCar.Length; i1++)
                {
                    message += aCar[i1].ToString() + " ";
                }

                DialogResult removeCar = MessageBox.Show(message, "Remove", MessageBoxButtons.YesNo);

                string[] cols = { "car_id", "car_type", "car_branch", "model", "year", "plate_num" };
                mycommand.CommandText += "DELETE FROM car WHERE ";

                aCar[0] = (aSelection.SubItems[0].Text);
                aCar[1] = descToType((aSelection.SubItems[1].Text));
                aCar[2] = cityToBID((aSelection.SubItems[2].Text));
                aCar[3] = (aSelection.SubItems[3].Text);
                aCar[4] = (aSelection.SubItems[4].Text);
                aCar[5] = (aSelection.SubItems[5].Text);

                int i;
                for (i = 0; i < aCar.Length; i++)
                {                    
                    if (i + 1 == aCar.Length)
                    {
                        break;
                    }
                    else
                    {
                        mycommand.CommandText += " AND ";
                    }

                    mycommand.CommandText += cols[i] + " = ";
                    mycommand.CommandText += "'" + aCar[i] + "'";


                }
                mycommand.CommandText += " AND plate_num = ";
                mycommand.CommandText += "'" + aCar[aCar.Length - 1] + "'";

                MessageBox.Show(mycommand.CommandText.ToString());
                if (removeCar == DialogResult.Yes)
                {
                    try
                    {
                        int nums = mycommand.ExecuteNonQuery();

                        MessageBox.Show("Car removed from the system." + nums.ToString());
                        removeOutput.Items.RemoveAt(aSelection.Index);
                    }
                    catch (SqlException) { MessageBox.Show("Something went wrong..\nThe car could not be removed.\nPlease try again", "ERROR"); }


                }
            }
        }
        //------------------------------------------------END REMOVE CARS-------------------------------------------------------//





        //-----------------------------------------------------EDIT CAR---------------------------------------------------------//
        private void editCar_id(object sender, EventArgs e)
        {
            editOutput.Items.Clear();
            searchForCarsWithID(this, editcar_id.Text.ToString());

        }



        private void edit_output_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (editOutput.SelectedItems.Count != 0)
            {
                string[] aCar = new string[6];

                ListViewItem aSelection = editOutput.SelectedItems[0];

                aCar[0] = (aSelection.SubItems[0].Text);
                aCar[1] = (aSelection.SubItems[1].Text);
                aCar[2] = (aSelection.SubItems[2].Text);
                aCar[3] = (aSelection.SubItems[3].Text);
                aCar[4] = (aSelection.SubItems[4].Text);
                aCar[5] = (aSelection.SubItems[5].Text);

                Edit edit = new Edit(parent, aCar);
                edit.Show();
                editOutput.Items.Clear();
            }
        }



        private void editCar_Submit(object sender, EventArgs e)
        {
            string[] carInfo = new string[6];
            ListViewItem anItem;
            editOutput.Items.Clear();
            if (editcar_id.Text.Length != 0)
            {
                searchForCarsWithID(this, editcar_id.Text);
            }

            else if (edit_branch.SelectedItem.ToString().Equals("Any") && edit_type.SelectedItem.ToString().Equals("Any"))
            {
                mycommand.CommandText = "SELECT * FROM car C, branch B, type T WHERE (C.car_branch = B.branch_id AND C.car_type = T.type_id)" + " AND C.model like '" + edit_model.Text + "%' AND C.year like '" + edit_year.Text + "%' AND C.plate_num like '" + edit_plate.Text + "%'"; // AND T.description = " + "'" + edit_type.SelectedItem.ToString() + "'" + " AND B.city = " + "'" + edit_branch.SelectedItem.ToString() + "'";
                try
                {
                    myreader = mycommand.ExecuteReader();
                    while (myreader.Read())
                    {
                        carInfo[0] = myreader["car_id"].ToString();
                        carInfo[1] = myreader["description"].ToString();
                        carInfo[2] = myreader["city"].ToString();
                        carInfo[3] = myreader["model"].ToString();
                        carInfo[4] = myreader["year"].ToString();
                        carInfo[5] = myreader["plate_num"].ToString();
                        anItem = new ListViewItem(carInfo);
                        editOutput.Items.Add(anItem);
                    }

                } catch (SqlException e4) { MessageBox.Show(e4.Message); }
                myreader.Close();

            }
            else if (edit_branch.SelectedItem.ToString().Equals("Any") && !(edit_type.SelectedItem.ToString().Equals("Any")))
            {
                mycommand.CommandText = "SELECT * FROM car C, branch B, type T WHERE (C.car_branch = B.branch_id AND C.car_type = T.type_id) AND T.description = " + "'" + edit_type.SelectedItem.ToString() + "'" + " AND C.model like '" + edit_model.Text + "%' AND C.year like '" + edit_year.Text + "%' AND C.plate_num like '" + edit_plate.Text + "%'"; // + " AND B.city = " + "'" + edit_branch.SelectedItem.ToString() + "'";
                try
                {
                    myreader = mycommand.ExecuteReader();
                    while (myreader.Read())
                    {
                        carInfo[0] = myreader["car_id"].ToString();
                        carInfo[1] = myreader["description"].ToString();
                        carInfo[2] = myreader["city"].ToString();
                        carInfo[3] = myreader["model"].ToString();
                        carInfo[4] = myreader["year"].ToString();
                        carInfo[5] = myreader["plate_num"].ToString();
                        anItem = new ListViewItem(carInfo);
                        editOutput.Items.Add(anItem);
                    }

                } catch (SqlException e3) { MessageBox.Show(e3.Message); }
                myreader.Close();
            }

            else if (!(edit_branch.SelectedItem.ToString().Equals("Any")) && edit_type.SelectedItem.ToString().Equals("Any"))
            {
                mycommand.CommandText = "SELECT * FROM car C, branch B, type T WHERE (C.car_branch = B.branch_id AND C.car_type = T.type_id) AND B.city = " + "'" + edit_branch.SelectedItem.ToString() + "'" + " AND C.model like '" + edit_model.Text + "%' AND C.year like '" + edit_year.Text + "%' AND C.plate_num like '" + edit_plate.Text + "%'"; ; // AND T.description = " + "'" + edit_type.SelectedItem.ToString() + "'" + " 
                try
                {
                    myreader = mycommand.ExecuteReader();
                    while (myreader.Read())
                    {
                        carInfo[0] = myreader["car_id"].ToString();
                        carInfo[1] = myreader["description"].ToString();
                        carInfo[2] = myreader["city"].ToString();
                        carInfo[3] = myreader["model"].ToString();
                        carInfo[4] = myreader["year"].ToString();
                        carInfo[5] = myreader["plate_num"].ToString();
                        anItem = new ListViewItem(carInfo);
                        editOutput.Items.Add(anItem);
                    }

                } catch (SqlException e2) {MessageBox.Show(e2.Message); }
                myreader.Close();

            }

            else
            {
                mycommand.CommandText = "SELECT * FROM car C, branch B, type T WHERE (C.car_branch = B.branch_id AND C.car_type = T.type_id) AND T.description = " + "'" + edit_type.SelectedItem.ToString() + "'" + " AND B.city = " + "'" + edit_branch.SelectedItem.ToString() + "'" + " AND C.model like '" + edit_model.Text + "%' AND C.year like '" + edit_year.Text + "%' AND C.plate_num like '" + edit_plate.Text + "%'"; ;
                try
                {
                    myreader = mycommand.ExecuteReader();
                    while (myreader.Read())
                    {
                        carInfo[0] = myreader["car_id"].ToString();
                        carInfo[1] = myreader["description"].ToString();
                        carInfo[2] = myreader["city"].ToString();
                        carInfo[3] = myreader["model"].ToString();
                        carInfo[4] = myreader["year"].ToString();
                        carInfo[5] = myreader["plate_num"].ToString();
                        anItem = new ListViewItem(carInfo);
                        editOutput.Items.Add(anItem);
                    }
                }

                catch (SqlException e1) { MessageBox.Show(e1.Message);}
                myreader.Close();

            }
        }
        //----------------------------------------------END EDIT CAR--------------------------------------------------//












        private void searchForCarsWithID(object sender, string anID)
        {
            string[] carInfo = new string[6];
            ListViewItem anItem;
            mycommand.CommandText = "SELECT * FROM car C, branch B, type T ";
            mycommand.CommandText += "WHERE (C.car_branch = B.branch_id AND C.car_type = T.type_id) ";
            mycommand.CommandText += "AND car_id like " + "'" + anID + "%'";

            try
            {
                myreader = mycommand.ExecuteReader();

                if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage6"])
                {
                    removeOutput.Items.Clear();
                    while (myreader.Read())
                    {
                        carInfo[0] = myreader["car_id"].ToString();
                        carInfo[1] = myreader["description"].ToString();
                        carInfo[2] = myreader["city"].ToString();
                        carInfo[3] = myreader["model"].ToString();
                        carInfo[4] = myreader["year"].ToString();
                        carInfo[5] = myreader["plate_num"].ToString();
                        anItem = new ListViewItem(carInfo);
                        removeOutput.Items.Add(anItem);
                    }
                }
                else if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage7"])
                {
                    editOutput.Items.Clear();
                    while (myreader.Read())
                    {
                        carInfo[0] = myreader["car_id"].ToString();
                        carInfo[1] = myreader["description"].ToString();
                        carInfo[2] = myreader["city"].ToString();
                        carInfo[3] = myreader["model"].ToString();
                        carInfo[4] = myreader["year"].ToString();
                        carInfo[5] = myreader["plate_num"].ToString();
                        anItem = new ListViewItem(carInfo);
                        editOutput.Items.Add(anItem);
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            myreader.Close();
        }



        //Allows enter key to be used as a click for the submit button for each tab page
        private void tabPage_Switch(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage1"])
            {
                this.AcceptButton = button1;
                user_id.ResetText();
                user_info.Visible = false;
                drop_off_check.Checked = default;
                date_from.ResetText();
                date_to.Value = default_date_to;
            }
                
            else if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage2"])
            {
                this.AcceptButton = submit_return;
                return_id_error.Visible = false;
                returnOutputBox.Items.Clear();
                //returnOutput.Items.Clear();
                dropoff_date.ResetText();
                return_id.ResetText();
               
            }
                
            else if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage3"])
            {
                this.AcceptButton = InventoryButton;
                InventoryBranch.SelectedIndex = 0;
                invDateFrom.ResetText();
                invDateTo.Value = default_date_to;
            }
                
            else if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage4"])
            {
                this.AcceptButton = report_submit;
                reportOutputBox.Items.Clear();
                reportOutputBox.Columns.Clear();
                radioButton5.Checked = default;
                radioButton4.Checked = default;
                radioButton3.Checked = default;
                radioButton2.Checked = default;
                radioButton1.Checked = default;
                if (noFilters.Visible == true)
                    noFilters.Visible = false;
                report_branch.SelectedIndex = default;
                report_type.SelectedIndex = default;
                report_datefrom.Text = default_date;
                report_dateto.Value = default_date_to;
                reportOutputBox.Visible = false;


            }
                
            else if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage5"])
            {
                this.AcceptButton = addcar_submit;
                addcar_branch.SelectedIndex = default;
                addcar_type.SelectedIndex = default;
                addcar_year.ResetText();
                addcar_model.ResetText();
                addcar_plate.ResetText();
            }
                
            else if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage6"])
            {
                remcar_id.Text = default;
                this.AcceptButton = remove_submit;
                removeOutput.Items.Clear();
                remcar_type.SelectedIndex = default;
                remcar_branch.SelectedIndex = default;
                remcar_year.ResetText();
                remcar_plate.ResetText();
                remcar_model.ResetText();

            }
                
            else if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage7"])
            {
                editcar_id.Text = default;
                this.AcceptButton = editSubmit;
                editOutput.Items.Clear();
                edit_branch.SelectedIndex = default;
                edit_type.SelectedIndex = default;
                edit_model.ResetText();
                edit_plate.ResetText();
                edit_year.ResetText();

            }
                
            
        }

        private void getInventory(object sender, EventArgs e)
        {
            string branchID = InventoryBranch.SelectedItem.ToString();

            string dateFrom = invDateFrom.Text;
            string dateTo = invDateTo.Text;
            Inventory inventory = new Inventory(this, branchID, dateFrom, dateTo);
            inventory.Show();
            this.Visible = false;
            
            
           
        }

        private void customerLookup(object sender, EventArgs e)
        {

            TextBox textBox;
            if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage2"])
            {
                textBox = return_id;
                CustomerLookup customerLookup = new CustomerLookup(this, textBox);
                customerLookup.Show();
                this.Visible = false;
            }
            else if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage1"])
            {
                textBox = user_id;
                CustomerLookup customerLookup = new CustomerLookup(this, textBox);
                customerLookup.Show();
                this.Visible = false;
            }
        }

        private string cityToBID(string city)
        {
            string aBID = "";
            mycommand.CommandText = "SELECT branch_id FROM branch WHERE city = " + "'" + city + "'";

            try
            {
                myreader = mycommand.ExecuteReader();
                while (myreader.Read())
                    aBID += myreader["branch_id"].ToString().TrimEnd();
            } catch (SqlException e) { MessageBox.Show(e.Message); }

            myreader.Close();
            return aBID;
        }

        private string descToType(string description)
        {
            string aTypeID = "";
            mycommand.CommandText = "SELECT type_id FROM type WHERE description = " + "'" + description + "'";

            try
            {
                myreader = mycommand.ExecuteReader();
                while (myreader.Read())
                    aTypeID += myreader["type_id"].ToString().TrimEnd();
            } catch (SqlException e) { MessageBox.Show(e.Message); }

            myreader.Close();
            return aTypeID;


        }
    }    
}
