using SharpCompiler.CodeGen;
using static SharpCompiler.Parser;
using static SharpCompiler.Analyzer.AnnotateAst;

namespace SharpCompiler.Test.CodeGen;

public class CodeGenTest
{
    private CppCodeEmitter Emitter { get; }

    public CodeGenTest()
    {
        Emitter = new CppCodeEmitter(new StringWriter());
    }

    private string Emit(string program)
    {
        var node = Parse(program);

        Analyze(node);

        return Emitter.Emit(node).ToString() ?? string.Empty;
    }

    [Fact]
    public void Generates_Assignment()
    {
        Assert.Equal(
            @"int x = 1;
",
            Emit("x = 1;"));
    }

    [Fact]
    public void Generates_BinaryExpression()
    {
        Assert.Equal(
            @"int x = (1 + (2 * 3));
",
            Emit("x = 1 + 2 * 3;"));
    }

    [Fact]
    public void Generates_String_Concatenation()
    {
        Assert.Equal(
            @"std::string a = (std::string(""a"") + std::string(""b""));
",
            Emit("a = \"a\" + \"b\";"));
    }

    [Fact]
    public void Generates_If_Statement()
    {
        Assert.Equal(
            @"if (true) {
    int x = 2;
} else {
    int y = 3;
}
",
            Emit("if (true) x = 2; else y = 3;"));
    }

    [Fact]
    public void Generates_While_Statement()
    {
        Assert.Equal(
            @"while (true) {
    int x = 2;
}
",
            Emit("while (true) x = 2;"));
    }

    [Fact]
    public void Generates_Func_Statement()
    {
        Assert.Equal(
            @"int main() {
    int x = 1;
    while (true) {
        if (false) {
            x = 4;
        } else {
            x = 5;
        }
    }
}
",
            Emit("void main() { x = 1; while (true) if (false) x = 4; else x = 5; }"));
    }
}