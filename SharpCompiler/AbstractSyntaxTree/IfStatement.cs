namespace SharpCompiler.AbstractSyntaxTree;

public class IfStatement : Statement
{
    public Expression Condition { get; }

    public Statement TrueStatement { get; }

    public IfStatement(Expression condition, Statement trueStatement)
    {
        Condition = condition;
        TrueStatement = trueStatement;
    }
}