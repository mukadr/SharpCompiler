using SharpCompiler.AbstractSyntaxTree;
using static SharpCompiler.Parser;

namespace SharpCompiler.Test;

public class StatementTest
{
    [Theory]
    [InlineData("x")]
    [InlineData("Y")]
    [InlineData("alpha")]
    [InlineData("beta120")]
    [InlineData("_sharp_")]
    public void Accepts_Assignment(string id)
    {
        var statement = ParseAllText($"{id} = 1;");

        var assignment = Assert.IsType<Assignment>(statement);

        Assert.Equal(id, assignment.Variable);
    }
}