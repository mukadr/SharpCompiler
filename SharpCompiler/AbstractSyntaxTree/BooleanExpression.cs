namespace SharpCompiler.AbstractSyntaxTree;

public class BooleanExpression(bool value) : Expression
{
    public bool Value { get; } = value;

    public override void Accept(INodeVisitor visitor)
    {
        visitor.VisitBooleanExpression(this);
    }
}