namespace SharpCompiler.AbstractSyntaxTree;

public class WhileStatement(Expression condition, Statement statement) : Statement
{
    public Expression Condition { get; } = condition;

    public Statement Statement { get; } = statement;

    public override void Accept(INodeVisitor visitor)
    {
        visitor.VisitWhileStatement(this);
    }
}