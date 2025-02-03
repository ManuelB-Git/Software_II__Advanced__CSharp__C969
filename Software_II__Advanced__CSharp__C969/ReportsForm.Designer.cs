namespace Software_II__Advanced__CSharp__C969
{
    partial class ReportsForm
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
            this.dgvReports = new System.Windows.Forms.DataGridView();
            this.btnReport1 = new System.Windows.Forms.Button();
            this.btnReport2 = new System.Windows.Forms.Button();
            this.btnReport3 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReports)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvReports
            // 
            this.dgvReports.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvReports.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReports.Location = new System.Drawing.Point(12, 12);
            this.dgvReports.Name = "dgvReports";
            this.dgvReports.Size = new System.Drawing.Size(1024, 333);
            this.dgvReports.TabIndex = 0;
            // 
            // btnReport1
            // 
            this.btnReport1.Location = new System.Drawing.Point(175, 351);
            this.btnReport1.Name = "btnReport1";
            this.btnReport1.Size = new System.Drawing.Size(190, 23);
            this.btnReport1.TabIndex = 1;
            this.btnReport1.Text = "Appointment Types by Month";
            this.btnReport1.UseVisualStyleBackColor = true;
            this.btnReport1.Click += new System.EventHandler(this.btnReport1_Click);
            // 
            // btnReport2
            // 
            this.btnReport2.Location = new System.Drawing.Point(415, 351);
            this.btnReport2.Name = "btnReport2";
            this.btnReport2.Size = new System.Drawing.Size(214, 23);
            this.btnReport2.TabIndex = 2;
            this.btnReport2.Text = "Schedule for Each Consultant";
            this.btnReport2.UseVisualStyleBackColor = true;
            this.btnReport2.Click += new System.EventHandler(this.btnReport2_Click);
            // 
            // btnReport3
            // 
            this.btnReport3.Location = new System.Drawing.Point(711, 351);
            this.btnReport3.Name = "btnReport3";
            this.btnReport3.Size = new System.Drawing.Size(218, 23);
            this.btnReport3.TabIndex = 3;
            this.btnReport3.Text = "Total Appointments per Customer";
            this.btnReport3.UseVisualStyleBackColor = true;
            this.btnReport3.Click += new System.EventHandler(this.btnReport3_Click);
            // 
            // ReportsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1048, 468);
            this.Controls.Add(this.btnReport3);
            this.Controls.Add(this.btnReport2);
            this.Controls.Add(this.btnReport1);
            this.Controls.Add(this.dgvReports);
            this.Name = "ReportsForm";
            this.Text = "ReportsForm";
            ((System.ComponentModel.ISupportInitialize)(this.dgvReports)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvReports;
        private System.Windows.Forms.Button btnReport1;
        private System.Windows.Forms.Button btnReport2;
        private System.Windows.Forms.Button btnReport3;
    }
}