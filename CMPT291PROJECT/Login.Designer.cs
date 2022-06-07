namespace CMPT291PROJECT
{
    partial class Login
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
            this.existing_user = new System.Windows.Forms.Button();
            this.new_user = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.user_id = new System.Windows.Forms.TextBox();
            this.employee_check = new System.Windows.Forms.CheckBox();
            this.enter_username = new System.Windows.Forms.Label();
            this.error_text = new System.Windows.Forms.Label();
            this.debug = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // existing_user
            // 
            this.existing_user.Location = new System.Drawing.Point(108, 137);
            this.existing_user.Name = "existing_user";
            this.existing_user.Size = new System.Drawing.Size(86, 23);
            this.existing_user.TabIndex = 0;
            this.existing_user.Text = "Existing User";
            this.existing_user.UseVisualStyleBackColor = true;
            this.existing_user.Click += new System.EventHandler(this.existing_user_Click);
            // 
            // new_user
            // 
            this.new_user.Location = new System.Drawing.Point(236, 137);
            this.new_user.Name = "new_user";
            this.new_user.Size = new System.Drawing.Size(86, 23);
            this.new_user.TabIndex = 1;
            this.new_user.Text = "New User";
            this.new_user.UseVisualStyleBackColor = true;
            this.new_user.Click += new System.EventHandler(this.new_user_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(150, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Welcome To 291 Rentals";
            // 
            // user_id
            // 
            this.user_id.Location = new System.Drawing.Point(108, 88);
            this.user_id.Name = "user_id";
            this.user_id.Size = new System.Drawing.Size(214, 20);
            this.user_id.TabIndex = 3;
            // 
            // employee_check
            // 
            this.employee_check.AutoSize = true;
            this.employee_check.Location = new System.Drawing.Point(344, 174);
            this.employee_check.Name = "employee_check";
            this.employee_check.Size = new System.Drawing.Size(72, 17);
            this.employee_check.TabIndex = 5;
            this.employee_check.Text = "Employee";
            this.employee_check.UseVisualStyleBackColor = true;
            this.employee_check.CheckedChanged += new System.EventHandler(this.employee_check_CheckedChanged);
            // 
            // enter_username
            // 
            this.enter_username.AutoSize = true;
            this.enter_username.Location = new System.Drawing.Point(21, 91);
            this.enter_username.Name = "enter_username";
            this.enter_username.Size = new System.Drawing.Size(81, 13);
            this.enter_username.TabIndex = 6;
            this.enter_username.Text = "Enter username";
            this.enter_username.Click += new System.EventHandler(this.label2_Click);
            // 
            // error_text
            // 
            this.error_text.AutoSize = true;
            this.error_text.ForeColor = System.Drawing.Color.Red;
            this.error_text.Location = new System.Drawing.Point(105, 121);
            this.error_text.Name = "error_text";
            this.error_text.Size = new System.Drawing.Size(52, 13);
            this.error_text.TabIndex = 7;
            this.error_text.Text = "Welcome";
            // 
            // debug
            // 
            this.debug.AutoSize = true;
            this.debug.Checked = true;
            this.debug.CheckState = System.Windows.Forms.CheckState.Checked;
            this.debug.Location = new System.Drawing.Point(344, 151);
            this.debug.Name = "debug";
            this.debug.Size = new System.Drawing.Size(56, 17);
            this.debug.TabIndex = 8;
            this.debug.Text = "debug";
            this.debug.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.debug.UseVisualStyleBackColor = true;
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(428, 203);
            this.Controls.Add(this.debug);
            this.Controls.Add(this.error_text);
            this.Controls.Add(this.enter_username);
            this.Controls.Add(this.employee_check);
            this.Controls.Add(this.user_id);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.new_user);
            this.Controls.Add(this.existing_user);
            this.Name = "Login";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Login_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button existing_user;
        private System.Windows.Forms.Button new_user;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox user_id;
        private System.Windows.Forms.CheckBox employee_check;
        private System.Windows.Forms.Label enter_username;
        private System.Windows.Forms.Label error_text;
        private System.Windows.Forms.CheckBox debug;
    }
}

