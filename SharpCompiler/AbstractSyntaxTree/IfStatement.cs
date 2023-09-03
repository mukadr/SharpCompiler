namespace SharpCompiler.AbstractSyntaxTree;

public class IfStatement : Statement
{
    public Expression Condition { get; }

    public Statement TrueStatement { get; }

    public Statement? FalseStatement { get; }

    public IfStatement(Expression condition, Statement trueStatement, Statement? falseStatement)
    {
        Condition = condition;
        TrueStatement = trueStatement;
        FalseStatement = falseStatement;
    }

    public override void Accept(INodeVisitor visitor)
    {
        visitor.VisitIfStatement(this);
    }
}