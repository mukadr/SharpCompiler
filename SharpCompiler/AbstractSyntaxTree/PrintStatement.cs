namespace SharpCompiler.AbstractSyntaxTree
{
    public class PrintStatement : Statement
    {
        public Expression Expression { get; }

        public PrintStatement(Expression expression)
        {
            Expression = expression;
        }

        public override void Accept(INodeVisitor visitor)
        {
            visitor.VisitPrintStatement(this);
        }
    }
}
