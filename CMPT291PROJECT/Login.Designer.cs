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
            this.existing_user.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.existing_user.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.existing_user.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.existing_user.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.existing_user.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.existing_user.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.existing_user.Location = new System.Drawing.Point(297, 438);
            this.existing_user.Margin = new System.Windows.Forms.Padding(6);
            this.existing_user.Name = "existing_user";
            this.existing_user.Size = new System.Drawing.Size(190, 80);
            this.existing_user.TabIndex = 0;
            this.existing_user.Text = "Existing User";
            this.existing_user.UseVisualStyleBackColor = false;
            this.existing_user.Click += new System.EventHandler(this.existing_user_Click);
            // 
            // new_user
            // 
            this.new_user.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.new_user.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.new_user.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.new_user.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.new_user.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.new_user.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.new_user.Location = new System.Drawing.Point(645, 438);
            this.new_user.Margin = new System.Windows.Forms.Padding(6);
            this.new_user.Name = "new_user";
            this.new_user.Size = new System.Drawing.Size(190, 80);
            this.new_user.TabIndex = 1;
            this.new_user.Text = "New User";
            this.new_user.UseVisualStyleBackColor = false;
            this.new_user.Click += new System.EventHandler(this.new_user_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(391, 39);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(342, 33);
            this.label1.TabIndex = 2;
            this.label1.Text = "Welcome To 291 Rentals";
            // 
            // user_id
            // 
            this.user_id.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.user_id.Location = new System.Drawing.Point(369, 284);
            this.user_id.Margin = new System.Windows.Forms.Padding(6);
            this.user_id.Name = "user_id";
            this.user_id.Size = new System.Drawing.Size(389, 35);
            this.user_id.TabIndex = 3;
            // 
            // employee_check
            // 
            this.employee_check.AutoSize = true;
            this.employee_check.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.employee_check.Location = new System.Drawing.Point(934, 553);
            this.employee_check.Margin = new System.Windows.Forms.Padding(6);
            this.employee_check.Name = "employee_check";
            this.employee_check.Size = new System.Drawing.Size(141, 33);
            this.employee_check.TabIndex = 5;
            this.employee_check.Text = "Employee";
            this.employee_check.UseVisualStyleBackColor = true;
            this.employee_check.CheckedChanged += new System.EventHandler(this.employee_check_CheckedChanged);
            // 
            // enter_username
            // 
            this.enter_username.AutoSize = true;
            this.enter_username.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.enter_username.Location = new System.Drawing.Point(124, 284);
            this.enter_username.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.enter_username.Name = "enter_username";
            this.enter_username.Size = new System.Drawing.Size(206, 31);
            this.enter_username.TabIndex = 6;
            this.enter_username.Text = "Enter username";
            this.enter_username.Click += new System.EventHandler(this.label2_Click);
            // 
            // error_text
            // 
            this.error_text.AutoSize = true;
            this.error_text.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.error_text.ForeColor = System.Drawing.Color.Red;
            this.error_text.Location = new System.Drawing.Point(291, 374);
            this.error_text.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.error_text.Name = "error_text";
            this.error_text.Size = new System.Drawing.Size(126, 31);
            this.error_text.TabIndex = 7;
            this.error_text.Text = "Welcome";
            // 
            // debug
            // 
            this.debug.AutoSize = true;
            this.debug.Checked = true;
            this.debug.CheckState = System.Windows.Forms.CheckState.Checked;
            this.debug.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.debug.Location = new System.Drawing.Point(934, 512);
            this.debug.Margin = new System.Windows.Forms.Padding(6);
            this.debug.Name = "debug";
            this.debug.Size = new System.Drawing.Size(101, 33);
            this.debug.TabIndex = 8;
            this.debug.Text = "debug";
            this.debug.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.debug.UseVisualStyleBackColor = true;
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(1107, 601);
            this.Controls.Add(this.debug);
            this.Controls.Add(this.error_text);
            this.Controls.Add(this.enter_username);
            this.Controls.Add(this.employee_check);
            this.Controls.Add(this.user_id);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.new_user);
            this.Controls.Add(this.existing_user);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.ImeMode = System.Windows.Forms.ImeMode.Close;
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Welcome!";
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

