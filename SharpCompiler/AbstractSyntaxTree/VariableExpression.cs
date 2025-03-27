namespace SharpCompiler.AbstractSyntaxTree;

public class VariableExpression(string name) : Expression
{
    public string Name { get; } = name;

    public override void Accept(INodeVisitor visitor)
    {
        visitor.VisitVariableExpression(this);
    }
}