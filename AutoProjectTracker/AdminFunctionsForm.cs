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
        List<EmployeeRate> employeeRates = null;
        List<Project> projects = null;
        List<Task> tasks = null;
        public AdminFunctionsForm(Employee _employee)
        {
            InitializeComponent();

            employee = _employee;

            StartPosition = FormStartPosition.Manual;
            Location = new Point(55, 55);

            dayRadioButton.Checked = true;

            ReadTables();
        }

        private void ShowProfitsB_Click(object sender, EventArgs e)
        {
            Int32 employeeId = 0;
            Int32 projectId = 0;

            if (projectComboBox.SelectedItem != null && !String.IsNullOrEmpty(projectComboBox.Text))
                projectId = Convert.ToInt32((projectComboBox.SelectedItem as dynamic).Value);

            if (employeeComboBox.SelectedItem != null && !String.IsNullOrEmpty(employeeComboBox.Text))
                employeeId = Convert.ToInt32((employeeComboBox.SelectedItem as dynamic).Value);

            profitLabel.Text = "$" + CalculateProfit(startDateTimePicker.Value, endDateTimePicker.Value, employeeId, projectId) + Environment.NewLine;
        }

        private void WorkerB_Click(object sender, EventArgs e)
        {
            ProjectListForm projectListForm = new ProjectListForm(employee);
            projectListForm.Show();
        }

        public Double CalculateProfit(DateTime StartDate, DateTime EndDate, Int32 employeeId, Int32 projectId)
        {
            Double profit = 0;
            List<Task> projectTasks = null;

            try
            {
                Project project = projects.Find(a => a.Id == projectId);
                
                if (project != null)
                    projectTasks = tasks.FindAll(a => a.ProjectId == project.Id);

                string sql = "select * from TaskHours where StartDate >= '" + Utility.SetDateTime(StartDate) + "' and EndDate <= '" + Utility.SetDateTime(EndDate) + "'";

                if (projectTasks != null)
                {
                    string ids = "";

                    foreach (Task task in projectTasks)
                        ids += task.Id + ",";

                    sql += " and TaskId in (" + ids.Substring(0, ids.Length - 1) + ")";
                }

                if (employeeId != 0)
                    sql += " and EmployeeId = " + employeeId;

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

        private void ReadTables()
        {
            string sql = "select * from Employees";
            adapter = new SQLiteDataAdapter(sql, dbConnection);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);
            DataTable dtt = dataSet.Tables[0];
            employees = dtt.DataTableToList<Employee>();

            sql = "select * from EmployeeRate Order By StartDate Desc";
            adapter = new SQLiteDataAdapter(sql, dbConnection);
            dataSet = new DataSet();
            adapter.Fill(dataSet);
            dtt = dataSet.Tables[0];
            employeeRates = dtt.DataTableToList<EmployeeRate>();

            sql = "select * from Projects";
            adapter = new SQLiteDataAdapter(sql, dbConnection);
            dataSet = new DataSet();
            adapter.Fill(dataSet);
            dtt = dataSet.Tables[0];
            projects = dtt.DataTableToList<Project>();

            sql = "select * from Tasks";
            adapter = new SQLiteDataAdapter(sql, dbConnection);
            dataSet = new DataSet();
            adapter.Fill(dataSet);
            dtt = dataSet.Tables[0];
            tasks = dtt.DataTableToList<Task>();

            employeeComboBox.Items.Add(new { Text = "", Value = "" });
            foreach (Employee employee in employees)
                employeeComboBox.Items.Add(new { Text = employee.FirstName + " " + employee.LastName, Value = employee.Id });

            projectComboBox.Items.Add(new { Text = "", Value = "" });
            foreach (Project project in projects)
                projectComboBox.Items.Add(new { Text = project.ProjectName, Value = project.Id });
        }

    }
}
