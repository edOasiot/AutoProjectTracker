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
        public ProjectListForm(Employee employee)
        {
            InitializeComponent();

            TableName = Utility.settings.ProjectTable;
            KeyColumn = "ProjectName";
            //KeyColumnValue = ProjectName;
            TableType = "Project";

            if (employee.Role.Equals(Convert.ToInt32(Enums.Roles.Worker)))
                ListColumnsNotShown = "Phone,Email,HourlyRate,CurrentProfit";

            SetDBConnection();

            List<Project> projects = ReadRecords<Project>();
            dataGridView1.DataSource = projects;

            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                if (ListColumnsNotShown != null && ListColumnsNotShown.Split(',').Contains(column.Name))
                    column.Visible = false;
            }

        }

        private void CancelB_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}
