namespace AutoProjectTracker
{
    public class Settings
    {
        private string _ServiceName;
        private string _LogFileName;
        private string _Email;
        private string _Key;

        private string _AutoProjectTrackerDB; 
        private string _MarketWatchTable; 
        private string _CompanyWatchTable;
        private string _EmployeeTable;

        public string ServiceName { get => _ServiceName; set => _ServiceName = value; }
        public string LogFileName { get => _LogFileName; set => _LogFileName = value; }
        public string Email { get => _Email; set => _Email = value; }
        public string Key { get => _Key; set => _Key = value; }
        public string AutoProjectTrackerDB { get => _AutoProjectTrackerDB; set => _AutoProjectTrackerDB = value; }
        public string MarketWatchTable { get => _MarketWatchTable; set => _MarketWatchTable = value; }
        public string CompanyWatchTable { get => _CompanyWatchTable; set => _CompanyWatchTable = value; }
        public string EmployeeTable { get => _EmployeeTable; set => _EmployeeTable = value; }
    }
}
