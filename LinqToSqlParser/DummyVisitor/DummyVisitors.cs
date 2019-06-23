using System;
using System.Linq.Expressions;

namespace LinqToSqlParser.DummyVisitor
{
    // Base Visitor class:
    public abstract class Visitor
    {
        private readonly Expression node;

        protected Visitor(Expression node)
        {
            this.node = node;
        }

        public abstract void Visit(string prefix);

        public ExpressionType NodeType => this.node.NodeType;
        public static Visitor CreateFromExpression(Expression node)
        {
            switch (node.NodeType)
            {
                case ExpressionType.Constant:
                    return new ConstantVisitor((ConstantExpression)node);
                case ExpressionType.Lambda:
                    return new LambdaVisitor((LambdaExpression)node);
                case ExpressionType.Parameter:
                    return new ParameterVisitor((ParameterExpression)node);
                case ExpressionType.Add:
                    return new BinaryVisitor((BinaryExpression)node);
                default:
                    Console.Error.WriteLine($"Node not processed yet: {node.NodeType}");
                    return default(Visitor);
            }
        }
    }

    // Lambda Visitor
    public class LambdaVisitor : Visitor
    {
        private readonly LambdaExpression node;
        public LambdaVisitor(LambdaExpression node) : base(node)
        {
            this.node = node;
        }

        public override void Visit(string prefix)
        {
            Console.WriteLine($"{prefix}This expression is a {NodeType} expression type");
            Console.WriteLine($"{prefix}The name of the lambda is {((node.Name == null) ? "<null>" : node.Name)}");
            Console.WriteLine($"{prefix}The return type is {node.ReturnType.ToString()}");
            Console.WriteLine($"{prefix}The expression has {node.Parameters.Count} argument(s). They are:");
            // Visit each parameter:
            foreach (var argumentExpression in node.Parameters)
            {
                var argumentVisitor = Visitor.CreateFromExpression(argumentExpression);
                argumentVisitor.Visit(prefix + "\t");
            }
            Console.WriteLine($"{prefix}The expression body is:");
            // Visit the body:
            var bodyVisitor = Visitor.CreateFromExpression(node.Body);
            bodyVisitor.Visit(prefix + "\t");
        }
    }

    // Binary Expression Visitor:
    public class BinaryVisitor : Visitor
    {
        private readonly BinaryExpression node;
        public BinaryVisitor(BinaryExpression node) : base(node)
        {
            this.node = node;
        }

        public override void Visit(string prefix)
        {
            Console.WriteLine($"{prefix}This binary expression is a {NodeType} expression");
            var left = Visitor.CreateFromExpression(node.Left);
            Console.WriteLine($"{prefix}The Left argument is:");
            left.Visit(prefix + "\t");
            var right = Visitor.CreateFromExpression(node.Right);
            Console.WriteLine($"{prefix}The Right argument is:");
            right.Visit(prefix + "\t");
        }
    }

    // Parameter visitor:
    public class ParameterVisitor : Visitor
    {
        private readonly ParameterExpression node;
        public ParameterVisitor(ParameterExpression node) : base(node)
        {
            this.node = node;
        }

        public override void Visit(string prefix)
        {
            Console.WriteLine($"{prefix}This is an {NodeType} expression type");
            Console.WriteLine($"{prefix}Type: {node.Type.ToString()}, Name: {node.Name}, ByRef: {node.IsByRef}");
        }
    }

    // Constant visitor:
    public class ConstantVisitor : Visitor
    {
        private readonly ConstantExpression node;
        public ConstantVisitor(ConstantExpression node) : base(node)
        {
            this.node = node;
        }

        public override void Visit(string prefix)
        {
            Console.WriteLine($"{prefix}This is an {NodeType} expression type");
            Console.WriteLine($"{prefix}The type of the constant value is {node.Type}");
            Console.WriteLine($"{prefix}The value of the constant value is {node.Value}");
        }
    }
}
