using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaskManager.Infrastructure;

namespace TaskManager.Models
{
    public class TaskDetails
    {
        public int TaskId { get; set; }
        public string Name { get; set; }
        public string Body { get; set; }
        public int StatusId { get; set; }
        public string Status { get; set; }
        public string StatusClass { get; set; }
        public int PriorityId { get; set; }
        public string Priority { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int CategoryId { get; set; }
        public string Category { get; set; }
        public System.DateTime DateAdded { get; set; }
        public System.DateTime DateUpdated { get; set; }
        public System.DateTime? DueDate { get; set; }
        
    }
}