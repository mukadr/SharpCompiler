namespace SharpCompiler.AbstractSyntaxTree;

public class IntegerExpression : Expression
{
    public int Value { get; }

    public IntegerExpression(int value)
    {
        Value = value;
    }

    public override void Accept(INodeVisitor visitor)
    {
        visitor.VisitIntegerExpression(this);
    }
}