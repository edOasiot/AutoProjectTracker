using System;

namespace AutoProjectTracker
{
    public partial class EmployeeForm : MyForm
    {
        //private static SQLiteConnection dbConnection;
        //private Dictionary<string, string> key = new Dictionary<string, string>();
        public EmployeeForm(string Username)
        {
            InitializeComponent();

            TableName = Utility.settings.EmployeeTable;
            KeyColumn = "Username";
            KeyColumnValue = Username;
            TableType = "Employee";

            ReadRecord();
        }

        private void CancelB_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SaveB_Click(object sender, EventArgs e)
        {
            UpdateRecord();
        }
    }
}
