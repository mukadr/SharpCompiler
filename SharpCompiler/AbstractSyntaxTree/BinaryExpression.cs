namespace SharpCompiler.AbstractSyntaxTree;

public class BinaryExpression(Expression left, string @operator, Expression right) : Expression
{
    public Expression Left { get; } = left;

    public string Operator { get; } = @operator;

    public Expression Right { get; } = right;

    public override void Accept(INodeVisitor visitor)
    {
        visitor.VisitBinaryExpression(this);
    }
}