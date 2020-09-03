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
    public partial class ProjectForm : Form
    {
        private Settings settings = new Settings();
        private static SQLiteConnection dbConnection;
        public ProjectForm()
        {
            InitializeComponent();

            SetDefaults();

            ReadRecord("p1");
        }

        private void SetDefaults()
        {
            settings = settings = Utility.ReadSettings();

            if (dbConnection == null)
                dbConnection = new SQLiteConnection("Data Source=" + settings.AutoProjectTrackerDB + ";Version=3;");

            if (!dbConnection.State.Equals(ConnectionState.Open))
                dbConnection.Open();

            if (HourlyRate.Text.Length.Equals(0))
                HourlyRate.Text = settings.HourlyRate.ToString();

        }

        private void ReadRecord(string project)
        {
            String sql = "select * from " + settings.ProjectTable;
            sql += " Where ProjectName = '" + project + "'";

            SQLiteDataAdapter adapter = new SQLiteDataAdapter(sql, dbConnection);
            DataSet dataSet = new DataSet();
            List<Project> projects = null;

            try
            {
                adapter.Fill(dataSet);
                DataTable dtt = dataSet.Tables[0];
                projects = dtt.DataTableToList<Project>();
            }
            catch { }

            if (projects.Count.Equals(1))
            {
                Color.Text = projects[0].Color;
            }
        }
    }
}
