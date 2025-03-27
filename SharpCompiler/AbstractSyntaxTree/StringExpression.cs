namespace SharpCompiler.AbstractSyntaxTree;

public class StringExpression(string value) : Expression
{
    public string Value { get; } = value;

    public override void Accept(INodeVisitor visitor)
    {
        visitor.VisitStringExpression(this);
    }
}