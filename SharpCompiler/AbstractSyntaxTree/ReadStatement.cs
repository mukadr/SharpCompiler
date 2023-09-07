namespace SharpCompiler.AbstractSyntaxTree;

public class ReadStatement : Statement
{
    public VariableExpression VariableExpression { get; }

    public Variable? Variable { get; set; }

    public ReadStatement(VariableExpression variableExpression)
    {
        VariableExpression = variableExpression;
    }

    public override void Accept(INodeVisitor visitor)
    {
        visitor.VisitReadStatement(this);
    }
}
