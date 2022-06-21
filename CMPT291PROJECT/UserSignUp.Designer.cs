namespace CMPT291PROJECT
{
    partial class UserSignUp
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.signup_id = new System.Windows.Forms.TextBox();
            this.signup_Lname = new System.Windows.Forms.TextBox();
            this.signup_Fname = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.submit = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.employee_check = new System.Windows.Forms.CheckBox();
            this.signup_city = new System.Windows.Forms.TextBox();
            this.signup_street = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.signup_phone1 = new System.Windows.Forms.TextBox();
            this.signup_phone2 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.signup_branch_label = new System.Windows.Forms.Label();
            this.id_error = new System.Windows.Forms.Label();
            this.branch_error = new System.Windows.Forms.Label();
            this.signup_branch = new System.Windows.Forms.ComboBox();
            this.signup_back = new System.Windows.Forms.Button();
            this.Signup_Prov = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // signup_id
            // 
            this.signup_id.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.signup_id.Location = new System.Drawing.Point(233, 105);
            this.signup_id.Name = "signup_id";
            this.signup_id.Size = new System.Drawing.Size(193, 35);
            this.signup_id.TabIndex = 0;
            this.signup_id.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // signup_Lname
            // 
            this.signup_Lname.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.signup_Lname.Location = new System.Drawing.Point(233, 293);
            this.signup_Lname.Name = "signup_Lname";
            this.signup_Lname.Size = new System.Drawing.Size(193, 35);
            this.signup_Lname.TabIndex = 2;
            // 
            // signup_Fname
            // 
            this.signup_Fname.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.signup_Fname.Location = new System.Drawing.Point(233, 209);
            this.signup_Fname.Name = "signup_Fname";
            this.signup_Fname.Size = new System.Drawing.Size(193, 35);
            this.signup_Fname.TabIndex = 1;
            this.signup_Fname.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(57, 111);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(135, 29);
            this.label1.TabIndex = 3;
            this.label1.Text = "User Name";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(64, 299);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(128, 29);
            this.label2.TabIndex = 4;
            this.label2.Text = "Last Name";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(61, 220);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(131, 29);
            this.label3.TabIndex = 5;
            this.label3.Text = "First Name";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // submit
            // 
            this.submit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.submit.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.submit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.submit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.submit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.submit.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.submit.Location = new System.Drawing.Point(398, 524);
            this.submit.Name = "submit";
            this.submit.Size = new System.Drawing.Size(166, 57);
            this.submit.TabIndex = 6;
            this.submit.TabStop = false;
            this.submit.Text = "Submit";
            this.submit.UseVisualStyleBackColor = false;
            this.submit.Click += new System.EventHandler(this.submit_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(423, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(280, 37);
            this.label4.TabIndex = 7;
            this.label4.Text = "New User Sign Up";
            // 
            // employee_check
            // 
            this.employee_check.AutoSize = true;
            this.employee_check.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.employee_check.Location = new System.Drawing.Point(962, 524);
            this.employee_check.Name = "employee_check";
            this.employee_check.Size = new System.Drawing.Size(141, 33);
            this.employee_check.TabIndex = 8;
            this.employee_check.TabStop = false;
            this.employee_check.Text = "Employee";
            this.employee_check.UseVisualStyleBackColor = true;
            this.employee_check.CheckedChanged += new System.EventHandler(this.employee_check_CheckedChanged);
            // 
            // signup_city
            // 
            this.signup_city.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.signup_city.Location = new System.Drawing.Point(816, 170);
            this.signup_city.Name = "signup_city";
            this.signup_city.Size = new System.Drawing.Size(193, 35);
            this.signup_city.TabIndex = 5;
            // 
            // signup_street
            // 
            this.signup_street.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.signup_street.Location = new System.Drawing.Point(816, 249);
            this.signup_street.Name = "signup_street";
            this.signup_street.Size = new System.Drawing.Size(193, 35);
            this.signup_street.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(693, 176);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 29);
            this.label5.TabIndex = 12;
            this.label5.Text = "City";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(574, 255);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(172, 29);
            this.label7.TabIndex = 14;
            this.label7.Text = "Street Address";
            // 
            // signup_phone1
            // 
            this.signup_phone1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.signup_phone1.Location = new System.Drawing.Point(233, 372);
            this.signup_phone1.Name = "signup_phone1";
            this.signup_phone1.Size = new System.Drawing.Size(193, 35);
            this.signup_phone1.TabIndex = 3;
            // 
            // signup_phone2
            // 
            this.signup_phone2.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.signup_phone2.Location = new System.Drawing.Point(233, 444);
            this.signup_phone2.Name = "signup_phone2";
            this.signup_phone2.Size = new System.Drawing.Size(193, 35);
            this.signup_phone2.TabIndex = 7;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(109, 378);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(83, 29);
            this.label8.TabIndex = 17;
            this.label8.Text = "Phone";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(90, 450);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(102, 29);
            this.label9.TabIndex = 18;
            this.label9.Text = "Phone 2";
            // 
            // signup_branch_label
            // 
            this.signup_branch_label.AutoSize = true;
            this.signup_branch_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.signup_branch_label.Location = new System.Drawing.Point(658, 334);
            this.signup_branch_label.Name = "signup_branch_label";
            this.signup_branch_label.Size = new System.Drawing.Size(88, 29);
            this.signup_branch_label.TabIndex = 20;
            this.signup_branch_label.Text = "Branch";
            // 
            // id_error
            // 
            this.id_error.AutoSize = true;
            this.id_error.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.id_error.Location = new System.Drawing.Point(228, 143);
            this.id_error.Name = "id_error";
            this.id_error.Size = new System.Drawing.Size(98, 29);
            this.id_error.TabIndex = 21;
            this.id_error.Text = "id_error";
            // 
            // branch_error
            // 
            this.branch_error.AutoSize = true;
            this.branch_error.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.branch_error.Location = new System.Drawing.Point(811, 378);
            this.branch_error.Name = "branch_error";
            this.branch_error.Size = new System.Drawing.Size(0, 29);
            this.branch_error.TabIndex = 22;
            // 
            // signup_branch
            // 
            this.signup_branch.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.signup_branch.FormattingEnabled = true;
            this.signup_branch.Location = new System.Drawing.Point(816, 326);
            this.signup_branch.Name = "signup_branch";
            this.signup_branch.Size = new System.Drawing.Size(193, 37);
            this.signup_branch.TabIndex = 8;
            // 
            // signup_back
            // 
            this.signup_back.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.signup_back.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.signup_back.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.signup_back.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.signup_back.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.signup_back.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.signup_back.Location = new System.Drawing.Point(615, 524);
            this.signup_back.Name = "signup_back";
            this.signup_back.Size = new System.Drawing.Size(166, 57);
            this.signup_back.TabIndex = 23;
            this.signup_back.TabStop = false;
            this.signup_back.Text = "Back";
            this.signup_back.UseVisualStyleBackColor = false;
            this.signup_back.Click += new System.EventHandler(this.signup_back_Click);
            // 
            // Signup_Prov
            // 
            this.Signup_Prov.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Signup_Prov.FormattingEnabled = true;
            this.Signup_Prov.Location = new System.Drawing.Point(816, 103);
            this.Signup_Prov.Name = "Signup_Prov";
            this.Signup_Prov.Size = new System.Drawing.Size(193, 37);
            this.Signup_Prov.TabIndex = 24;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(639, 111);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(107, 29);
            this.label6.TabIndex = 13;
            this.label6.Text = "Province";
            // 
            // UserSignUp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(1145, 653);
            this.Controls.Add(this.Signup_Prov);
            this.Controls.Add(this.signup_back);
            this.Controls.Add(this.signup_branch);
            this.Controls.Add(this.branch_error);
            this.Controls.Add(this.id_error);
            this.Controls.Add(this.signup_branch_label);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.signup_phone2);
            this.Controls.Add(this.signup_phone1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.signup_street);
            this.Controls.Add(this.signup_city);
            this.Controls.Add(this.employee_check);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.submit);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.signup_Fname);
            this.Controls.Add(this.signup_Lname);
            this.Controls.Add(this.signup_id);
            this.Name = "UserSignUp";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sign Up";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.UserSignUp_FormClosing);
            this.Load += new System.EventHandler(this.UserSignUp_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox signup_id;
        private System.Windows.Forms.TextBox signup_Lname;
        private System.Windows.Forms.TextBox signup_Fname;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button submit;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox employee_check;
        private System.Windows.Forms.TextBox signup_city;
        private System.Windows.Forms.TextBox signup_street;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox signup_phone1;
        private System.Windows.Forms.TextBox signup_phone2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label signup_branch_label;
        private System.Windows.Forms.Label id_error;
        private System.Windows.Forms.Label branch_error;
        private System.Windows.Forms.ComboBox signup_branch;
        private System.Windows.Forms.Button signup_back;
        private System.Windows.Forms.ComboBox Signup_Prov;
        private System.Windows.Forms.Label label6;
    }
}