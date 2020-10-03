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
    public partial class ProjectListForm : MyForm
    {
        Employee employee = null;
        public ProjectListForm(Employee _employee)
        {
            InitializeComponent();

            employee = _employee;

            StartPosition = FormStartPosition.Manual;
            Location = new Point(55, 55);

            TableName = "Projects";
            KeyColumn = "ProjectName";
            TableType = "Project";

            if (employee.Role.Equals(Convert.ToInt32(Enums.Roles.Worker)))
                ListColumnsNotShown = "Phone,Email,HourlyRate,CurrentProfit";

            List<Project> projects = ReadRecords<Project>();
            dataGridView1.DataSource = projects;

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
            int index = dataGridView1.Columns["ProjectName"].Index;
            Text = dataGridView1.SelectedRows[0].Cells[index].Value.ToString();

            TaskListForm taskListForm = new TaskListForm(employee.Id, Convert.ToInt32(id));
            taskListForm.ShowDialog();
        }

        private void newB_Click(object sender, EventArgs e)
        {
            ProjectForm projectForm = new ProjectForm(0);
            projectForm.ShowDialog();
        }
    }
}
