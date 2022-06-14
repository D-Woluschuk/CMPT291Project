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
            this.output = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // date_from
            // 
            this.date_from.Location = new System.Drawing.Point(78, 54);
            this.date_from.Name = "date_from";
            this.date_from.Size = new System.Drawing.Size(200, 20);
            this.date_from.TabIndex = 0;
            // 
            // date_to
            // 
            this.date_to.Location = new System.Drawing.Point(375, 54);
            this.date_to.Name = "date_to";
            this.date_to.Size = new System.Drawing.Size(200, 20);
            this.date_to.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Date From:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(317, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Date To: ";
            // 
            // pickup_branch
            // 
            this.pickup_branch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.pickup_branch.FormattingEnabled = true;
            this.pickup_branch.Items.AddRange(new object[] {
            "Any"});
            this.pickup_branch.Location = new System.Drawing.Point(98, 114);
            this.pickup_branch.Name = "pickup_branch";
            this.pickup_branch.Size = new System.Drawing.Size(121, 21);
            this.pickup_branch.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 117);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Pick up Branch";
            // 
            // vehicle_type
            // 
            this.vehicle_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.vehicle_type.FormattingEnabled = true;
            this.vehicle_type.Items.AddRange(new object[] {
            "Any"});
            this.vehicle_type.Location = new System.Drawing.Point(98, 169);
            this.vehicle_type.Name = "vehicle_type";
            this.vehicle_type.Size = new System.Drawing.Size(121, 21);
            this.vehicle_type.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 172);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Vehicle Type";
            // 
            // submit
            // 
            this.submit.Location = new System.Drawing.Point(144, 224);
            this.submit.Name = "submit";
            this.submit.Size = new System.Drawing.Size(75, 23);
            this.submit.TabIndex = 10;
            this.submit.Text = "Submit";
            this.submit.UseVisualStyleBackColor = true;
            this.submit.Click += new System.EventHandler(this.submit_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(262, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "View Available Cars";
            // 
            // log_out
            // 
            this.log_out.Location = new System.Drawing.Point(12, 224);
            this.log_out.Name = "log_out";
            this.log_out.Size = new System.Drawing.Size(75, 23);
            this.log_out.TabIndex = 13;
            this.log_out.Text = "Log Out";
            this.log_out.UseVisualStyleBackColor = true;
            this.log_out.Click += new System.EventHandler(this.log_out_Click);
            // 
            // output
            // 
            this.output.FormattingEnabled = true;
            this.output.Location = new System.Drawing.Point(320, 95);
            this.output.Name = "output";
            this.output.Size = new System.Drawing.Size(255, 147);
            this.output.TabIndex = 14;
            // 
            // Customer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(602, 259);
            this.Controls.Add(this.output);
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
            this.Name = "Customer";
            this.Text = "Form2";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Customer_FormClosing);
            this.Load += new System.EventHandler(this.Customer_Load);
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
        private System.Windows.Forms.ListBox output;
    }
}