using static SharpCompiler.Parser;
using static SharpCompiler.Analyzer.AnnotateAst;

namespace SharpCompiler.Test.Analyzer;

public class AnalyzerTest
{
    [Fact]
    public void Print_Statement_Accepts_String()
    {
        var program = Parse("void t() { print \"Hello World!\"; }");

        Analyze(program);
    }

    [Fact]
    public void Print_Statement_Rejects_Integer()
    {
        var program = Parse("void t() { print 150; }");

        Assert.ThrowsAny<CompileException>(() => Analyze(program));
    }

    [Fact]
    public void Print_Statement_Rejects_Boolean()
    {
        var program = Parse("void t() { print 1 == 0; }");

        Assert.ThrowsAny<CompileException>(() => Analyze(program));
    }

    [Fact]
    public void If_Statement_Accepts_Boolean_Condition()
    {
        var program = Parse("void t() { if (true) x = 2; }");

        Analyze(program);
    }

    [Fact]
    public void If_Statement_Rejects_Integer_Condition()
    {
        var program = Parse("void t() { if (1) x = 2; }");

        Assert.ThrowsAny<CompileException>(() => Analyze(program));
    }

    [Fact]
    public void If_Statement_Rejects_String_Condition()
    {
        var program = Parse("void t() { if (\"a\") x = 2; }");

        Assert.ThrowsAny<CompileException>(() => Analyze(program));
    }

    [Fact]
    public void While_Statement_Accepts_Boolean_Condition()
    {
        var program = Parse("void t() { while (false) x = 2; }");

        Analyze(program);
    }

    [Fact]
    public void While_Statement_Rejects_Integer_Condition()
    {
        var program = Parse("void t() { while (1) x = 2; }");

        Assert.ThrowsAny<CompileException>(() => Analyze(program));
    }

    [Fact]
    public void While_Statement_Rejects_String_Condition()
    {
        var program = Parse("void t() { while (\"a\") x = 2; }");

        Assert.ThrowsAny<CompileException>(() => Analyze(program));
    }

    [Fact]
    public void Can_Use_Declared_Variable()
    {
        var program = Parse(@"
            void variables() {
                x = 10;
                y = 2 * x;
            }");

        Analyze(program);
    }

    [Fact]
    public void Cannot_Use_Undeclared_Variable()
    {
        var program = Parse(@"
            void variables() {
                x = y;
            }");

        Assert.ThrowsAny<CompileException>(() => Analyze(program));
    }

    [Fact]
    public void Can_Assign_Same_Type_To_Same_Variable_Twice()
    {
        var program = Parse(@"
            void variables() {
                x = 10;
                x = 20;
            }");

        Analyze(program);
    }

    [Fact]
    public void Cannot_Assign_Different_Types_To_Same_Variable()
    {
        var program = Parse(@"
            void variables() {
                x = 10;
                x = false;
            }");

        Assert.ThrowsAny<CompileException>(() => Analyze(program));
    }

    [Fact]
    public void Cannot_Assign_Different_Types_To_Same_Variable_In_Different_Scopes()
    {
        var program = Parse(@"
            void variables() {
                x = 10;
                if (true) x = false;
            }");

        Assert.ThrowsAny<CompileException>(() => Analyze(program));
    }

    [Fact]
    public void Cannot_Read_Undeclared_variable()
    {
        var program = Parse(@"
            void variables() {
                read x;
            }");

        Assert.ThrowsAny<CompileException>(() => Analyze(program));
    }

    [Fact]
    public void Can_Read_Declared_Variable()
    {
        var program = Parse(@"
            void variables() {
                x = 10;
                read x;
            }");

        Analyze(program);
    }
}