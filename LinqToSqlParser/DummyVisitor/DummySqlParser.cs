using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace LinqToSqlParser.DummyVisitor
{
    internal class DummySqlParser : IParseLinqToSql
    {
        public string Parse<TIn>(Expression<Func<IEnumerable<TIn>>> expression) where TIn : class
        {
            var fromExpression = Visitor.CreateFromExpression(expression);
            fromExpression.Visit("");
            return string.Empty;
        }
    }
}
