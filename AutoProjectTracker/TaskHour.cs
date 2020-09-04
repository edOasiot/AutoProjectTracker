using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoProjectTracker
{
    public class TaskHour
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int TaskId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public TaskHour()
        {
        }
    }
}
