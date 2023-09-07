using SharpCompiler.AbstractSyntaxTree;
using static SharpCompiler.Parser;
using static SharpCompiler.Test.Parser.Util;

namespace SharpCompiler.Test.Parser;

public class ExpressionTest
{
    [Fact]
    public void Accepts_False()
    {
        var statement = GetSingleStatement(Parse("void t() { x = false; }"));

        var assignment = Assert.IsType<AssignmentStatement>(statement);
        var boolean = Assert.IsType<BooleanExpression>(assignment.Rhs);

        Assert.False(boolean.Value);
    }

    [Fact]
    public void Accepts_True()
    {
        var statement = GetSingleStatement(Parse("void t() { x = true; }"));

        var assignment = Assert.IsType<AssignmentStatement>(statement);
        var boolean = Assert.IsType<BooleanExpression>(assignment.Rhs);

        Assert.True(boolean.Value);
    }

    [Fact]
    public void Accepts_Integer()
    {
        var statement = GetSingleStatement(Parse("void t() { x = 35; }"));

        var assignment = Assert.IsType<AssignmentStatement>(statement);
        var integer = Assert.IsType<IntegerExpression>(assignment.Rhs);

        Assert.Equal(35, integer.Value);
    }

    [Fact]
    public void Accepts_String()
    {
        var statement = GetSingleStatement(Parse("void t() { x = \"Hello World!\"; }"));

        var assignment = Assert.IsType<AssignmentStatement>(statement);
        var @string = Assert.IsType<StringExpression>(assignment.Rhs);

        Assert.Equal("Hello World!", @string.Value);
    }

    [Theory]
    [InlineData("+")]
    [InlineData("-")]
    [InlineData("*")]
    [InlineData("/")]
    public void Accepts_Binary_Expression(string @operator)
    {
        var statement = GetSingleStatement(Parse($"void t() {{ x = 1 {@operator} 2 {@operator} 3; }}"));

        var assignment = Assert.IsType<AssignmentStatement>(statement);
        var binary = Assert.IsType<BinaryExpression>(assignment.Rhs);
        var subTree = Assert.IsType<BinaryExpression>(binary.Left);
        var lhs = Assert.IsType<IntegerExpression>(subTree.Left);
        var middle = Assert.IsType<IntegerExpression>(subTree.Right);
        var rhs = Assert.IsType<IntegerExpression>(binary.Right);

        Assert.Equal(1, lhs.Value);
        Assert.Equal(@operator, subTree.Operator);
        Assert.Equal(2, middle.Value);
        Assert.Equal(@operator, binary.Operator);
        Assert.Equal(3, rhs.Value);
    }

    [Theory]
    [InlineData("+", "*")]
    [InlineData("-", "/")]
    public void Accepts_Binary_Expression_With_Correct_Precedence(string operator1, string operator2)
    {
        var statement = GetSingleStatement(Parse($"void t() {{ x = 1 {operator1} 2 {operator2} 3; }}"));

        var assignment = Assert.IsType<AssignmentStatement>(statement);
        var binary = Assert.IsType<BinaryExpression>(assignment.Rhs);
        var lhs = Assert.IsType<IntegerExpression>(binary.Left);
        var subTree = Assert.IsType<BinaryExpression>(binary.Right);
        var middle = Assert.IsType<IntegerExpression>(subTree.Left);
        var rhs = Assert.IsType<IntegerExpression>(subTree.Right);

        Assert.Equal(1, lhs.Value);
        Assert.Equal(operator1, binary.Operator);
        Assert.Equal(2, middle.Value);
        Assert.Equal(operator2, subTree.Operator);
        Assert.Equal(3, rhs.Value);
    }
}