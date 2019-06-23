using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace LinqToSqlParser
{
    public interface IParseLinqToSql
    {
        string Parse<TIn>(Expression<Func<IEnumerable<TIn>>> expression)
            where TIn : class;
    }
}
