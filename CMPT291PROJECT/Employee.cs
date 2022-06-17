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
        public IAsyncResult asyncResult;
        IList<string> arguments = new List<string>();
        public string default_date;

        public Employee(Login f1)
        {
            InitializeComponent();
            default_date = date_from.Text;
            user_info.Visible = false;
            return_id_error.Visible = false;
            // Inherit SQL Connection
            parent = f1;
            myconnection = f1.myconnection;
            myreader = f1.myreader;
            mycommand = f1.mycommand;
            editOutput.View = View.Details;
            editOutput.GridLines = true;
            editOutput.FullRowSelect = true;

            editOutput.Columns.Add("Car ID", 55);
            editOutput.Columns.Add("Type", 90);
            editOutput.Columns.Add("Branch", 95);
            editOutput.Columns.Add("Model", 95);
            editOutput.Columns.Add("Year", 70);
            editOutput.Columns.Add("Licence Plate", 90);


            removeOutput.View = View.Details;

            removeOutput.Columns.Add("Car ID", 55);
            removeOutput.Columns.Add("Type", 90);
            removeOutput.Columns.Add("Branch", 95);
            removeOutput.Columns.Add("Model", 95);
            removeOutput.Columns.Add("Year", 70);
            removeOutput.Columns.Add("Licence Plate", 90);




            returnHeading.Text = "Booking ID\t" + "\t" + "Customer ID" + "\t" + "Car ID";

            // Populate report branch combobox
            mycommand.CommandText = "SELECT branch_id FROM branch";

            myreader.Close();
            myreader = mycommand.ExecuteReader();
            while (myreader.Read())
            {
                report_branch.Items.Add(myreader["branch_id"].ToString());
            }

            // Populate report type combobox
            myreader.Close();
            mycommand.CommandText = "SELECT type_id from type";
            myreader = mycommand.ExecuteReader();
            while (myreader.Read())
            {
                report_type.Items.Add(myreader["type_id"].ToString());
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

                pickup.DataSource = branches;
                dropoff.DataSource = branches;
                
                vehicle_type.DataSource = types;
                
            }
            
            catch (Exception e3) { MessageBox.Show(e3.ToString()); }
            myreader.Close();
            
            
        
           
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Employee_Load(object sender, EventArgs e)
        {
        }

        private void Employee_FormClosing(object sender, FormClosingEventArgs e)
        {
            parent.Visible = true;
        }

        private void booking_submit_click(object sender, EventArgs e)
        {
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
                        if (myreader["Gold_status"].ToString() == "True") { user_info.Text += "\nGold Status"; }
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
                mycommand.CommandText += "and t.description = '" + vehicle_type.SelectedItem.ToString() + "' ";
            }
            if (date_from.Value.ToString() != default_date || date_to.Value.ToString() != default_date)
            {
                //Get all bookings interfering with dates selected, get car_id not in that list
                // (existing start <= new end) and (new start <= existing end) Then they overlap //
                mycommand.CommandText += "and car_id not in (select car_id from Booking b where ";
                mycommand.CommandText += "(b.Date_From <= '" + date_to.Value.ToString() + "') and ('";
                mycommand.CommandText += date_from.Value.ToString() + "' <= b.Date_To)) ";              //TODO Check if date formats are compatible
            }
            //Clipboard.SetText(mycommand.CommandText);

            // Display all available vehicles to output
            IList<string> result = new List<string>();
            string temp;
            float cost = 0;
            try
            {
                myreader = mycommand.ExecuteReader();
                while (myreader.Read())
                {
                    temp = myreader["city"].ToString() + "\t" + myreader["description"].ToString() + "\t";
                    temp += myreader["model"].ToString() + "\t" + myreader["year"].ToString() + "\t";
                    cost = calculate_price(date_from.Value, date_to.Value, myreader["daily"].ToString(), myreader["weekly"].ToString(), myreader["monthly"].ToString());

                    temp += myreader["plate_num"].ToString() + "\t" + cost.ToString();
                    result.Add(temp);
                }
            }
            catch (Exception e3) { MessageBox.Show(e3.ToString()); }

            myreader.Close();
            booking_output.DataSource = result;
            //output.Text = String.Join("", result.Distinct().ToList());
            booking_output.Visible = true;

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

            if (booking_output.SelectedValue != null)
            {
                string[] args = booking_output.SelectedItem.ToString().Split('\t');
                string selected_car_id = get_car_id_from_plate(args[args.Length - 2]);
                string branch_id = "", type_id = "";

                // Get type_id and branch_id from selected text
                mycommand.CommandText = "SELECT DISTINCT type_id, branch_id FROM type, branch WHERE type.description = '" + args[1];
                mycommand.CommandText += "' and branch.city = '" + args[0] + "'";
                Clipboard.SetText(mycommand.CommandText);
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
                string message = "Create booking: " + args[1].Trim() + " From " + date_from.Text + " To " + date_to.Text + " For " + args[5];
                DialogResult confirm = MessageBox.Show(message, "Confirm Booking", MessageBoxButtons.YesNo);
                if (confirm == DialogResult.Yes)
                {
                    // Create insertion command and commit to database
                    string new_id = get_new_id("booking");
                    mycommand.CommandText = "INSERT INTO booking VALUES('" + new_id + "', '";
                    mycommand.CommandText += user_id.Text + "', '" + selected_car_id + "', '" + date_from.Text + "', '";
                    mycommand.CommandText += date_to.Text + "', NULL, " + args[args.Length - 1] + ", '";
                    mycommand.CommandText += type_id + "', '" + branch_id + "', ";
                    if (drop_off_check.Checked == true)
                    {
                        mycommand.CommandText += "'" + dropoff.SelectedText + "')";
                    }
                    else
                    {
                        mycommand.CommandText += "NULL)";
                    }
                    Clipboard.SetText(mycommand.CommandText);

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

        

        private void addcar_submit_Click(object sender, EventArgs e)
        {
            string confirm;
            confirm = "Add a " + addcar_year.Text + " " + addcar_type.SelectedItem.ToString() + " to " + addcar_branch.SelectedItem.ToString();
            confirm += " with " + addcar_colour.Text + " colour?";
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
            addcar_colour.Clear();
            addcar_year.Clear();
            addcar_plate.Clear();
            addcar_model.Clear();

        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void label22_Click(object sender, EventArgs e)
        {

        }

        private void label23_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }




        private void report_submit_Click(object sender, EventArgs e)
        {
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
            reportbox.Visible = true;
        }

        private void drop_off_check_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void results1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (booking_output.SelectedItem != null)
            {
                DialogResult dialogResult = MessageBox.Show("Book " + booking_output.SelectedItem.ToString(), "Book", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    //TODO INSERT INTO BOOKING TABLE
                }
            }
        }





        //---------------------------------------REMOVE CAR---------------------------------------------//
        private void remcar_id_TextChanged(object sender, EventArgs e)
        {
            remove_output.Items.Clear();

            searchForCarsWithID(this, remcar_id.Text.ToString());
        }

        private void remtype_SelectedIndexChanged(object sender, EventArgs e)
        {
            remove_output.DataSource = null;
        }

        private void rembranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            //remove_output.DataSource = null;
        }

        private void remSubmit_Click(object sender, EventArgs e)
        {
            string[] carInfo = new string[6];
            ListViewItem anItem;

            //remove_output.DataSource = null;
            removeOutput.Items.Clear();
            if (remcar_id.Text.Length != 0)
            {
                searchForCarsWithID(this, remcar_id.Text);
                /*mycommand.CommandText = "SELECT C.car_id, T.description, B.city, C.model, C.year, C.plate_num" +
                    " FROM car C, branch B, type T" +
                    " WHERE (C.car_branch = B.branch_id AND C.car_type = T.type_id)" +
                    " AND car_id = " + "'" + remcar_id.Text + "'";

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

                        /*
                        remove_output.Items.Add(myreader["car_id"].ToString().Replace("  ", "")
                            + "\t" + myreader["description"].ToString().Replace("  ", "")
                            + "\t" + myreader["city"].ToString().Replace("  ", "")
                            + "\t" + myreader["year"].ToString().Replace("  ", "")
                            + "\t" + myreader["model"].ToString().Replace("  ", "")
                            + "\t" + myreader["plate_num"].ToString().Replace("  ", ""));
                    }
                } catch (SqlException e1) { MessageBox.Show(e1.Message); }
                myreader.Close();*/

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

                        /*
                        remove_output.Items.Add(myreader["car_id"].ToString().Replace("  ", "")
                            + "\t" + myreader["description"].ToString().Replace("  ", "")
                            + "\t" + myreader["city"].ToString().Replace("  ", "")
                            + "\t" + myreader["year"].ToString().Replace("  ", "")
                            + "\t" + myreader["model"].ToString().Replace("  ", "")
                            + "\t" + myreader["plate_num"].ToString().Replace("  ", ""));*/
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

                        /*
                        remove_output.Items.Add(myreader["car_id"].ToString().Replace("  ", "")
                            + "\t" + myreader["description"].ToString().Replace("  ", "")
                            + "\t" + myreader["city"].ToString().Replace("  ", "")
                            + "\t" + myreader["year"].ToString().Replace("  ", "")
                            + "\t" + myreader["model"].ToString().Replace("  ", "")
                            + "\t" + myreader["plate_num"].ToString().Replace("  ", ""));*/
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

                        /*
                        remove_output.Items.Add(myreader["car_id"].ToString().Replace("  ", "")
                            + "\t" + myreader["description"].ToString().Replace("  ", "")
                            + "\t" + myreader["city"].ToString().Replace("  ", "")
                            + "\t" + myreader["year"].ToString().Replace("  ", "")
                            + "\t" + myreader["model"].ToString().Replace("  ", "")
                            + "\t" + myreader["plate_num"].ToString().Replace("  ", ""));*/
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

                        /*
                        remove_output.Items.Add(myreader["car_id"].ToString().Replace("  ", "")
                            + "\t" + myreader["description"].ToString().Replace("  ", "")
                            + "\t" + myreader["city"].ToString().Replace("  ", "")
                            + "\t" + myreader["year"].ToString().Replace("  ", "")
                            + "\t" + myreader["model"].ToString().Replace("  ", "")
                            + "\t" + myreader["plate_num"].ToString().Replace("  ", ""));*/
                    }
                } catch (SqlException e5) { MessageBox.Show(e5.Message); }
                myreader.Close();

            }

            //if (remove_output.Items.Count == 0)
              //  remove_output.Items.Add("No Results");
        }

        private void Remove_Output_DoubleClick(object sender, MouseEventArgs e)
        {
            if (removeOutput.SelectedItems != null)
            {
                string[] aCar = new string[6];

                ListViewItem aSelection = removeOutput.SelectedItems[0];
                

                aCar[0] = (aSelection.SubItems[0].Text);
                aCar[1] = descToType((aSelection.SubItems[1].Text));
                aCar[2] = cityToBID((aSelection.SubItems[2].Text));
                aCar[3] = (aSelection.SubItems[3].Text);
                aCar[4] = (aSelection.SubItems[4].Text);
                aCar[5] = (aSelection.SubItems[5].Text);


                string message = "Are you sure you would like to remove: ";
                for (int i1 = 0; i1 < aCar.Length; i1++)
                {
                    message += aCar[i1].ToString() + " ";
                }
                DialogResult removeCar = MessageBox.Show(message, "Remove", MessageBoxButtons.YesNo);

                string[] cols = { "car_id", "car_type", "car_branch", "model", "year", "plate_num" };
                mycommand.CommandText = "DELETE FROM car WHERE ";
                int i;
                for (i = 0; i < aCar.Length; i++)
                {

                    mycommand.CommandText += cols[i] + " = ";
                    mycommand.CommandText += "'" + aCar[i] + "'";
                    if (i + 1 == aCar.Length)
                        break;
                    mycommand.CommandText += " AND ";

                }
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

                //string car;
                //int selectedCar;
                //selectedCar = remove_output.SelectedIndex;

                //message += remove_output.SelectedItem.ToString();



                //car = remove_output.SelectedItem.ToString();
                //string[] args = car.Split('\t');

                //args[1] = descToType(args[1]);
                //args[2] = cityToBID(args[2]);

                /*
                string[] cols = { "car_id", "car_type", "car_branch", "year", "model", "plate_num" };
                mycommand.CommandText = "DELETE FROM car WHERE ";
                int i;
                for (i = 0; i < args.Length; i++)
                {

                    mycommand.CommandText += cols[i] + " = ";
                    mycommand.CommandText += "'" + args[i] + "'";
                    if (i + 1 == args.Length)
                        break;
                    mycommand.CommandText += " AND ";

                }
                if (removeCar == DialogResult.Yes)
                {
                    try
                    {
                        mycommand.ExecuteNonQuery();
                    } catch (SqlException) { MessageBox.Show("Something went wrong..\nThe car could not be removed.\nPlease try again", "ERROR"); }
                    MessageBox.Show("Car removed from the system.");
                    remove_output.Items.RemoveAt(selectedCar);

                }*/


            }
        }
        //------------------------------------------------END REMOVE CARS-------------------------------------------------------//





        //-----------------------------------------------------EDIT CAR---------------------------------------------------------//
        private void editCar_id(object sender, EventArgs e)
        {
            edit_output.Items.Clear();
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
                //MessageBox.Show("Editing: " + edit_output.SelectedItem.ToString());
            }
            
            //MessageBox.Show(aSelection.SubItems[0].Text);
            //MessageBox.Show(aSelection.SubItems[1].Text);
            //string[] aCar = new string[15];
            //editOutput.SelectedItems.CopyTo(aCar, 0);
            //int j = 0;
            //foreach (int i in aSelection)
            //{
            //  MessageBox.Show(editOutput.Items[i].ToString());
            //for (j = 0; j < 6; j++)
            // MessageBox.Show(editOutput.Items[i].SubItems[j].ToString());

            //}
            //MessageBox.Show(aSelection.ToString());
            //for (int i = 0; i < aCar.Length; i++)
                //MessageBox.Show(aCar[i]);


        }



        private void editCar_Submit(object sender, EventArgs e)
        {
            string[] carInfo = new string[6];
            ListViewItem anItem;
            editOutput.Items.Clear();
            //edit_output.Items.Clear();
            if (editcar_id.Text.Length != 0)
            {
                searchForCarsWithID(this, editcar_id.Text);
                /*mycommand.CommandText = "SELECT * FROM car C, branch B, type T WHERE (C.car_branch = B.branch_id AND C.car_type = T.type_id) AND car_id = " + "'" + editcar_id.Text + "'";
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

                        /*edit_output.Items.Add(myreader["car_id"].ToString().Replace("  ", "")
                            + "\t" + myreader["description"].ToString()
                            + "\t" + myreader["city"].ToString().Replace("  ", "")
                            + "\t" + myreader["year"].ToString().Replace("  ", "")
                            + "\t" + myreader["model"].ToString().Replace("  ", "")
                            + "\t" + myreader["plate_num"].ToString().Replace("  ", ""));
                    }
                } catch (SqlException e5) { MessageBox.Show(e5.Message); }
                myreader.Close();*/

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
                        /*
                        edit_output.Items.Add(myreader["car_id"].ToString().Replace("  ", "")
                            + "\t" + myreader["description"].ToString()
                            + "\t" + myreader["city"].ToString().Replace("  ", "")
                            + "\t" + myreader["year"].ToString().Replace("  ", "")
                            + "\t" + myreader["model"].ToString().Replace("  ", "")
                            + "\t" + myreader["plate_num"].ToString().Replace("  ", ""));*/
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
                        /*
                        edit_output.Items.Add(myreader["car_id"].ToString().Replace("  ", "")
                            + "\t" + myreader["description"].ToString()
                            + "\t" + myreader["city"].ToString().Replace("  ", "")
                            + "\t" + myreader["year"].ToString().Replace("  ", "")
                            + "\t" + myreader["model"].ToString().Replace("  ", "")
                            + "\t" + myreader["plate_num"].ToString().Replace("  ", ""));*/
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
                        /*
                        edit_output.Items.Add(myreader["car_id"].ToString().Replace("  ", "")
                            + "\t" + myreader["description"].ToString()
                            + "\t" + myreader["city"].ToString().Replace("  ", "")
                            + "\t" + myreader["year"].ToString().Replace("  ", "")
                            + "\t" + myreader["model"].ToString().Replace("  ", "")
                            + "\t" + myreader["plate_num"].ToString().Replace("  ", ""));*/
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
                        /*
                        edit_output.Items.Add(myreader["car_id"].ToString().Replace("  ", "")
                            + "\t" + myreader["description"].ToString()
                            + "\t" + myreader["city"].ToString().Replace("  ", "")
                            + "\t" + myreader["year"].ToString().Replace("  ", "")
                            + "\t" + myreader["model"].ToString().Replace("  ", "")
                            + "\t" + myreader["plate_num"].ToString().Replace("  ", ""));*/
                    }
                }
                catch (SqlException e1) { MessageBox.Show(e1.Message);}
                myreader.Close();

            }

            //if (edit_output.Items.Count == 0)
                //edit_output.Items.Add("No Results");


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




                    /*while (myreader.Read())
                    {
                    remove_output.Items.Add(myreader["car_id"].ToString().Replace("  ", "")
                        + "\t" + myreader["description"].ToString().Replace("  ", "")
                        + "\t" + myreader["city"].ToString().Replace("  ", "")
                        + "\t" + myreader["year"].ToString().Replace("  ", "")
                        + "\t" + myreader["model"].ToString().Replace("  ", "")
                        + "\t" + myreader["plate_num"].ToString().Replace("  ", ""));
                         }
                    } catch (SqlException e1) { MessageBox.Show(e1.Message);}
                    myreader.Close();
                    }*/


        private void searchForCarsWithIDEdit(object sender, string anID)
        {
            editOutput.Items.Clear();
            string[] carInfo = new string[6];
            ListViewItem anItem;
            mycommand.CommandText = "SELECT * FROM car C, branch B, type T ";
            mycommand.CommandText += "WHERE (C.car_branch = B.branch_id AND C.car_type = T.type_id) ";
            mycommand.CommandText += "AND car_id like " + "'" + anID + "%'";

            try {
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
                    /*
                    edit_output.Items.Add(myreader["car_id"].ToString().Replace("  ", "")
                        + "\t" + myreader["description"].ToString().Replace("  ", "")
                        + "\t" + myreader["city"].ToString().Replace("  ", "")
                        + "\t" + myreader["year"].ToString().Replace("  ", "")
                        + "\t" + myreader["model"].ToString().Replace("  ", "")
                        + "\t" + myreader["plate_num"].ToString().Replace("  ", ""));*/
                }
                } catch(Exception e) { MessageBox.Show(e.Message); }
            myreader.Close();
        }



        //Allows enter key to be used as a click for the submit button for each tab page
        private void tabPage_Switch(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage1"])
            {
                this.AcceptButton = button1;
                booking_output.DataSource = null;
                user_id.ResetText();
                user_info.Visible = false;
                drop_off_check.Checked = default;
            }
                
            else if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage2"])
            {
                this.AcceptButton = submit_return;
                return_id_error.Visible = false;
                returnOutput.Items.Clear();
                return_id.ResetText();
            }
                
            else if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage3"])
            {
                this.AcceptButton = InventoryButton;
                //InventoryBranch.SelectedIndex = 0;
                //this.dateTimePicker6.Text = default;
            }
                
            else if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage4"])
            {
                this.AcceptButton = report_submit;
                reportbox.Items.Clear();
            }
                
            else if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage5"])
            {
                this.AcceptButton = addcar_submit;
            }
                
            else if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage6"])
            {
                remcar_id.Text = default;
                this.AcceptButton = remove_submit;
                //remove_output.DataSource = null;
                removeOutput.Items.Clear();
                remcar_type.SelectedIndex = 0;
                remcar_branch.SelectedIndex = 0;
                remcar_year.Text = default;
                remcar_plate.Text = default;
                remcar_model.Text = default;

            }
                
            else if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage7"])
            {
                editcar_id.Text = default;
                this.AcceptButton = editSubmit;
                editOutput.Items.Clear();
                //edit_output.DataSource = null;
                //edit_output.Items.Clear();
                edit_branch.SelectedIndex = 0;
                edit_type.SelectedIndex = 0;
                edit_model.Text = default;
                edit_plate.Text = default;
                edit_year.Text = default;
                
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
            string[] args = returnOutput.Text.Split('\t');
            string drop_off = cityToBID(return_dropoff.SelectedItem.ToString());
            float final_price = float.Parse(args[8]);
            bool late = false, change = false;
            string message = "Return Car: " + args[1] + " To " + return_dropoff.Text;

            //Check if dropped of at different branch - Only apply fee if cust not gold
            if (drop_off != args[6])
            {
                if (get_cust_status(return_id.Text) == false)
                {
                    change = true;
                    message += "\nChange Branch Fee Applied";
                }
            }

            //Check if the drop off is late
            if (DateTime.Parse(args[5]) < dropoff_date.Value)
            {
                late = true;
                message += "\nLate Dropoff Fee Applied";
            }
            final_price += calc_price(DateTime.Parse(args[4]), dropoff_date.Value, args[7], late, change);

            DialogResult confirm = MessageBox.Show( message + "\nFinal Price: " + final_price.ToString(), "Confirm", MessageBoxButtons.OKCancel);
            if (confirm == DialogResult.OK)
            {
                mycommand.CommandText += "UPDATE booking SET ";
                mycommand.CommandText += "branchTo = '" + drop_off + "', ";
                mycommand.CommandText += "[returned] = '" + dropoff_date.Text + "' ";
                mycommand.CommandText += "WHERE booking_id = '" + args[0] + "'";
                try
                {
                    mycommand.ExecuteNonQuery();
                    MessageBox.Show("Return Processed");
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Cannot Complete Return: " + ex.Message);
                    Clipboard.SetText(mycommand.CommandText);
                }
                myreader.Close();
            }
        }

        private void submit_return_Click(object sender, EventArgs e)
        {
            returnOutput.Items.Clear();
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
                }catch (SqlException ex)
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
            //MessageBox.Show(mycommand.CommandText);
            Clipboard.SetText(mycommand.CommandText);
            //List <string> = new List<string>
            try
            {
                myreader = mycommand.ExecuteReader();
                while (myreader.Read())
                {
                    string temp = "";
                    temp += myreader["booking_id"].ToString() + "\t" + myreader["car_id"].ToString() + "\t";
                    temp += myreader["description"].ToString() + "\t" + myreader["plate_num"].ToString() + "\t";
                    temp += DateTime.Parse(myreader["date_From"].ToString()).Date.ToString("d") + "\t" + DateTime.Parse(myreader["date_to"].ToString()).Date.ToString("d") + "\t";
                    temp += myreader["branchFrom"].ToString() + "\t" + myreader["type_requested"] + "\t" + myreader["price"].ToString();
                    returnOutput.Items.Add(temp);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            myreader.Close();
        }
    }




    /* UNUSED FOR NOW
    private void searchForCarsWithoutID(object sender, IList<string> args)
    {
        mycommand.CommandText = "SELECT * FROM car WHERE car_branch = " + "'" + args[0] + "'" + " AND car_type = " + "'" + args[1] + "'";

        if (remcar_plate.Text.ToString().Length != 0)
            mycommand.CommandText += " AND plate_num like " + "'" + remcar_plate.Text.ToString() + "%'";

        if (remcar_model.Text.ToString().Length != 0)
            mycommand.CommandText += " AND model like " + "'" + remcar_model.Text.ToString() + "%'";

        if (remcar_year.Text.ToString().Length != 0)
            mycommand.CommandText += " AND year like " + "'" + remcar_year.Text.ToString() + "%'";

        myreader = mycommand.ExecuteReader();
        while (myreader.Read())
        {
            remove_output.Items.Add(myreader["car_id"].ToString() + "\t" + myreader["car_type"].ToString() + "\t" + myreader["car_branch"].ToString() + "\t" + myreader["year"].ToString() + "\t" + myreader["model"].ToString() + "\t" + myreader["plate_num"].ToString());
        }
        myreader.Close();
    }*/
    
}
