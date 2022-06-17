namespace CMPT291PROJECT
{
    partial class Inventory
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
            this.CurrentInventory = new System.Windows.Forms.ListView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.InvBack = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // CurrentInventory
            // 
            this.CurrentInventory.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CurrentInventory.GridLines = true;
            this.CurrentInventory.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.CurrentInventory.HideSelection = false;
            this.CurrentInventory.Location = new System.Drawing.Point(12, 150);
            this.CurrentInventory.MultiSelect = false;
            this.CurrentInventory.Name = "CurrentInventory";
            this.CurrentInventory.Size = new System.Drawing.Size(776, 288);
            this.CurrentInventory.TabIndex = 0;
            this.CurrentInventory.UseCompatibleStateImageBehavior = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "label1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(225, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "label2";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(491, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "label3";
            // 
            // InvBack
            // 
            this.InvBack.Location = new System.Drawing.Point(713, 22);
            this.InvBack.Name = "InvBack";
            this.InvBack.Size = new System.Drawing.Size(75, 23);
            this.InvBack.TabIndex = 5;
            this.InvBack.Text = "Back";
            this.InvBack.UseVisualStyleBackColor = true;
            this.InvBack.Click += new System.EventHandler(this.BackInv);
            // 
            // Inventory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.InvBack);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CurrentInventory);
            this.Name = "Inventory";
            this.Text = "Inventory";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.InventoryClose);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView CurrentInventory;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button InvBack;
    }
}