namespace CMPT291PROJECT
{
    partial class Edit
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
            this.label27 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.edit_plate = new System.Windows.Forms.TextBox();
            this.edit_model = new System.Windows.Forms.TextBox();
            this.yearLabel = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.edit_branch = new System.Windows.Forms.ComboBox();
            this.label19 = new System.Windows.Forms.Label();
            this.edit_type = new System.Windows.Forms.ComboBox();
            this.label18 = new System.Windows.Forms.Label();
            this.edit_year = new System.Windows.Forms.TextBox();
            this.edit_milage = new System.Windows.Forms.TextBox();
            this.addcar_submit = new System.Windows.Forms.Button();
            this.edit_cancel = new System.Windows.Forms.Button();
            this.editc_id_label = new System.Windows.Forms.Label();
            this.edit_carID = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(191, 214);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(71, 13);
            this.label27.TabIndex = 26;
            this.label27.Text = "Plate Number";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(214, 166);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(36, 13);
            this.label26.TabIndex = 25;
            this.label26.Text = "Model";
            // 
            // edit_plate
            // 
            this.edit_plate.Location = new System.Drawing.Point(268, 211);
            this.edit_plate.Name = "edit_plate";
            this.edit_plate.Size = new System.Drawing.Size(121, 20);
            this.edit_plate.TabIndex = 24;
            // 
            // edit_model
            // 
            this.edit_model.Location = new System.Drawing.Point(268, 160);
            this.edit_model.Name = "edit_model";
            this.edit_model.Size = new System.Drawing.Size(121, 20);
            this.edit_model.TabIndex = 23;
            // 
            // yearLabel
            // 
            this.yearLabel.AutoSize = true;
            this.yearLabel.Location = new System.Drawing.Point(16, 214);
            this.yearLabel.Name = "yearLabel";
            this.yearLabel.Size = new System.Drawing.Size(29, 13);
            this.yearLabel.TabIndex = 22;
            this.yearLabel.Text = "Year";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(214, 112);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(41, 13);
            this.label20.TabIndex = 21;
            this.label20.Text = "Branch";
            // 
            // edit_branch
            // 
            this.edit_branch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.edit_branch.FormattingEnabled = true;
            this.edit_branch.Location = new System.Drawing.Point(268, 106);
            this.edit_branch.Name = "edit_branch";
            this.edit_branch.Size = new System.Drawing.Size(121, 21);
            this.edit_branch.TabIndex = 20;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(13, 163);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(44, 13);
            this.label19.TabIndex = 19;
            this.label19.Text = "Mileage";
            // 
            // edit_type
            // 
            this.edit_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.edit_type.FormattingEnabled = true;
            this.edit_type.Location = new System.Drawing.Point(60, 109);
            this.edit_type.Name = "edit_type";
            this.edit_type.Size = new System.Drawing.Size(121, 21);
            this.edit_type.TabIndex = 18;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(13, 109);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(31, 13);
            this.label18.TabIndex = 17;
            this.label18.Text = "Type";
            // 
            // edit_year
            // 
            this.edit_year.Location = new System.Drawing.Point(60, 211);
            this.edit_year.Name = "edit_year";
            this.edit_year.Size = new System.Drawing.Size(121, 20);
            this.edit_year.TabIndex = 16;
            // 
            // edit_milage
            // 
            this.edit_milage.Location = new System.Drawing.Point(60, 160);
            this.edit_milage.Name = "edit_milage";
            this.edit_milage.Size = new System.Drawing.Size(121, 20);
            this.edit_milage.TabIndex = 15;
            // 
            // addcar_submit
            // 
            this.addcar_submit.Location = new System.Drawing.Point(60, 263);
            this.addcar_submit.Name = "addcar_submit";
            this.addcar_submit.Size = new System.Drawing.Size(75, 23);
            this.addcar_submit.TabIndex = 14;
            this.addcar_submit.Text = "Submit";
            this.addcar_submit.UseVisualStyleBackColor = true;
            this.addcar_submit.Click += new System.EventHandler(this.editSubmit);
            // 
            // edit_cancel
            // 
            this.edit_cancel.Location = new System.Drawing.Point(314, 263);
            this.edit_cancel.Name = "edit_cancel";
            this.edit_cancel.Size = new System.Drawing.Size(75, 23);
            this.edit_cancel.TabIndex = 28;
            this.edit_cancel.Text = "Cancel";
            this.edit_cancel.UseVisualStyleBackColor = true;
            this.edit_cancel.Click += new System.EventHandler(this.editCar_Cancel);
            // 
            // editc_id_label
            // 
            this.editc_id_label.AutoSize = true;
            this.editc_id_label.Location = new System.Drawing.Point(13, 65);
            this.editc_id_label.Name = "editc_id_label";
            this.editc_id_label.Size = new System.Drawing.Size(37, 13);
            this.editc_id_label.TabIndex = 29;
            this.editc_id_label.Text = "Car ID";
            // 
            // edit_carID
            // 
            this.edit_carID.AutoSize = true;
            this.edit_carID.Location = new System.Drawing.Point(60, 65);
            this.edit_carID.Name = "edit_carID";
            this.edit_carID.Size = new System.Drawing.Size(35, 13);
            this.edit_carID.TabIndex = 30;
            this.edit_carID.Text = "label2";
            // 
            // Edit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(516, 299);
            this.Controls.Add(this.edit_carID);
            this.Controls.Add(this.editc_id_label);
            this.Controls.Add(this.edit_cancel);
            this.Controls.Add(this.label27);
            this.Controls.Add(this.label26);
            this.Controls.Add(this.edit_plate);
            this.Controls.Add(this.edit_model);
            this.Controls.Add(this.yearLabel);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.edit_branch);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.edit_type);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.edit_year);
            this.Controls.Add(this.edit_milage);
            this.Controls.Add(this.addcar_submit);
            this.Name = "Edit";
            this.Text = "Editing Vehicle Information";
            this.Load += new System.EventHandler(this.Edit_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.TextBox edit_plate;
        private System.Windows.Forms.TextBox edit_model;
        private System.Windows.Forms.Label yearLabel;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.ComboBox edit_branch;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.ComboBox edit_type;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox edit_year;
        private System.Windows.Forms.TextBox edit_milage;
        private System.Windows.Forms.Button addcar_submit;
        private System.Windows.Forms.Button edit_cancel;
        private System.Windows.Forms.Label editc_id_label;
        private System.Windows.Forms.Label edit_carID;
    }
}