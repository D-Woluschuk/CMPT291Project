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
            // Inherit SQL Connection
            parent = f1;
            myconnection = f1.myconnection;
            myreader = f1.myreader;
            mycommand = f1.mycommand;
            editOutput.View = View.Details;
            editOutput.GridLines = true;
            editOutput.FullRowSelect = true;

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

            bookingOutput.Columns.Add("Branch");
            bookingOutput.Columns.Add("Type");
            bookingOutput.Columns.Add("Model");
            bookingOutput.Columns.Add("Year");
            bookingOutput.Columns.Add("Plate");
            bookingOutput.Columns.Add("Price");


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

        private void button1_Click(object sender, EventArgs e)
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
                    if (myreader["Gold_status"].ToString() == "1") { user_info.Text += "\nGold Status"; }
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
            string[] aCar = new string[7];
            ListViewItem anItem;
            try
            {
                myreader = mycommand.ExecuteReader();
                while (myreader.Read())
                {
                    aCar[0] = myreader["city"].ToString();
                    aCar[1] = myreader["description"].ToString();
                    aCar[2] = myreader["model"].ToString();
                    aCar[3] = myreader["year"].ToString();
                    aCar[4] = myreader["plate_num"].ToString();
                    aCar[5] = calculate_price(date_from.Value, date_to.Value, myreader["daily"].ToString(), myreader["weekly"].ToString(), myreader["monthly"].ToString()).ToString();
                    
                    anItem = new ListViewItem(aCar);
                    bookingOutput.Items.Add(anItem);

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
            /*
            mycommand.CommandText = "SELECT daily, weekly, monthly, late_fee FROM type WHERE description = '" + type_desc + "'";
            try
            {
                myreader = mycommand.ExecuteReader();
                while (myreader.Read())
                {
                    daily = float.Parse(myreader["daily"].ToString());
                    weekly = float.Parse(myreader["weekly"].ToString());
                    monthly = float.Parse(myreader["monthly"].ToString());
                    late_fee = float.Parse(myreader["late_fee"].ToString());
                }

            }
            catch (SqlException ex) { MessageBox.Show("Cannot find price of " + type_desc + ex.Message); }
            myreader.Close();
            */
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
                    if (myreader["Gold_status"].ToString() == "1") { user_info.Text += "\nGold Status"; }
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
                mycommand.CommandText = "DELETE FROM car WHERE ";

                aCar[0] = (aSelection.SubItems[0].Text);
                aCar[1] = descToType((aSelection.SubItems[1].Text));
                aCar[2] = cityToBID((aSelection.SubItems[2].Text));
                aCar[3] = (aSelection.SubItems[3].Text);
                aCar[4] = (aSelection.SubItems[4].Text);
                aCar[5] = (aSelection.SubItems[5].Text);

                int i;
                for (i = 0; i < aCar.Length; i++)
                {

                    mycommand.CommandText += cols[i] + " = ";
                    mycommand.CommandText += "'" + aCar[i] + "'";
                    if (i + 1 == aCar.Length)
                        break;
                    mycommand.CommandText += " AND ";

                }
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
                drop_off_check.Checked = default;
            }
                
            else if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage2"])
            {
                this.AcceptButton = submit_return;
                returnOutput.Items.Clear();
                return_id.ResetText();
            }
                
            else if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage3"])
            {
                this.AcceptButton = InventoryButton;
                InventoryBranch.SelectedIndex = 0;
                invDateFrom.ResetText();
                invDateTo.ResetText();
            }
                
            else if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage4"])
            {
                this.AcceptButton = report_submit;
                reportbox.Items.Clear();
            }
                
            else if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage5"])
            {
                this.AcceptButton = addcar_submit;
                addcar_year.ResetText();
                addcar_model.ResetText();
                addcar_plate.ResetText();
            }
                
            else if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage6"])
            {
                remcar_id.Text = default;
                this.AcceptButton = remove_submit;
                removeOutput.Items.Clear();
                remcar_type.SelectedIndex = 0;
                remcar_branch.SelectedIndex = 0;
                remcar_year.ResetText();
                remcar_plate.ResetText();
                remcar_model.ResetText();

            }
                
            else if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage7"])
            {
                editcar_id.Text = default;
                this.AcceptButton = editSubmit;
                editOutput.Items.Clear();
                edit_branch.SelectedIndex = 0;
                edit_type.SelectedIndex = 0;
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
