namespace SharpCompiler.AbstractSyntaxTree;

public class BinaryExpression : Expression
{
    public Expression Left { get; }

    public string Operator { get; }

    public Expression Right { get; }

    public BinaryExpression(Expression left, string @operator, Expression right)
    {
        Left = left;
        Operator = @operator;
        Right = right;
    }

    public override void Accept(IAstVisitor visitor)
    {
        visitor.VisitBinaryExpression(this);
    }
}