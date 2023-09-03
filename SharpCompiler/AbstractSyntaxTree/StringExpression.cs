namespace SharpCompiler.AbstractSyntaxTree;

public class StringExpression : Expression
{
    public string Value { get; }

    public StringExpression(string value)
    {
        Value = value;
    }

    public override void Accept(IAstVisitor visitor)
    {
        visitor.VisitStringExpression(this);
    }
}