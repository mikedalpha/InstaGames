﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace GroupProject.RepositoryService.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext Context;

        public Repository(DbContext context)
        {
            Context = context;
        }

        public TEntity FindById(int? id)
        {
            return Context.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> Get()
        {
            return Context.Set<TEntity>().ToList();
        }

     

        //public TEntity FindRandomGame()
        //{
        //    var x = new Random().Next(1, 6);
        //    return Context.Set<TEntity>().Find(x);
        //}
    }
}