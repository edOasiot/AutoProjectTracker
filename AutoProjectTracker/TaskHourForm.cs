using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Web.Security;

namespace AutoProjectTracker
{
    public partial class TaskHourForm : MyForm
    {
        public TaskHourForm(Int32 employeeId, Int32 taskId)
        {
            InitializeComponent();

            StartPosition = FormStartPosition.Manual;
            Location = new Point(115, 115);

            TableName = "TaskHours";
            KeyColumn = "EmployeeId,TaskId";
            KeyColumnValue = employeeId.ToString() + "," + taskId.ToString();
            TableType = "TaskHour";

            SetDefaults();
        }

        private void SetDefaults()
        {
            StartDate.Text = "8:00:00 AM";
            EndDate.Text = "4:00:00 PM";
        }

        private void EnterTimeB_Click(object sender, EventArgs e)
        {
            InsertRecord();

            Close();
        }
    }
}
