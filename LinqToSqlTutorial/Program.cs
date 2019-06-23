using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LinqToSqlParser;
using LinqToSqlTutorial.SampleDto;

namespace LinqToSqlTutorial
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var input = new List<Foo>();
            Expression<Func<IEnumerable<string>>> query = () =>
                input.Where(p => p.MyIntProperty == 5).Select(p => p.MyStringProperty);

            var linqParser = ParserFactory.Create();
            var result = linqParser.Parse(query);
            Console.WriteLine(result);

            var collection = new List<Foo>();
            Expression<Func<IEnumerable<string>>> newExp = () => collection.Where(p => p.MyIntProperty == 5).Select(p => p.MyStringProperty);
        }
    }
}
