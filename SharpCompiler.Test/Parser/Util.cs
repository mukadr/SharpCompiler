using SharpCompiler.AbstractSyntaxTree;

namespace SharpCompiler.Test.Parser;

public static class Util
{
    internal static Statement GetSingleStatement(Statement function)
    {
        var functionStatement = Assert.IsType<FunctionStatement>(function);

        Assert.Single(functionStatement.Children);

        return functionStatement.Children[0];
    }
}