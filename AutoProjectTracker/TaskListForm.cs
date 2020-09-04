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
    public partial class TaskListForm : MyForm
    {
        Int32 employeeId;
        public TaskListForm(Int32 _employeeId, Int32 projectId)
        {
            InitializeComponent();

            employeeId = _employeeId;

            StartPosition = FormStartPosition.Manual;
            Location = new Point(85, 85);

            TableName = Utility.settings.TaskTable;
            KeyColumn = "ProjectId";
            KeyColumnValue = projectId.ToString();
            TableType = "Task";

            SetDBConnection();

            List<Task> tasks = ReadRecords<Task>();
            dataGridView1.DataSource = tasks;

            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                if (column.Name.EndsWith("Id"))
                    column.Visible = false;
                else if (ListColumnsNotShown != null && ListColumnsNotShown.Split(',').Contains(column.Name))
                    column.Visible = false;
                else
                    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }

            DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            dataGridView1.Columns.Add(btn);
            btn.HeaderText = "Hours";
            btn.Text = "Show Hours";
            btn.Name = "ShowHours";
            btn.UseColumnTextForButtonValue = true;

            btn = new DataGridViewButtonColumn();
            dataGridView1.Columns.Add(btn);
            btn.HeaderText = "End";
            btn.Text = "End Task";
            btn.Name = "EndTask";
            btn.UseColumnTextForButtonValue = true;
        }

        private void CancelB_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dataGridView1_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (e.StateChanged != DataGridViewElementStates.Selected || dataGridView1.SelectedRows.Count.Equals(0)) 
                return;

            int index = dataGridView1.Columns["Id"].Index;
            object id = dataGridView1.SelectedRows[0].Cells[index].Value;
            index = dataGridView1.Columns["TaskName"].Index;
            Text = dataGridView1.SelectedRows[0].Cells[index].Value.ToString();

            TaskHourForm taskHourForm = new TaskHourForm(employeeId, Convert.ToInt32(id));
            taskHourForm.ShowDialog();

            index = dataGridView1.Columns["CurrentHours"].Index;
            dataGridView1.SelectedRows[0].Cells[index].Value = CalculateTaskHours(Convert.ToInt32(id));
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = dataGridView1.Columns["ShowHours"].Index;
            int index1 = dataGridView1.Columns["EndTask"].Index;

            if (e.ColumnIndex == index)
                MessageBox.Show((e.RowIndex + 1) + "  Row - ShowHours button clicked ");
            else if (e.ColumnIndex == index1)
                MessageBox.Show((e.RowIndex + 1) + "  Row - EndTask button clicked ");
        }
    }
}
