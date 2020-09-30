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
    public partial class AdminFunctionsForm : MyForm
    {
        Employee employee = null;
        public AdminFunctionsForm(Employee _employee)
        {
            InitializeComponent();

            employee = _employee;

            StartPosition = FormStartPosition.Manual;
            Location = new Point(55, 55);

            TableName = Utility.settings.ProjectTable;
            KeyColumn = "ProjectName";
            TableType = "Project";

            SetDBConnection();

        }

        private void ShowProfitsB_Click(object sender, EventArgs e)
        {
            string msgText = "Daily Profit: " + 2 + Environment.NewLine;
            msgText += "Weekly Profit: " + 2 + Environment.NewLine;

            MessageBox.Show(msgText);
        }

        private void WorkerB_Click(object sender, EventArgs e)
        {
            ProjectListForm projectListForm = new ProjectListForm(employee);
            projectListForm.Show();
        }

        private void ShowMonthlyProfitB_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Show Monthly button clicked ");

        }

        private void ShowYearlyProfitB_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Show Yearly button clicked ");

        }

        public Double ReadProfit()
        {
            Double Profit = 0;
            String sql = "select ";
            sql += " from " + TableName;

            adapter = new SQLiteDataAdapter(sql, dbConnection);
            DataSet dataSet = new DataSet();

            try
            {
                adapter.Fill(dataSet);
                DataTable dtt = dataSet.Tables[0];
                return Profit;
            }
            catch { }

            return 0;
        }
    }
}
