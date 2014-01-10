using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using TaskManager.Helpers;
using TaskManager.Infrastructure;
using TaskManager.Models;

namespace TaskManager.api
{
    [Authorize]
    public class TasksController : BaseApiController
    {
        private UserInfoHelper ui = new UserInfoHelper();

        // GET api/Task
        public IEnumerable<TaskDetails> GetTasks(int cat, int status)
        {
            DateTime _today = DateTime.Now;
            DateTime _soon = _today.AddDays(2);
            int _userId = ui.GetUserId();

            var tasks = (from t in db.Tasks
                         where t.UserId == _userId
                         & (cat != 0 && t.CategoryId == cat || cat == 0)
                         & (status != 0 && t.StatusId == status || status == 0)
                         select new TaskDetails
                         {
                             TaskId = t.TaskId,
                             Name = t.Name,
                             Body = t.Body,
                             UserName = t.UserProfile.UserName,
                             UserId = t.UserId,
                             DateAdded = t.DateAdded,
                             DateUpdated = t.DateUpdated,
                             DueDate = t.DueDate,
                             StatusId = t.StatusId,
                             Status = t.ref_Status.Status,
                             PriorityId = t.PriorityId,
                             Priority = t.ref_Priorities.Priority,
                             Category = t.Category.CategoryName,
                             CategoryId = t.CategoryId,
                             StatusClass = (t.DueDate <= _today && t.StatusId < 3) ? "DueNow" : (t.DueDate <= _soon && t.StatusId < 3) ? "DueSoon" : "",

                         }
                             );

            return tasks.AsEnumerable().OrderBy(x => x.StatusId).ThenByDescending(x => x.DueDate <= _today).ThenBy(x => x.DueDate);
        }

        // GET api/Tasks/5
        public TaskDetails GetTask(int id)
        {

            var TaskDetail = (from t in db.Tasks
                              where t.TaskId == id
                              select new TaskDetails
                              {
                                  TaskId = t.TaskId,
                                  Name = t.Name,
                                  Body = t.Body,
                                  UserName = t.UserProfile.UserName,
                                  UserId = t.UserId,
                                  DateAdded = t.DateAdded,
                                  DateUpdated = t.DateUpdated,
                                  DueDate = t.DueDate,
                                  StatusId = t.StatusId,
                                  Status = t.ref_Status.Status,
                                  PriorityId = t.PriorityId,
                                  Priority = t.ref_Priorities.Priority,
                                  Category = t.Category.CategoryName,
                                  CategoryId = t.CategoryId,
                              }
                            ).FirstOrDefault();

            if (TaskDetail == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return TaskDetail;
        }

        // PUT api/Tasks/5
        public HttpResponseMessage PutTask(int id, Task task)
        {
            if (ModelState.IsValid && id == task.TaskId)
            {
                var taskEdit = (from t in db.Tasks
                                where t.TaskId == task.TaskId
                                select t).FirstOrDefault();


                if (taskEdit == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
                else
                {
                    taskEdit.DateUpdated = DateTime.Now;
                    taskEdit.Name = task.Name;
                    taskEdit.Body = task.Body;
                    taskEdit.DueDate = task.DueDate;
                    taskEdit.PriorityId = task.PriorityId;
                    taskEdit.StatusId = task.StatusId;
                    taskEdit.CategoryId = task.CategoryId;

                    try
                    {
                        db.SaveChanges();
                        AddComment(id, task.StatusId, "Task Updated");
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound);
                    }
                }

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // POST api/Tasks
        public HttpResponseMessage PostTask(Task task)
        {
            if (ModelState.IsValid)
            {
                task.DateAdded = DateTime.Now;
                task.DateUpdated = DateTime.Now;
                if (task.StatusId == 0)
                    task.StatusId = 1;

                if (task.PriorityId == 0)
                    task.PriorityId = 1;

                task.UserId = ui.GetUserId();

                db.Tasks.Add(task);
                db.SaveChanges();

                AddComment(task.TaskId, 1, "Task Created");
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, task);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = task.TaskId }));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/Tasks/5
        public HttpResponseMessage DeleteTask(int id)
        {
            Task task = db.Tasks.Find(id);
            if (task == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);
    
            db.Tasks.Remove(task);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, task);
        }

        /// <summary>
        /// Add new history comment
        /// </summary>
        /// <param name="_taskId"></param>
        /// <param name="_statusId"></param>
        /// <param name="_comment"></param>
        private void AddComment(int _taskId, int _statusId, string _comment)
        {
            int _userId = ui.GetUserId();
            Comment newComment = new Comment();
            newComment.DateAdded = DateTime.Now;
            newComment.StatusId = _statusId;
            newComment.TaskId = _taskId;
            newComment.Body = _comment;
            db.Comments.Add(newComment);
            db.SaveChanges();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}