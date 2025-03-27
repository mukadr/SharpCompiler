namespace SharpCompiler.AbstractSyntaxTree;

public class PrintStatement(Expression expression) : Statement
{
    public Expression Expression { get; } = expression;

    public override void Accept(INodeVisitor visitor)
    {
        visitor.VisitPrintStatement(this);
    }
}
