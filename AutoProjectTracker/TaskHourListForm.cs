using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoProjectTracker
{
    public partial class TaskHourListForm : MyForm
    {
        public TaskHourListForm(Int32 employeeId, Int32 taskId)
        {
            InitializeComponent();

            StartPosition = FormStartPosition.Manual;
            Location = new Point(115, 115);

            TableName = Utility.settings.TaskHourTable;
            KeyColumn = "EmployeeId,TaskId";
            KeyColumnValue = employeeId.ToString() + "," + taskId.ToString();
            TableType = "TaskHour";

            SetDBConnection();

            List<TaskHour> taskHours = ReadRecords<TaskHour>();
            dataGridView1.DataSource = taskHours;

            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                if (column.Name.EndsWith("Id"))
                    column.Visible = false;
                else if (ListColumnsNotShown != null && ListColumnsNotShown.Split(',').Contains(column.Name))
                    column.Visible = false;
                else
                    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }

        }

        private void CancelB_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dataGridView1_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (e.StateChanged != DataGridViewElementStates.Selected || dataGridView1.SelectedRows.Count.Equals(0)) 
                return;

            object id = dataGridView1.SelectedRows[0].Cells[0].Value;
            int index = dataGridView1.Columns["TaskName"].Index;
            Text = dataGridView1.SelectedRows[0].Cells[index].Value.ToString();
        }
    }
}
