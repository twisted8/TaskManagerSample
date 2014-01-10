using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManager.Models
{
    public class TaskStats
    {
        public int TotalTasks { get; set; }
        public int PastDueTasks { get; set; }
        public int CompletedTasks { get; set; }
        public int PendingTask { get; set; }

    }
}