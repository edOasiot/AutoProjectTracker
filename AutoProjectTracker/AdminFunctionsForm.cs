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
        List<Employee> employees = null;
        List<Task> tasks = null;
        List<EmployeeRate> employeeRates = null;
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

            dayRadioButton.Checked = true;
            allTasksRadioButton.Checked = true;

            taskEmployeeComboBox.DisplayMember = "Text";
            taskEmployeeComboBox.ValueMember = "Value";
            
            ReadTables();
        }

        private void ShowProfitsB_Click(object sender, EventArgs e)
        {
            string msg = "";

            if (allTasksRadioButton.Checked)
                msg += "Profit = $" + ReadProfit(startDateTimePicker.Value, endDateTimePicker.Value) + Environment.NewLine;
            else if (taskRadioButton.Checked && taskEmployeeComboBox.SelectedItem != null)
                msg += "Profit = $" + ReadProfit(startDateTimePicker.Value, endDateTimePicker.Value, "task", Convert.ToInt32((taskEmployeeComboBox.SelectedItem as dynamic).Value)) + Environment.NewLine;
            else if (employeeRadioButton.Checked && taskEmployeeComboBox.SelectedItem != null)
                msg += "Profit = $" + ReadProfit(startDateTimePicker.Value, endDateTimePicker.Value, "employee", Convert.ToInt32((taskEmployeeComboBox.SelectedItem as dynamic).Value)) + Environment.NewLine;

            MessageBox.Show(msg);
        }

        private void WorkerB_Click(object sender, EventArgs e)
        {
            ProjectListForm projectListForm = new ProjectListForm(employee);
            projectListForm.Show();
        }

        public Double ReadProfit(DateTime StartDate, DateTime EndDate, string type = "", Int32 id = 0)
        {
            Double profit = 0;

            try
            {
                string sql = "select * from TaskHours where StartDate >= '" + Utility.SetDateTime(StartDate) + "' and EndDate <= '" + Utility.SetDateTime(EndDate) + "'";

                if (type.Equals("task"))
                    sql += " and TaskId = " + id;
                else if (type.Equals("employee"))
                    sql += " and EmployeeId = " + id;

                adapter = new SQLiteDataAdapter(sql, dbConnection);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet);
                DataTable dtt = dataSet.Tables[0];
                List<TaskHour> taskHours = dtt.DataTableToList<TaskHour>();

                foreach (TaskHour taskHour in taskHours)
                {
                    double totalMinutes = (taskHour.EndDate - taskHour.StartDate).TotalMinutes;

                    EmployeeRate employeeRate = employeeRates.First(a => a.EmployeeId == taskHour.EmployeeId && a.StartDate <= taskHour.StartDate);
                    Task task = tasks.Find(a => a.Id == taskHour.TaskId);

                    profit += totalMinutes * (task.HourlyRate - employeeRate.Rate) / 60;
                }
            }
            catch (Exception ex)
            {
                string s = ex.Message;
            }

            return Math.Round(profit, 2);
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
            taskEmployeeComboBox.Text = "";
            taskEmployeeComboBox.Items.Clear();

            foreach (Employee employee in employees)
                taskEmployeeComboBox.Items.Add(new { Text = employee.FirstName + " " + employee.LastName, Value = employee.Id });
        }

        private void taskRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            taskEmployeeComboBox.Text = "";
            taskEmployeeComboBox.Items.Clear();

            foreach (Task task in tasks)
                taskEmployeeComboBox.Items.Add(new { Text = task.TaskName, Value = task.Id });
        }

        private void allTasksRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            taskEmployeeComboBox.Text = "";
            taskEmployeeComboBox.Items.Clear();
        }

        private void ReadTables()
        {
            string sql = "select * from Employees";
            adapter = new SQLiteDataAdapter(sql, dbConnection);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);
            DataTable dtt = dataSet.Tables[0];
            employees = dtt.DataTableToList<Employee>();

            sql = "select * from Tasks";
            adapter = new SQLiteDataAdapter(sql, dbConnection);
            dataSet = new DataSet();
            adapter.Fill(dataSet);
            dtt = dataSet.Tables[0];
            tasks = dtt.DataTableToList<Task>();

            sql = "select * from EmployeeRate Order By StartDate Desc";
            adapter = new SQLiteDataAdapter(sql, dbConnection);
            dataSet = new DataSet();
            adapter.Fill(dataSet);
            dtt = dataSet.Tables[0];
            employeeRates = dtt.DataTableToList<EmployeeRate>();
        }

    }
}
