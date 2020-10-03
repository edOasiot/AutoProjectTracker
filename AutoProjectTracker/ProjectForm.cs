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
        public ProjectForm(Int32 projectId)
        {
            InitializeComponent();

            TableName = "Projects";
            KeyColumn = "Id";
            KeyColumnValue = projectId.ToString();
            TableType = "Project";

            if (ReadRecord() != null)
                InsertMode = false;

            SetDefaults();
        }

        private void SetDefaults()
        {
            if (HourlyRate.Text.Length.Equals(0))
                HourlyRate.Text = Utility.settings.HourlyRate.ToString();

            if (InsertMode)
                SaveInsertB.Text = "Insert";
            else
                SaveInsertB.Text = "Save";
        }

        private void CancelB_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SaveInsertB_Click(object sender, EventArgs e)
        {
            if (InsertMode)
                InsertRecord();
            else
                UpdateRecord();
        }
    }
}
