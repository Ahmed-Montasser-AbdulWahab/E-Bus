﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryContracts
{
    public interface IGetterRepository<T> where T : class
    {
        public Task<List<T>?> GetAllAsync(bool include = false);

        public Task<T?> GetByIdAsync(object id, bool include = false);
    }
}
