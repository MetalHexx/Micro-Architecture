using System;
using System.Collections.Generic;
using System.Text;

namespace ProductsApi.Repository
{
    public class RepoResult<T>
    {
        public RepoResultType Type { get; set; }
        public T Entity { get; set; }

        public RepoResult(T entity)
        {
            Entity = entity;
        }

    }

    public enum RepoResultType
    {
        Invalid,
        Success,
        Failed,
        NotFound
    }
}
