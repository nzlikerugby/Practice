using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Repository
{
    public interface IRepository<T>
    {
        T Get(Guid Id);
        List<T> GetAll();
        void Remove(T entity);
        void Add(T entity);
        void Update(T entity);
        T GetFromFK(Guid Id,string FKName);
        List<T> Filter(string keyword);
    }
}
