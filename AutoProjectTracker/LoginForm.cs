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
    public partial class LoginForm : MyForm
    {
        public LoginForm()
        {
            InitializeComponent();

            Username.Text = "austin";

            TableName = Utility.settings.EmployeeTable;
            KeyColumn = "Username";
            TableType = "Employee";

            SetDBConnection();
        }

        private void LoginB_Click(object sender, EventArgs e)
        {
            KeyColumnValue = Username.Text;
            Employee employee = (Employee)ReadRecord();

            if (employee == null)
            {
                MessageBox.Show("Employee Not Found");
                return;
            }

            if (employee.Role.Equals(Convert.ToInt32(Enums.Roles.Worker)))
            {
                ProjectListForm projectListForm = new ProjectListForm(employee);
                projectListForm.Show();
            }
            else
            {
                ProjectListForm projectListForm = new ProjectListForm(employee);
                projectListForm.Show();

                //EmployeeForm employeeForm = new EmployeeForm(Username.Text);
                //employeeForm.Show();
            }
        }
    }
}
