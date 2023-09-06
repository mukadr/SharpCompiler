using static SharpCompiler.Parser;
using static SharpCompiler.Analyzer.AnnotateAst;

namespace SharpCompiler.Test.Analyzer;

public class AnalyzerTest
{
    [Fact]
    public void Print_Statement_Accepts_String()
    {
        var program = Parse("print \"Hello World!\";");

        Analyze(program);
    }

    [Fact]
    public void Print_Statement_Rejects_Integer()
    {
        var program = Parse("print 150;");

        Assert.ThrowsAny<Exception>(() => Analyze(program));
    }

    [Fact]
    public void Print_Statement_Rejects_Boolean()
    {
        var program = Parse("print 1 == 0;");

        Assert.ThrowsAny<Exception>(() => Analyze(program));
    }

    [Fact]
    public void If_Statement_Accepts_Boolean_Condition()
    {
        var program = Parse("if (true) x = 2;");

        Analyze(program);
    }

    [Fact]
    public void If_Statement_Rejects_Integer_Condition()
    {
        var program = Parse("if (1) x = 2;");

        Assert.ThrowsAny<Exception>(() => Analyze(program));
    }

    [Fact]
    public void If_Statement_Rejects_String_Condition()
    {
        var program = Parse("if (\"a\") x = 2;");

        Assert.ThrowsAny<Exception>(() => Analyze(program));
    }

    [Fact]
    public void While_Statement_Accepts_Boolean_Condition()
    {
        var program = Parse("while (false) x = 2;");

        Analyze(program);
    }

    [Fact]
    public void While_Statement_Rejects_Integer_Condition()
    {
        var program = Parse("while (1) x = 2;");

        Assert.ThrowsAny<Exception>(() => Analyze(program));
    }

    [Fact]
    public void While_Statement_Rejects_String_Condition()
    {
        var program = Parse("while (\"a\") x = 2;");

        Assert.ThrowsAny<Exception>(() => Analyze(program));
    }
}