using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoProjectTracker
{
    public class Task
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string TaskName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int HourlyRate { get; set; }
        public decimal CurrentHours { get; set; }
        public Task()
        {
        }
    }
}
