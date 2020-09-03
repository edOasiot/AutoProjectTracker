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
    public partial class EmployeeForm : Form
    {
        private Settings settings = new Settings();
        private static SQLiteConnection dbConnection;
        public EmployeeForm(string Username)
        {
            InitializeComponent();
        
            SetDefaults();

            ReadRecord(Username);
        }

        private void SetDefaults()
        {
            settings = settings = Utility.ReadSettings();

            if (dbConnection == null)
                dbConnection = new SQLiteConnection("Data Source=" + settings.AutoProjectTrackerDB + ";Version=3;");

            if (!dbConnection.State.Equals(ConnectionState.Open))
                dbConnection.Open();


        }

        private void ReadRecord(string Username)
        {
            String sql = "select * from " + settings.EmployeeTable;
            sql += " Where UserName = '" + Username + "'";

            SQLiteDataAdapter adapter = new SQLiteDataAdapter(sql, dbConnection);
            DataSet dataSet = new DataSet();
            List<Employee> employees = null;

            try
            {
                adapter.Fill(dataSet);
                DataTable dtt = dataSet.Tables[0];
                employees = dtt.DataTableToList<Employee>();
            }
            catch { }

            if (employees.Count.Equals(1))
            {
                //Object o = Utility.GetPropValue(this, "FirstNameTB");

                Utility.SetControlValue(this, employees[0]);

                //FirstNameTB.Text = employees[0].FirstName;
            }
        }

    }
}
