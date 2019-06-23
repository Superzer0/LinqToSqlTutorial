using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace LinqToSqlParser.SqlVisitor
{
    internal class LinqMsSqlParser : IParseLinqToSql
    {
        public string Parse<TIn>(Expression<Func<IEnumerable<TIn>>> expression) where TIn : class 
        {
            throw new NotImplementedException();

            // use ExpressionVisitor from .net 
            // basic version: select where and orderby flat. 
            // you need to go by call functions and decompose each of them. suggest creating ExpressionVisitor for each of them. 
            // we will be only interested in lambda from original input. 
        }
    }
}
