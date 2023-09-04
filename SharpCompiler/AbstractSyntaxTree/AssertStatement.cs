namespace SharpCompiler.AbstractSyntaxTree
{
    public class AssertStatement : Statement
    {
        public Expression Expression { get; }

        public AssertStatement(Expression expression)
        {
            Expression = expression;
        }

        public override void Accept(INodeVisitor visitor)
        {
            visitor.VisitAssertStatement(this);
        }
    }
}
