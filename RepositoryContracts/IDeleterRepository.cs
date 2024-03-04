﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryContracts
{
    public interface IDeleterRepository<T> where T : class
    {
        public Task<bool> DeleteAsync(object entityId);
    }
}
