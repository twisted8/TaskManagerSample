using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using TaskManager.Helpers;
using TaskManager.Infrastructure;

namespace TaskManager.Repository
{
    public class TaskRepository : IRepository<Task>
    {
        private EntitiesDb context;

        public TaskRepository(EntitiesDb context)
        {
            this.context = context;
        }


        public void Create(Task task)
        {
            context.Tasks.Add(task);
            Save();
        }


        public void Delete(int id)
        {
            Task record = GetFirst(id);
            if (record != null)
            {
                context.Tasks.Remove(record);
                Save();
            }
        }

        public void Update(Task fullprofle)
        {
            context.Entry(fullprofle).State = EntityState.Modified;
        }


        public Task GetFirst(int id)
        {
            return context.Tasks.FirstOrDefault(c => c.TaskId == id);
        }

        public IEnumerable<Task> GetAll()
        {
            UserInfoHelper ui = new UserInfoHelper();
            return context.Tasks.ToList().Where(x => x.UserId == ui.GetUserId());
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}