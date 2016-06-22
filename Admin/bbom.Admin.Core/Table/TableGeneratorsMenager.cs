using System;
using System.Collections.Generic;
using bbom.Admin.Core.Table.Profiles;
using bbom.Data.ContentModel;

namespace bbom.Admin.Core.Table
{
    public static class TableGeneratorsMenager
    {
        public static ITableGenerator<T> GetGenerator<T>()
        {
            Dictionary<Type, object> generators = new Dictionary<Type, object>
            {
                {typeof(Post), new PostTableGeneratorProfile()}
            };
            return (ITableGenerator<T>) generators[typeof(T)];
        }
    }
}