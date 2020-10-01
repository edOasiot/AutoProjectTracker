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
            string msg = "";

            msg += "Daily Profit = $" + ReadProfit(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day)) + Environment.NewLine;
            msg += "Weekly Profit = $" + ReadProfit(Utility.StartOfWeek(DayOfWeek.Sunday)) + Environment.NewLine;
            msg += "Monthly Profit = $" + ReadProfit(new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)) + Environment.NewLine;
            msg += "Yearly Profit = $" + ReadProfit(new DateTime(DateTime.Now.Year, 1, 1)) + Environment.NewLine;

            MessageBox.Show(msg);
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

        public Double ReadProfit(DateTime dateTime)
        {
            Double profit = 0;

            try
            {
                Double rate = 0;

                String sql = "select Rate from EmployeeRate where EmployeeId = " + employee.Id + " and StartDate <= '" + DateTime.Now + "'";
                SQLiteCommand sQLiteCommand = new SQLiteCommand(sql, dbConnection);
                SQLiteDataReader sQLiteDataReader = sQLiteCommand.ExecuteReader();
                sQLiteDataReader.Read();
                rate = sQLiteDataReader.GetDouble(0);

                sql = "select * from Tasks";
                adapter = new SQLiteDataAdapter(sql, dbConnection);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet);
                DataTable dtt = dataSet.Tables[0];
                List<Task> tasks = dtt.DataTableToList<Task>();

                sql = "select * from TaskHours where EmployeeId = " + employee.Id + " and StartDate >= '" + Utility.SetDateTime(dateTime) + "'";
                adapter = new SQLiteDataAdapter(sql, dbConnection);
                dataSet = new DataSet();
                adapter.Fill(dataSet);
                dtt = dataSet.Tables[0];
                List<TaskHour> taskHours = dtt.DataTableToList<TaskHour>();

                foreach (TaskHour taskHour in taskHours)
                {
                    double totalMinutes = (taskHour.EndDate - taskHour.StartDate).TotalMinutes;

                    Task task = tasks.Find(a => a.Id == taskHour.TaskId);

                    profit += totalMinutes * (task.HourlyRate - rate) / 60;
                }
            }
            catch (Exception ex)
            {
                string s = ex.Message;
            }

            return profit;
        }
    }
}
