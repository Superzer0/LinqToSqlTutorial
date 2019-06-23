using LinqToSqlParser.DummyVisitor;

namespace LinqToSqlParser
{
    /// <summary>
    /// Creates default instance for SQL parser
    /// </summary>
    public static class ParserFactory
    {
        public static IParseLinqToSql Create()
        {
            return new DummySqlParser();
        }
    }
}
