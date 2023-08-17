using SharpCompiler.AbstractSyntaxTree;
using static SharpCompiler.Parser;

namespace SharpCompiler.Test;

public class ParserTest
{
    [Fact]
    public void Accepts_Number()
    {
        var expression = ParseAllText("35");

        var integer = Assert.IsType<Integer>(expression);

        Assert.Equal(35, integer.Value);
    }

    [Fact]
    public void Accepts_Plus_Expression()
    {
        var expression = ParseAllText("1 + 2");

        var binary = Assert.IsType<BinaryExpression>(expression);
        var lhs = Assert.IsType<Integer>(binary.Left);
        var rhs = Assert.IsType<Integer>(binary.Right);

        Assert.Equal(1, lhs.Value);
        Assert.Equal("+", binary.Operator);
        Assert.Equal(2, rhs.Value);
    }
}