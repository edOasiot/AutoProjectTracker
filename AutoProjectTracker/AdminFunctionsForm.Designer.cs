namespace AutoProjectTracker
{
    partial class AdminFunctionsForm
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
            this.ShowProfitsB = new System.Windows.Forms.Button();
            this.WorkerB = new System.Windows.Forms.Button();
            this.ShowMonthlyProfitB = new System.Windows.Forms.Button();
            this.ShowYearlyProfitB = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ShowProfitsB
            // 
            this.ShowProfitsB.Location = new System.Drawing.Point(12, 12);
            this.ShowProfitsB.Name = "ShowProfitsB";
            this.ShowProfitsB.Size = new System.Drawing.Size(111, 23);
            this.ShowProfitsB.TabIndex = 4;
            this.ShowProfitsB.TabStop = false;
            this.ShowProfitsB.Text = "Show Profits";
            this.ShowProfitsB.UseVisualStyleBackColor = true;
            this.ShowProfitsB.Click += new System.EventHandler(this.ShowProfitsB_Click);
            // 
            // WorkerB
            // 
            this.WorkerB.Location = new System.Drawing.Point(12, 41);
            this.WorkerB.Name = "WorkerB";
            this.WorkerB.Size = new System.Drawing.Size(111, 23);
            this.WorkerB.TabIndex = 5;
            this.WorkerB.Text = "Proceed As Worker";
            this.WorkerB.UseVisualStyleBackColor = true;
            this.WorkerB.Click += new System.EventHandler(this.WorkerB_Click);
            // 
            // ShowMonthlyProfitB
            // 
            this.ShowMonthlyProfitB.Location = new System.Drawing.Point(12, 70);
            this.ShowMonthlyProfitB.Name = "ShowMonthlyProfitB";
            this.ShowMonthlyProfitB.Size = new System.Drawing.Size(111, 23);
            this.ShowMonthlyProfitB.TabIndex = 6;
            this.ShowMonthlyProfitB.Text = "Show Monthly Profit";
            this.ShowMonthlyProfitB.UseVisualStyleBackColor = true;
            this.ShowMonthlyProfitB.Click += new System.EventHandler(this.ShowMonthlyProfitB_Click);
            // 
            // ShowYearlyProfitB
            // 
            this.ShowYearlyProfitB.Location = new System.Drawing.Point(12, 99);
            this.ShowYearlyProfitB.Name = "ShowYearlyProfitB";
            this.ShowYearlyProfitB.Size = new System.Drawing.Size(111, 23);
            this.ShowYearlyProfitB.TabIndex = 7;
            this.ShowYearlyProfitB.Text = "Show Yearly Profit";
            this.ShowYearlyProfitB.UseVisualStyleBackColor = true;
            this.ShowYearlyProfitB.Click += new System.EventHandler(this.ShowYearlyProfitB_Click);
            // 
            // AdminFunctionsForm
            // 
            this.AcceptButton = this.ShowProfitsB;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(374, 136);
            this.Controls.Add(this.ShowYearlyProfitB);
            this.Controls.Add(this.ShowMonthlyProfitB);
            this.Controls.Add(this.WorkerB);
            this.Controls.Add(this.ShowProfitsB);
            this.Name = "AdminFunctionsForm";
            this.Text = "Admin Functions";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button ShowProfitsB;
        private System.Windows.Forms.Button WorkerB;
        private System.Windows.Forms.Button ShowMonthlyProfitB;
        private System.Windows.Forms.Button ShowYearlyProfitB;
    }
}

