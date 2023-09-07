using SharpCompiler.AbstractSyntaxTree;

namespace SharpCompiler.Test.Parser;

public static class Util
{
    internal static Statement GetSingleStatement(Statement function)
    {
        var funcStatement = Assert.IsType<FuncStatement>(function);

        Assert.Single(funcStatement.Children);

        return funcStatement.Children[0];
    }
}