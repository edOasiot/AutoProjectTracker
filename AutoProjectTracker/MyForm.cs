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
    public partial class MyForm : Form
    {
        public string TableName;
        public string KeyColumn;
        public string KeyColumnValue;
        public string TableType;
        public SQLiteConnection dbConnection;
        public SQLiteDataAdapter adapter;
        public MyForm()
        {
            InitializeComponent();

            SetDBConnection();
        }

        public void SetDBConnection()
        {
            if (dbConnection == null)
                dbConnection = new SQLiteConnection("Data Source=" + Utility.settings.AutoProjectTrackerDB + ";Version=3;");

            if (!dbConnection.State.Equals(ConnectionState.Open))
                dbConnection.Open();
        }

        public void ReadRecord()
        {
            String sql = "select * from " + TableName;
            sql += " Where " + KeyColumn + " = '" + KeyColumnValue + "'";

            adapter = new SQLiteDataAdapter(sql, dbConnection);
            DataSet dataSet = new DataSet();
            List<object> records = null;

            try
            {
                adapter.Fill(dataSet);
                DataTable dtt = dataSet.Tables[0];
                records = dtt.DataTableToList("AutoProjectTracker." + TableType);
            }
            catch { }

            if (records.Count.Equals(1))
                SetTextBoxValues(records[0]);
        }

        public void UpdateRecord()
        {
            try
            {
                String sql = "update " + TableName + " Set ";

                foreach (KeyValuePair<string, string> entry in SaveTextBoxValues())
                    sql += entry.Key + " = '" + entry.Value + "', ";

                sql = sql.Substring(0, sql.Length - 2);
                sql += " Where " + KeyColumn + " = '" + KeyColumnValue + "'";
                SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                //Utility.WriteToLog(LogFileName, MethodBase.GetCurrentMethod().ReflectedType.FullName, ex);
            }
        }

        public void SetTextBoxValues(object dBclass)
        {
            foreach (Control control in this.Controls)
                if (control.GetType().Equals(typeof(TextBox)))
                    Utility.SetPropValue(control, "Text", Utility.GetPropValue(dBclass, control.Name).ToString());
        }

        public Dictionary<string, string> SaveTextBoxValues()
        {
            Dictionary<string, string> values = new Dictionary<string, string>();

            foreach (Control control in this.Controls)
                if (control.GetType().Equals(typeof(TextBox)) && control.Name != KeyColumn)
                    values.Add(control.Name, Utility.GetPropValue(control, "Text").ToString());

            return values;
        }

    }
}
