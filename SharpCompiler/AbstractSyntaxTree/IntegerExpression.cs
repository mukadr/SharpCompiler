namespace SharpCompiler.AbstractSyntaxTree;

public class IntegerExpression : Expression
{
    public int Value { get; }

    public IntegerExpression(int value)
    {
        Value = value;
        Type = Type.Integer;
    }

    public override void Accept(IAstVisitor visitor)
    {
        visitor.VisitIntegerExpression(this);
    }
}