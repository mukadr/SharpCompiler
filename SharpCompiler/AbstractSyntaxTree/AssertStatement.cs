namespace SharpCompiler.AbstractSyntaxTree;

public class AssertStatement(Expression expression) : Statement
{
    public Expression Expression { get; } = expression;

    public override void Accept(INodeVisitor visitor)
    {
        visitor.VisitAssertStatement(this);
    }
}