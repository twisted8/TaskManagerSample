using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManager.Repository
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        T GetFirst(int id);
        void Create(T record);
        void Delete(int id);
        void Update(T record);
        void Save();
    }
}