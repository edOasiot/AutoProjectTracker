namespace AutoProjectTracker
{
    partial class TaskHourForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.EnterTimeB = new System.Windows.Forms.Button();
            this.StartDate = new System.Windows.Forms.DateTimePicker();
            this.EndDate = new System.Windows.Forms.DateTimePicker();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(40, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Start Time:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(40, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "End Time:";
            // 
            // EnterTimeB
            // 
            this.EnterTimeB.Location = new System.Drawing.Point(153, 97);
            this.EnterTimeB.Name = "EnterTimeB";
            this.EnterTimeB.Size = new System.Drawing.Size(75, 23);
            this.EnterTimeB.TabIndex = 4;
            this.EnterTimeB.Text = "Enter Time";
            this.EnterTimeB.UseVisualStyleBackColor = true;
            this.EnterTimeB.Click += new System.EventHandler(this.EnterTimeB_Click);
            // 
            // StartDate
            // 
            this.StartDate.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.StartDate.Location = new System.Drawing.Point(108, 36);
            this.StartDate.Name = "StartDate";
            this.StartDate.ShowUpDown = true;
            this.StartDate.Size = new System.Drawing.Size(120, 20);
            this.StartDate.TabIndex = 5;
            // 
            // EndDate
            // 
            this.EndDate.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.EndDate.Location = new System.Drawing.Point(108, 62);
            this.EndDate.Name = "EndDate";
            this.EndDate.ShowUpDown = true;
            this.EndDate.Size = new System.Drawing.Size(120, 20);
            this.EndDate.TabIndex = 6;
            // 
            // TaskHourForm
            // 
            this.AcceptButton = this.EnterTimeB;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(290, 153);
            this.Controls.Add(this.EndDate);
            this.Controls.Add(this.StartDate);
            this.Controls.Add(this.EnterTimeB);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "TaskHourForm";
            this.Text = "Task / Hour Entry";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button EnterTimeB;
        private System.Windows.Forms.DateTimePicker StartDate;
        private System.Windows.Forms.DateTimePicker EndDate;
    }
}

