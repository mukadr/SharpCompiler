namespace SharpCompiler.AbstractSyntaxTree;

public class VariableExpression : Expression
{
    public string Name { get; }

    public VariableExpression(string name)
    {
        Name = name;
    }

    public override void Accept(INodeVisitor visitor)
    {
        visitor.VisitVariableExpression(this);
    }
}