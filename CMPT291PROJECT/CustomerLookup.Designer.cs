namespace CMPT291PROJECT
{
    partial class CustomerLookup
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
            this.firstName = new System.Windows.Forms.TextBox();
            this.LastName = new System.Windows.Forms.TextBox();
            this.Street = new System.Windows.Forms.TextBox();
            this.City = new System.Windows.Forms.TextBox();
            this.fName = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.custLookupResults = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Info = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.Phone = new System.Windows.Forms.TextBox();
            this.CustLookupSubmit = new System.Windows.Forms.Button();
            this.province = new System.Windows.Forms.ComboBox();
            this.lookupCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // firstName
            // 
            this.firstName.Location = new System.Drawing.Point(79, 60);
            this.firstName.Name = "firstName";
            this.firstName.Size = new System.Drawing.Size(100, 20);
            this.firstName.TabIndex = 0;
            // 
            // LastName
            // 
            this.LastName.Location = new System.Drawing.Point(79, 104);
            this.LastName.Name = "LastName";
            this.LastName.Size = new System.Drawing.Size(100, 20);
            this.LastName.TabIndex = 1;
            // 
            // Street
            // 
            this.Street.Location = new System.Drawing.Point(79, 148);
            this.Street.Name = "Street";
            this.Street.Size = new System.Drawing.Size(100, 20);
            this.Street.TabIndex = 2;
            // 
            // City
            // 
            this.City.Location = new System.Drawing.Point(79, 198);
            this.City.Name = "City";
            this.City.Size = new System.Drawing.Size(100, 20);
            this.City.TabIndex = 3;
            // 
            // fName
            // 
            this.fName.AutoSize = true;
            this.fName.Location = new System.Drawing.Point(16, 63);
            this.fName.Name = "fName";
            this.fName.Size = new System.Drawing.Size(57, 13);
            this.fName.TabIndex = 4;
            this.fName.Text = "First Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 107);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Last Name";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(38, 151);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Street";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(38, 201);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(24, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "City";
            // 
            // custLookupResults
            // 
            this.custLookupResults.FormattingEnabled = true;
            this.custLookupResults.Location = new System.Drawing.Point(422, 31);
            this.custLookupResults.Name = "custLookupResults";
            this.custLookupResults.Size = new System.Drawing.Size(366, 407);
            this.custLookupResults.TabIndex = 8;
            this.custLookupResults.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.customerSelected);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 250);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Province";
            // 
            // Info
            // 
            this.Info.AutoSize = true;
            this.Info.Location = new System.Drawing.Point(143, 13);
            this.Info.Name = "Info";
            this.Info.Size = new System.Drawing.Size(35, 13);
            this.Info.TabIndex = 11;
            this.Info.Text = "label5";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(35, 300);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Phone";
            // 
            // Phone
            // 
            this.Phone.Location = new System.Drawing.Point(79, 293);
            this.Phone.Name = "Phone";
            this.Phone.Size = new System.Drawing.Size(100, 20);
            this.Phone.TabIndex = 13;
            // 
            // CustLookupSubmit
            // 
            this.CustLookupSubmit.Location = new System.Drawing.Point(79, 355);
            this.CustLookupSubmit.Name = "CustLookupSubmit";
            this.CustLookupSubmit.Size = new System.Drawing.Size(75, 23);
            this.CustLookupSubmit.TabIndex = 14;
            this.CustLookupSubmit.Text = "Submit";
            this.CustLookupSubmit.UseVisualStyleBackColor = true;
            this.CustLookupSubmit.Click += new System.EventHandler(this.LookupSubmit);
            // 
            // province
            // 
            this.province.FormattingEnabled = true;
            this.province.Location = new System.Drawing.Point(79, 250);
            this.province.Name = "province";
            this.province.Size = new System.Drawing.Size(100, 21);
            this.province.TabIndex = 15;
            // 
            // lookupCancel
            // 
            this.lookupCancel.Location = new System.Drawing.Point(187, 355);
            this.lookupCancel.Name = "lookupCancel";
            this.lookupCancel.Size = new System.Drawing.Size(75, 23);
            this.lookupCancel.TabIndex = 16;
            this.lookupCancel.Text = "Cancel";
            this.lookupCancel.UseVisualStyleBackColor = true;
            this.lookupCancel.Click += new System.EventHandler(this.CustLookupCancel);
            // 
            // CustomerLookup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(795, 445);
            this.Controls.Add(this.lookupCancel);
            this.Controls.Add(this.province);
            this.Controls.Add(this.CustLookupSubmit);
            this.Controls.Add(this.Phone);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.Info);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.custLookupResults);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.fName);
            this.Controls.Add(this.City);
            this.Controls.Add(this.Street);
            this.Controls.Add(this.LastName);
            this.Controls.Add(this.firstName);
            this.Name = "CustomerLookup";
            this.Text = "Customer Lookup";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox firstName;
        private System.Windows.Forms.TextBox LastName;
        private System.Windows.Forms.TextBox Street;
        private System.Windows.Forms.TextBox City;
        private System.Windows.Forms.Label fName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox custLookupResults;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label Info;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox Phone;
        private System.Windows.Forms.Button CustLookupSubmit;
        private System.Windows.Forms.ComboBox province;
        private System.Windows.Forms.Button lookupCancel;
    }
}