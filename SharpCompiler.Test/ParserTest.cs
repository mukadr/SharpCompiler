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
}