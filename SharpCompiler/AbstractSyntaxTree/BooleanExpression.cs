namespace SharpCompiler.AbstractSyntaxTree;

public class BooleanExpression : Expression
{
    public bool Value { get; }

    public BooleanExpression(bool value)
    {
        Value = value;
    }

    public override void Accept(INodeVisitor visitor)
    {
        visitor.VisitBooleanExpression(this);
    }
}