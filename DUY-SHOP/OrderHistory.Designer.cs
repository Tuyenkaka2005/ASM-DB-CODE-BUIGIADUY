﻿namespace DUY_SHOP
{
    partial class OrderHistory
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
            this.dtgOrderHistory = new System.Windows.Forms.DataGridView();
            this.btnBack = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dtgOrderHistory)).BeginInit();
            this.SuspendLayout();
            // 
            // dtgOrderHistory
            // 
            this.dtgOrderHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgOrderHistory.Location = new System.Drawing.Point(52, 29);
            this.dtgOrderHistory.Name = "dtgOrderHistory";
            this.dtgOrderHistory.RowHeadersWidth = 51;
            this.dtgOrderHistory.RowTemplate.Height = 24;
            this.dtgOrderHistory.Size = new System.Drawing.Size(689, 341);
            this.dtgOrderHistory.TabIndex = 0;
            this.dtgOrderHistory.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgOrderHistory_CellClick);
            this.dtgOrderHistory.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgOrderHistory_CellContentClick);
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(641, 392);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(113, 46);
            this.btnBack.TabIndex = 2;
            this.btnBack.Text = "btnBack";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // OrderHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.dtgOrderHistory);
            this.Name = "OrderHistory";
            this.Text = "OrderHistory";
            this.Load += new System.EventHandler(this.OrderHistory_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgOrderHistory)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dtgOrderHistory;
        private System.Windows.Forms.Button btnBack;
    }
}