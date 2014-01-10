using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TaskManager.Helpers;
using TaskManager.Infrastructure;

namespace TaskManager.api
{
    public class TaskReassignController : BaseApiController
    {

        // PUT api/taskreassign/5
        public void Put(int id, ReassignTask reassignTask)
        {
            var Task = db.Tasks.Find(id);
            {
                if (Task != null)
                {
                    Task.UserId = reassignTask.ToUserId;
                    db.SaveChanges();
                }
            }
        }

    }

    public class ReassignTask
    {
        public int ToUserId { get; set; }
    }
}
