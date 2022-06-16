namespace CMPT291PROJECT
{
    partial class Customer
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
            this.date_from = new System.Windows.Forms.DateTimePicker();
            this.date_to = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pickup_branch = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.vehicle_type = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.submit = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.log_out = new System.Windows.Forms.Button();
            this.custOutput = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // date_from
            // 
            this.date_from.Location = new System.Drawing.Point(156, 62);
            this.date_from.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.date_from.Name = "date_from";
            this.date_from.Size = new System.Drawing.Size(396, 31);
            this.date_from.TabIndex = 0;
            // 
            // date_to
            // 
            this.date_to.Location = new System.Drawing.Point(156, 160);
            this.date_to.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.date_to.Name = "date_to";
            this.date_to.Size = new System.Drawing.Size(396, 31);
            this.date_to.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 71);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "Date From:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(40, 169);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 25);
            this.label2.TabIndex = 3;
            this.label2.Text = "Date To: ";
            // 
            // pickup_branch
            // 
            this.pickup_branch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.pickup_branch.FormattingEnabled = true;
            this.pickup_branch.Items.AddRange(new object[] {
            "Any"});
            this.pickup_branch.Location = new System.Drawing.Point(209, 274);
            this.pickup_branch.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.pickup_branch.Name = "pickup_branch";
            this.pickup_branch.Size = new System.Drawing.Size(238, 33);
            this.pickup_branch.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(37, 280);
            this.label3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(157, 25);
            this.label3.TabIndex = 5;
            this.label3.Text = "Pick up Branch";
            // 
            // vehicle_type
            // 
            this.vehicle_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.vehicle_type.FormattingEnabled = true;
            this.vehicle_type.Items.AddRange(new object[] {
            "Any"});
            this.vehicle_type.Location = new System.Drawing.Point(207, 330);
            this.vehicle_type.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.vehicle_type.Name = "vehicle_type";
            this.vehicle_type.Size = new System.Drawing.Size(238, 33);
            this.vehicle_type.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(37, 336);
            this.label4.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(137, 25);
            this.label4.TabIndex = 7;
            this.label4.Text = "Vehicle Type";
            // 
            // submit
            // 
            this.submit.Location = new System.Drawing.Point(288, 431);
            this.submit.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.submit.Name = "submit";
            this.submit.Size = new System.Drawing.Size(150, 44);
            this.submit.TabIndex = 10;
            this.submit.Text = "Submit";
            this.submit.UseVisualStyleBackColor = true;
            this.submit.Click += new System.EventHandler(this.submit_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(833, 31);
            this.label5.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(151, 25);
            this.label5.TabIndex = 11;
            this.label5.Text = "Available Cars";
            // 
            // log_out
            // 
            this.log_out.Location = new System.Drawing.Point(24, 431);
            this.log_out.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.log_out.Name = "log_out";
            this.log_out.Size = new System.Drawing.Size(150, 44);
            this.log_out.TabIndex = 13;
            this.log_out.Text = "Log Out";
            this.log_out.UseVisualStyleBackColor = true;
            this.log_out.Click += new System.EventHandler(this.log_out_Click);
            // 
            // custOutput
            // 
            this.custOutput.BackColor = System.Drawing.Color.DarkGray;
            this.custOutput.FullRowSelect = true;
            this.custOutput.GridLines = true;
            this.custOutput.HideSelection = false;
            this.custOutput.Location = new System.Drawing.Point(670, 71);
            this.custOutput.Name = "custOutput";
            this.custOutput.Size = new System.Drawing.Size(532, 423);
            this.custOutput.TabIndex = 15;
            this.custOutput.UseCompatibleStateImageBehavior = false;
            this.custOutput.View = System.Windows.Forms.View.Details;
            // 
            // Customer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1204, 498);
            this.Controls.Add(this.custOutput);
            this.Controls.Add(this.log_out);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.submit);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.vehicle_type);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pickup_branch);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.date_to);
            this.Controls.Add(this.date_from);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Name = "Customer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "View Available Cars";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Customer_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker date_from;
        private System.Windows.Forms.DateTimePicker date_to;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox pickup_branch;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox vehicle_type;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button submit;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button log_out;
        private System.Windows.Forms.ListView custOutput;
    }
}