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

            msg += "Profit = $" + ReadProfit(startDateTimePicker.Value, endDateTimePicker.Value) + Environment.NewLine;

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

        public Double ReadProfit(DateTime StartDate, DateTime EndDate)
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

                sql = "select * from TaskHours where EmployeeId = " + employee.Id + " and StartDate >= '" + Utility.SetDateTime(StartDate) + "' and EndDate <= '" + Utility.SetDateTime(EndDate) + "'";
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


        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;

            if (!rb.Checked)
                return;

            switch (rb.Name)
            {
                case "dayRadioButton":
                    startDateTimePicker.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                    break;
                case "weekRadioButton":
                    startDateTimePicker.Value = Utility.StartOfWeek(DayOfWeek.Sunday);
                    break;
                case "monthRadioButton":
                    startDateTimePicker.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                    break;
                case "yearRadioButton":
                    startDateTimePicker.Value = new DateTime(DateTime.Now.Year, 1,1);
                    break;
            }
        }

        private void employeeRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            string sql = "select * from Employees";
            adapter = new SQLiteDataAdapter(sql, dbConnection);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);
            DataTable dtt = dataSet.Tables[0];
            List<Employee> employees = dtt.DataTableToList<Employee>();

            foreach (Employee employee in employees)
            {
                taskEmployeeComboBox.Items.Add(new { Text = employee.FirstName + " " + employee.LastName, Value = employee.Id });
            }

            taskEmployeeComboBox.DisplayMember = "Text";
            taskEmployeeComboBox.ValueMember = "Value";


            //object o = (taskEmployeeComboBox.SelectedItem as dynamic).Value;
        }

        private void taskRadioButton_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
