namespace SharpCompiler.AbstractSyntaxTree;

public class IntegerExpression(int value) : Expression
{
    public int Value { get; } = value;

    public override void Accept(INodeVisitor visitor)
    {
        visitor.VisitIntegerExpression(this);
    }
}