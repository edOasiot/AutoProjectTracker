using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace AutoProjectTracker
{
    public partial class MyForm : Form
    {
        public string TableName;
        public string KeyColumn;
        public string KeyColumnValue;
        public string TableType;
        public string ListColumnsNotShown;
        public SQLiteConnection dbConnection;
        public SQLiteDataAdapter adapter;
        public MyForm()
        {
            InitializeComponent();
        }

        public void SetDBConnection()
        {
            if (dbConnection == null)
                dbConnection = new SQLiteConnection("Data Source=" + Utility.settings.AutoProjectTrackerDB + ";Version=3;");

            if (!dbConnection.State.Equals(ConnectionState.Open))
                dbConnection.Open();
        }

        public object ReadRecord()
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

            if (records != null && records.Count.Equals(1))
            {
                SetTextBoxValues(records[0]);
                return records[0];
            }
            else
                return null;
        }

        public List<T> ReadRecords<T>() where T : class, new()
        {
            String sql = "select ";
            Utility.GetListTableFields(typeof(T), ref sql, ListColumnsNotShown);
            sql += " from " + TableName;

            if (KeyColumnValue != null)
            {
                sql += " Where " ;

                int index = 0;
                foreach (string keyColumn in  KeyColumn.Split(','))
                {
                    sql += keyColumn + " = '" + KeyColumnValue.Split(',')[index] + "' And ";
                    index++;
                }

                sql = sql.Substring(0, sql.Length - 5);
            }

            adapter = new SQLiteDataAdapter(sql, dbConnection);
            DataSet dataSet = new DataSet();

            try
            {
                adapter.Fill(dataSet);
                DataTable dtt = dataSet.Tables[0];
                return dtt.DataTableToList<T>();
            }
            catch { }

            return null;
        }

        public void UpdateRecord()
        {
            try
            {
                String sql = "update " + TableName + " Set ";

                foreach (KeyValuePair<string, string> entry in SaveTextBoxValues())
                    sql += entry.Key + " = '" + entry.Value + "', ";

                sql = sql.Substring(0, sql.Length - 2);

                if (KeyColumnValue != null)
                {
                    sql += " Where ";

                    int index = 0;
                    foreach (string keyColumn in KeyColumn.Split(','))
                    {
                        sql += keyColumn + " = '" + KeyColumnValue.Split(',')[index] + "' And ";
                        index++;
                    }

                    sql = sql.Substring(0, sql.Length - 5);
                }

                SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                //Utility.WriteToLog(LogFileName, MethodBase.GetCurrentMethod().ReflectedType.FullName, ex);
            }
        }

        public void InsertRecord()
        {
            try
            {
                String sql = "Insert Into " + TableName + " (<columns>) Values (<values>)";

                string columns = "";
                string values = "";

                foreach (KeyValuePair<string, string> entry in SaveTextBoxValues())
                {
                    columns += entry.Key + ", ";
                    values += "'" + entry.Value + "', ";
                }

                columns += KeyColumn;
                values += KeyColumnValue;

                sql = sql.Replace("<columns>", columns);
                sql = sql.Replace("<values>", values);

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
                {
                    object o = Utility.GetPropValue(dBclass, control.Name);

                    if (o != null)
                        Utility.SetPropValue(control, "Text", o.ToString());
                }
        }

        public Dictionary<string, string> SaveTextBoxValues()
        {
            Dictionary<string, string> values = new Dictionary<string, string>();

            foreach (Control control in this.Controls)
                if (control.GetType().Equals(typeof(TextBox)) && control.Name != KeyColumn)
                    values.Add(control.Name, Utility.GetPropValue(control, "Text").ToString());
                else if (control.GetType().Equals(typeof(DateTimePicker)))
                    values.Add(control.Name, Utility.SetDateTime(DateTime.Now.ToShortDateString() + " " + Utility.GetPropValue(control, "Text").ToString()));

            return values;
        }

        public decimal CalculateTaskHours(Int32 taskId)
        {
            decimal hours = 0;

            try
            {
                String sql = "select Sum(Cast ((JulianDay(EndDate) - JulianDay(StartDate)) *24 * 60 As Integer)) from " 
                    + Utility.settings.TaskHourTable + " Where TaskId = " + taskId;

                SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
                hours = Math.Round(Convert.ToDecimal(Convert.ToInt32(command.ExecuteScalar()) / 60.00), 2);
            }
            catch { }

            return hours;
        }
    }
}
