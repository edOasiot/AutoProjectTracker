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
    public partial class ProjectForm : MyForm
    {
        public ProjectForm(string ProjectName)
        {
            InitializeComponent();

            TableName = Utility.settings.ProjectTable;
            KeyColumn = "ProjectName";
            KeyColumnValue = ProjectName;
            TableType = "Project";

            SetDBConnection();
            ReadRecord();

            SetDefaults();

        }

        private void SetDefaults()
        {
            if (HourlyRate.Text.Length.Equals(0))
                HourlyRate.Text = Utility.settings.HourlyRate.ToString();

        }

        private void CancelB_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}
