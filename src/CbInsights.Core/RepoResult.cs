using System;
using System.Collections.Generic;
using System.Text;

namespace CbInsights.Core
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
        Success,
        Failed,
        NotFound
    }
}
