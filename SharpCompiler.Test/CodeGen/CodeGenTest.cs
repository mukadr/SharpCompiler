﻿using SharpCompiler.CodeGen;
using static SharpCompiler.Parser;

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
        return Emitter.Emit(Parse(program)).ToString() ?? string.Empty;
    }

    [Fact]
    public void Generates_Assignment()
    {
        Assert.Equal(
            @"x = 1;
",
            Emit("x = 1;"));
    }

    [Fact]
    public void Generates_BinaryExpression()
    {
        Assert.Equal(
            @"x = (1 + (2 * 3));
",
            Emit("x = 1 + 2 * 3;"));
    }

    [Fact]
    public void Generates_If_Statement()
    {
        Assert.Equal(
            @"if (1) {
    x = 2;
} else {
    y = 3;
}
",
            Emit("if (1) x = 2; else y = 3;"));
    }

    [Fact]
    public void Generates_While_Statement()
    {
        Assert.Equal(
            @"while (1) {
    x = 2;
}
",
            Emit("while (1) x = 2;"));
    }

    [Fact]
    public void Generates_Func_Statement()
    {
        Assert.Equal(
            @"int main() {
    x = 1;
    while (2) {
        if (3) {
            x = 4;
        } else {
            x = 5;
        }
    }
}
",
            Emit("void main() { x = 1; while (2) if (3) x = 4; else x = 5; }"));
    }
}