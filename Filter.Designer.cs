using System.ComponentModel;

namespace NHLPlayers
{
    partial class Filter
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            this.lbFilters = new System.Windows.Forms.Label();
            this.lbOrders = new System.Windows.Forms.Label();
            this.tbFilters = new System.Windows.Forms.TextBox();
            this.tbOrders = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lbFilters
            // 
            this.lbFilters.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbFilters.Location = new System.Drawing.Point(12, 9);
            this.lbFilters.Name = "lbFilters";
            this.lbFilters.Size = new System.Drawing.Size(100, 23);
            this.lbFilters.TabIndex = 0;
            this.lbFilters.Text = "Filters";
            // 
            // lbOrders
            // 
            this.lbOrders.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbOrders.Location = new System.Drawing.Point(12, 238);
            this.lbOrders.Name = "lbOrders";
            this.lbOrders.Size = new System.Drawing.Size(100, 23);
            this.lbOrders.TabIndex = 1;
            this.lbOrders.Text = "Orders";
            // 
            // tbFilters
            // 
            this.tbFilters.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbFilters.Location = new System.Drawing.Point(12, 35);
            this.tbFilters.Multiline = true;
            this.tbFilters.Name = "tbFilters";
            this.tbFilters.Size = new System.Drawing.Size(700, 200);
            this.tbFilters.TabIndex = 2;
            // 
            // tbOrders
            // 
            this.tbOrders.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbOrders.Location = new System.Drawing.Point(12, 264);
            this.tbOrders.Multiline = true;
            this.tbOrders.Name = "tbOrders";
            this.tbOrders.Size = new System.Drawing.Size(700, 200);
            this.tbOrders.TabIndex = 3;
            // 
            // Filter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(724, 476);
            this.Controls.Add(this.tbOrders);
            this.Controls.Add(this.tbFilters);
            this.Controls.Add(this.lbOrders);
            this.Controls.Add(this.lbFilters);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Filter";
            this.ShowIcon = false;
            this.Text = "Filter";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.TextBox tbOrders;

        private System.Windows.Forms.Label lbFilters;
        private System.Windows.Forms.Label lbOrders;
        private System.Windows.Forms.TextBox tbFilters;

        #endregion
    }
}