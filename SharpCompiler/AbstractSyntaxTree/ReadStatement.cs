namespace SharpCompiler.AbstractSyntaxTree;

public class ReadStatement(VariableExpression variableExpression) : Statement
{
    public VariableExpression VariableExpression { get; } = variableExpression;

    public Variable? Variable { get; set; }

    public override void Accept(INodeVisitor visitor)
    {
        visitor.VisitReadStatement(this);
    }
}
