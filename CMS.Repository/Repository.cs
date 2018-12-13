using CMS.Data;
using CMS.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMS.Repository
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly CMSDbContext context;
        private DbSet<T> entities;
        public Repository(CMSDbContext context)
        {
            this.context = context;
            entities = this.context.Set<T>();
        }

        public void Add(T entity)
        {
            context.Add(entity);
            context.SaveChanges();
        }

        public T Get(Guid Id)
        {
            var entity = entities.AsNoTracking().FirstOrDefault(p=>p.Id == Id);
            if(entity == null)
            {
                throw new NullReferenceException("ENTITY DOESN'T EXIST");
            }
            return entity;
        }

        public List<T> GetAll()
        {
            return entities.ToList();
        }

        public T GetFromFK(Guid Id,string FKName)
        {
            return entities.AsNoTracking().FirstOrDefault(p => (Guid)(p.GetType().GetProperty(FKName).GetValue(p)) == Id);
        }

        public void Remove(T entity)
        {
            if (entity == null)
            {
                throw new NullReferenceException("ENTITY DOESN'T EXIST");
            }
            entities.Remove(entity);
            context.SaveChanges();
        }

        public void Update(T entity)
        {
            entities.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }

        public List<T> Filter(string keyword)
        {
            var type = typeof(T);
            var properties = type.GetProperties();
            List<T> results = new List<T>();
            foreach(var item in entities)
            {
                foreach(var p in properties)
                {
                    if (p.GetValue(item)!=null && p.GetValue(item).ToString().ToLower().Contains(keyword.ToLower()))
                    {
                        results.Add(item);
                        break;
                    }
                }
            }
            return results;            
        }
    }
}
