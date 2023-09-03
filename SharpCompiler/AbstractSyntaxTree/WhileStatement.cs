namespace SharpCompiler.AbstractSyntaxTree;

public class WhileStatement : Statement
{
    public Expression Condition { get; }

    public Statement Statement { get; }

    public WhileStatement(Expression condition, Statement statement)
    {
        Condition = condition;
        Statement = statement;
    }

    public override void Accept(IAstVisitor visitor)
    {
        visitor.VisitWhileStatement(this);
    }
}