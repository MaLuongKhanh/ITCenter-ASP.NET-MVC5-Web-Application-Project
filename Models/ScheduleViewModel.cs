using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class ScheduleViewModel
    {
        public List<ScheduleDetail> ScheduleDetails { get; set; }
        public List<Schedule> Schedules { get; set; }
    }
}