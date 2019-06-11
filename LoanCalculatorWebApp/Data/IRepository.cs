﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoanCalculatorWebApp.Data
{
    public interface IRepository<T> where T : IEntity
    {
        IEnumerable<T> List { get; }
        Task<IEnumerable<T>> GetAll();
        Task Add(T entity);
        Task Delete(T entity);
        Task Update(T entity);
        Task<T> FindById(Guid? id);
    }
}
