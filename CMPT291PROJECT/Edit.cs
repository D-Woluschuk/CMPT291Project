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
    public partial class Edit : Form
    {
        public Login parent;
        public SqlConnection myconnection;
        public SqlCommand mycommand;
        public SqlDataReader myreader;
        public string[] args;

        public Edit(Login f1, string[] aCar)
        {
            InitializeComponent();
            parent = f1;
            myconnection = f1.myconnection;
            myreader = f1.myreader;
            mycommand = f1.mycommand;

            //Allows enter key strokes to trigger submit button clicks
            AcceptButton = addcar_submit;

            //Allows ESC key strokes to trigger cancel button clicks
            CancelButton = edit_cancel;

            args = aCar;

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
                myreader.Close();


                branches = branches.Distinct().ToList();
                types = types.Distinct().ToList();

                
                edit_branch.DataSource = branches;
                edit_type.DataSource = types;

                edit_carID.Text = aCar[0];
                edit_type.SelectedItem = aCar[1];
                edit_branch.SelectedItem = aCar[2];
                edit_model.Text = aCar[3];
                edit_year.Text = aCar[4];
                edit_plate.Text = aCar[5];
                
            }
            
            catch (Exception e3) { MessageBox.Show(e3.ToString()); }

            
        }

        private void Edit_Load(object sender, EventArgs e)
        {

        }

        private void editSubmit(object sender, EventArgs e)
        {
            string carType = "";
            string carBranch = "";
            string confirmation = "";
            string updateType = "";
            string updateBranch = "";
            string updateYear = "";
            string updateModel = "";
            string updatePlate = "";
            bool type = false;
            bool branch = false;
            bool year = false;
            bool model = false;
            bool plate = false;
            mycommand.CommandText = "SELECT T.type_id, B.branch_id FROM car C, branch B, type T WHERE (C.car_branch = B.branch_id AND C.car_type = T.type_id) AND C.car_id = " + "'" + args[0] + "'";

            myreader = mycommand.ExecuteReader();

            while (myreader.Read())
            {
                carType += myreader["type_id"].ToString();
                carBranch += myreader["branch_id"].ToString();
            }
            myreader.Close();

            
            if (!edit_type.SelectedItem.ToString().Equals(args[1]))
            {
                confirmation += "Car type changing from '" + args[1] + "' To '" + edit_type.SelectedItem.ToString() + "' ?\n";
                mycommand.CommandText = "SELECT type_id FROM type WHERE description = " + "'" + edit_type.SelectedItem.ToString().Trim() + "'";
                myreader = mycommand.ExecuteReader();
                while (myreader.Read())
                    carType = myreader["type_id"].ToString();

                myreader.Close();
                updateType = "car_type = " + "'" + carType + "'";
                type = true;
            }

            if (!edit_branch.SelectedItem.ToString().Equals(args[2]))
            {
                confirmation += "Car branch changing from '" + args[2] + "' To '" + edit_branch.SelectedItem.ToString() + "' ?\n";
                mycommand.CommandText = "SELECT branch_id FROM branch WHERE city = " + "'" + edit_branch.SelectedItem.ToString().Trim() + "'";
                myreader = mycommand.ExecuteReader();
                while (myreader.Read())
                    carBranch = myreader["branch_id"].ToString();

                myreader.Close();
                updateBranch = "car_branch = " + "'" + carBranch + "'";
                branch = true;
            }

            if (!edit_year.Text.Equals(args[4]))
            {
                confirmation += "Car year changing from '" + args[4] + "' To '" + edit_year.Text + "' ?\n";
                updateYear = "year = " + "'" + edit_year.Text + "'";
                year = true;
            }

            if (!edit_model.Text.Equals(args[3]))
            {
                confirmation += "Car model changing from '" + args[3] + "' To '" + edit_model.Text + "' ?\n";
                updateModel = "model = " + "'" + edit_model.Text + "'";
                model = true;
            }

            if (!edit_plate.Text.Equals(args[5]))
            {
                confirmation += "Car licence plate changing from '" + args[5] + "' To '" + edit_plate.Text + "' ?\n";
                updatePlate = "plate_num = " + "'" + edit_plate.Text + "'";
                plate = true;
            }


            mycommand.CommandText = "UPDATE car SET ";
            if (type || branch || year || model || plate)
            {
                

                if (type)
                    mycommand.CommandText += updateType;

                if (branch && (type))
                    mycommand.CommandText += ", " + updateBranch;
                else
                    mycommand.CommandText += updateBranch;


                if (year && (type || branch))
                    mycommand.CommandText += ", " + updateYear;
                else
                    mycommand.CommandText += updateYear;


                if (model && (type || branch || year))
                    mycommand.CommandText += ", " + updateModel;
                else
                    mycommand.CommandText += updateModel;

                if (plate && (type || branch || year || model))
                    mycommand.CommandText += ", " + updatePlate;
                else
                    mycommand.CommandText += updatePlate;


                mycommand.CommandText += " WHERE car_id = " + "'" + args[0] + "'";

                DialogResult editCar = MessageBox.Show("Are these changes correct?\n" + confirmation, "Confirm?", MessageBoxButtons.YesNo);

                if (editCar == DialogResult.Yes) 
                { 
                    mycommand.ExecuteNonQuery();
                    MessageBox.Show("Car was successfully updated!");
                    this.Close();
                    return;
                }
            }


            DialogResult confirm = MessageBox.Show("No changes were made\n", "", MessageBoxButtons.RetryCancel);
            if (confirm != DialogResult.Retry)
                this.Close();
            else
            {
                edit_type.SelectedItem = args[1];
                edit_branch.SelectedItem = args[2];
                edit_model.Text = args[3];
                edit_year.Text = args[4];
                edit_plate.Text = args[5];

            }
        }

        private void editCar_Cancel(object sender, EventArgs e)
        {
            this.Close();
        }


        private string cityToBID(string city)
        {
            string aBID = "";
            mycommand.CommandText = "SELECT branch_id FROM branch WHERE city = " + "'" + city + "'";
            myreader = mycommand.ExecuteReader();
            while (myreader.Read())
                aBID += myreader["branch_id"].ToString().TrimEnd();
            myreader.Close();
            return aBID;
        }

        private string descToType(string description)
        {
            string aTypeID = "";
            mycommand.CommandText = "SELECT type_id FROM type WHERE description = " + "'" + description + "'";
            myreader = mycommand.ExecuteReader();
            while (myreader.Read())
                aTypeID += myreader["type_id"].ToString().TrimEnd();
            myreader.Close();
            return aTypeID;


        }
    }
}
