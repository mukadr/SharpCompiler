namespace SharpCompiler.AbstractSyntaxTree;

public class StringExpression : Expression
{
    public string Value { get; }

    public StringExpression(string value)
    {
        Value = value;
        Type = Type.String;
    }

    public override void Accept(INodeVisitor visitor)
    {
        visitor.VisitStringExpression(this);
    }
}