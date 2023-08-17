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

    [Theory]
    [InlineData("+")]
    public void Accepts_Binary_Expression(string @operator)
    {
        var expression = ParseAllText("1" + @operator + "2" + @operator + "3");

        var binary = Assert.IsType<BinaryExpression>(expression);
        var subTree = Assert.IsType<BinaryExpression>(binary.Left);
        var lhs = Assert.IsType<Integer>(subTree.Left);
        var middle = Assert.IsType<Integer>(subTree.Right);
        var rhs = Assert.IsType<Integer>(binary.Right);

        Assert.Equal(1, lhs.Value);
        Assert.Equal(@operator, subTree.Operator);
        Assert.Equal(2, middle.Value);
        Assert.Equal(@operator, binary.Operator);
        Assert.Equal(3, rhs.Value);
    }
}