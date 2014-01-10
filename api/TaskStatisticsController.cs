using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TaskManager.Helpers;
using TaskManager.Infrastructure;
using TaskManager.Models;

namespace TaskManager.api
{
    public class TaskStatisticsController : BaseApiController
    {

        // GET api/taskstatistics
        public TaskStats Get()
        {
            TaskStats stats = new TaskStats();
            UserInfoHelper ui = new UserInfoHelper();

            int _userId = ui.GetUserId();
            DateTime _today = DateTime.Now;
            var task = (from t in db.Tasks
                        where t.UserId == _userId
                        select t).ToList();

            stats.TotalTasks = task.Count;
            stats.PendingTask = task.Where(x => x.StatusId < 3).Count();
            stats.PastDueTasks = task.Where(x => x.DueDate < _today).Count();
            stats.CompletedTasks = task.Where(x => x.StatusId == 3).Count();

            return stats;
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
