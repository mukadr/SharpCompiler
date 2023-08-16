using static SharpCompiler.Parser;

namespace SharpCompiler.Test;

public class ParserTest
{
    [Fact]
    public void Accepts_Number()
    {
        var expression = ParseAllText("35");

        Assert.Equal("35", expression.Value);
    }
}