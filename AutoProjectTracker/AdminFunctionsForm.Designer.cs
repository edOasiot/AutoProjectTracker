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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.startDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.endDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.dayRadioButton = new System.Windows.Forms.RadioButton();
            this.yearRadioButton = new System.Windows.Forms.RadioButton();
            this.monthRadioButton = new System.Windows.Forms.RadioButton();
            this.weekRadioButton = new System.Windows.Forms.RadioButton();
            this.taskRadioButton = new System.Windows.Forms.RadioButton();
            this.employeeRadioButton = new System.Windows.Forms.RadioButton();
            this.taskEmployeeComboBox = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ShowProfitsB
            // 
            this.ShowProfitsB.Location = new System.Drawing.Point(12, 27);
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
            this.WorkerB.Location = new System.Drawing.Point(12, 101);
            this.WorkerB.Name = "WorkerB";
            this.WorkerB.Size = new System.Drawing.Size(111, 23);
            this.WorkerB.TabIndex = 5;
            this.WorkerB.Text = "Proceed As Worker";
            this.WorkerB.UseVisualStyleBackColor = true;
            this.WorkerB.Click += new System.EventHandler(this.WorkerB_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(406, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Start Date";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(575, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "End Date";
            // 
            // startDateTimePicker
            // 
            this.startDateTimePicker.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.startDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.startDateTimePicker.Location = new System.Drawing.Point(373, 29);
            this.startDateTimePicker.Name = "startDateTimePicker";
            this.startDateTimePicker.Size = new System.Drawing.Size(138, 20);
            this.startDateTimePicker.TabIndex = 8;
            // 
            // endDateTimePicker
            // 
            this.endDateTimePicker.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.endDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.endDateTimePicker.Location = new System.Drawing.Point(533, 30);
            this.endDateTimePicker.Name = "endDateTimePicker";
            this.endDateTimePicker.Size = new System.Drawing.Size(141, 20);
            this.endDateTimePicker.TabIndex = 9;
            // 
            // dayRadioButton
            // 
            this.dayRadioButton.AutoSize = true;
            this.dayRadioButton.Location = new System.Drawing.Point(130, 31);
            this.dayRadioButton.Name = "dayRadioButton";
            this.dayRadioButton.Size = new System.Drawing.Size(44, 17);
            this.dayRadioButton.TabIndex = 10;
            this.dayRadioButton.TabStop = true;
            this.dayRadioButton.Text = "Day";
            this.dayRadioButton.UseVisualStyleBackColor = true;
            this.dayRadioButton.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            // 
            // yearRadioButton
            // 
            this.yearRadioButton.AutoSize = true;
            this.yearRadioButton.Location = new System.Drawing.Point(299, 31);
            this.yearRadioButton.Name = "yearRadioButton";
            this.yearRadioButton.Size = new System.Drawing.Size(47, 17);
            this.yearRadioButton.TabIndex = 11;
            this.yearRadioButton.TabStop = true;
            this.yearRadioButton.Text = "Year";
            this.yearRadioButton.UseVisualStyleBackColor = true;
            this.yearRadioButton.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            // 
            // monthRadioButton
            // 
            this.monthRadioButton.AutoSize = true;
            this.monthRadioButton.Location = new System.Drawing.Point(238, 31);
            this.monthRadioButton.Name = "monthRadioButton";
            this.monthRadioButton.Size = new System.Drawing.Size(55, 17);
            this.monthRadioButton.TabIndex = 12;
            this.monthRadioButton.TabStop = true;
            this.monthRadioButton.Text = "Month";
            this.monthRadioButton.UseVisualStyleBackColor = true;
            this.monthRadioButton.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            // 
            // weekRadioButton
            // 
            this.weekRadioButton.AutoSize = true;
            this.weekRadioButton.Location = new System.Drawing.Point(180, 31);
            this.weekRadioButton.Name = "weekRadioButton";
            this.weekRadioButton.Size = new System.Drawing.Size(54, 17);
            this.weekRadioButton.TabIndex = 13;
            this.weekRadioButton.TabStop = true;
            this.weekRadioButton.Text = "Week";
            this.weekRadioButton.UseVisualStyleBackColor = true;
            this.weekRadioButton.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            // 
            // taskRadioButton
            // 
            this.taskRadioButton.AutoSize = true;
            this.taskRadioButton.Location = new System.Drawing.Point(130, 54);
            this.taskRadioButton.Name = "taskRadioButton";
            this.taskRadioButton.Size = new System.Drawing.Size(49, 17);
            this.taskRadioButton.TabIndex = 14;
            this.taskRadioButton.TabStop = true;
            this.taskRadioButton.Text = "Task";
            this.taskRadioButton.UseVisualStyleBackColor = true;
            this.taskRadioButton.CheckedChanged += new System.EventHandler(this.taskRadioButton_CheckedChanged);
            // 
            // employeeRadioButton
            // 
            this.employeeRadioButton.AutoSize = true;
            this.employeeRadioButton.Location = new System.Drawing.Point(180, 54);
            this.employeeRadioButton.Name = "employeeRadioButton";
            this.employeeRadioButton.Size = new System.Drawing.Size(71, 17);
            this.employeeRadioButton.TabIndex = 15;
            this.employeeRadioButton.TabStop = true;
            this.employeeRadioButton.Text = "Employee";
            this.employeeRadioButton.UseVisualStyleBackColor = true;
            this.employeeRadioButton.CheckedChanged += new System.EventHandler(this.employeeRadioButton_CheckedChanged);
            // 
            // taskEmployeeComboBox
            // 
            this.taskEmployeeComboBox.FormattingEnabled = true;
            this.taskEmployeeComboBox.Location = new System.Drawing.Point(373, 56);
            this.taskEmployeeComboBox.Name = "taskEmployeeComboBox";
            this.taskEmployeeComboBox.Size = new System.Drawing.Size(301, 21);
            this.taskEmployeeComboBox.TabIndex = 16;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 130);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(111, 23);
            this.button1.TabIndex = 17;
            this.button1.Text = "Reports";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // AdminFunctionsForm
            // 
            this.AcceptButton = this.ShowProfitsB;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 192);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.taskEmployeeComboBox);
            this.Controls.Add(this.employeeRadioButton);
            this.Controls.Add(this.taskRadioButton);
            this.Controls.Add(this.weekRadioButton);
            this.Controls.Add(this.monthRadioButton);
            this.Controls.Add(this.yearRadioButton);
            this.Controls.Add(this.dayRadioButton);
            this.Controls.Add(this.endDateTimePicker);
            this.Controls.Add(this.startDateTimePicker);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.WorkerB);
            this.Controls.Add(this.ShowProfitsB);
            this.Name = "AdminFunctionsForm";
            this.Text = "Admin Functions";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button ShowProfitsB;
        private System.Windows.Forms.Button WorkerB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker startDateTimePicker;
        private System.Windows.Forms.DateTimePicker endDateTimePicker;
        private System.Windows.Forms.RadioButton dayRadioButton;
        private System.Windows.Forms.RadioButton yearRadioButton;
        private System.Windows.Forms.RadioButton monthRadioButton;
        private System.Windows.Forms.RadioButton weekRadioButton;
        private System.Windows.Forms.RadioButton taskRadioButton;
        private System.Windows.Forms.RadioButton employeeRadioButton;
        private System.Windows.Forms.ComboBox taskEmployeeComboBox;
        private System.Windows.Forms.Button button1;
    }
}

