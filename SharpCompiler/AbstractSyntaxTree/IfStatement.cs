namespace SharpCompiler.AbstractSyntaxTree;

public class IfStatement(Expression condition, Statement trueStatement, Statement? falseStatement) : Statement
{
    public Expression Condition { get; } = condition;

    public Statement TrueStatement { get; } = trueStatement;

    public Statement? FalseStatement { get; } = falseStatement;

    public override void Accept(INodeVisitor visitor)
    {
        visitor.VisitIfStatement(this);
    }
}