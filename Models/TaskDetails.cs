using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaskManager.Infrastructure;

namespace TaskManager.Models
{
    public class Tasks
    {
        public IEnumerable<ref_Status> Status { get; set; }
        public IEnumerable<ref_Priorities> Priorities { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<UserProfileM> Profiles { get; set; }
    }
}