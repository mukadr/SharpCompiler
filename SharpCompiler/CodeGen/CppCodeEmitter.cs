using SharpCompiler.AbstractSyntaxTree;
using System;
using System.IO;

namespace SharpCompiler.CodeGen;

public class CppCodeEmitter : INodeVisitor
{
    private TextWriter Writer { get; set; }

    private int _indentation = 0;

    public CppCodeEmitter(TextWriter? writer = null)
    {
        Writer = writer ?? Console.Out;
    }

    public TextWriter Compile(Node program)
    {
        EmitLine("#include <iostream>");

        return Emit(program);
    }

    public TextWriter Emit(Node program)
    {
        program.Accept(this);

        return Writer;
    }

    private void Emit(string code)
    {
        Writer.Write(code);
    }

    private void EmitLine(string code)
    {
        Writer.WriteLine(code);
    }

    private void EmitIndentation()
    {
        for (var i = 0; i < _indentation; i++)
        {
            Writer.Write("    ");
        }
    }

    public void VisitAssignmentStatement(AssignmentStatement assignmentStatement)
    {
        EmitIndentation();
        Emit(assignmentStatement.Variable);
        Emit(" = ");

        assignmentStatement.Rhs.Accept(this);

        EmitLine(";");
    }

    public void VisitPrintStatement(PrintStatement printStatement)
    {
        EmitIndentation();
        Emit("std::cout << (");

        printStatement.Expression.Accept(this);

        EmitLine(") << std::endl;");
    }

    public void VisitAssertStatement(AssertStatement assertStatement)
    {
        EmitIndentation();
        Emit("assert(");

        assertStatement.Expression.Accept(this);

        EmitLine(");");
    }

    public void VisitFuncStatement(FuncStatement funcStatement)
    {
        var returnType = funcStatement.Name == "main" ? "int" : funcStatement.ReturnType.ToCppType();

        Emit(returnType);
        Emit(" ");
        Emit(funcStatement.Name);
        EmitLine("() {");

        _indentation++;
        foreach (var child in funcStatement.Children)
        {
            child.Accept(this);
        }
        _indentation--;

        EmitLine("}");
    }

    public void VisitIfStatement(IfStatement ifStatement)
    {
        EmitIndentation();
        Emit("if (");

        ifStatement.Condition.Accept(this);

        EmitLine(") {");

        _indentation++;
        ifStatement.TrueStatement.Accept(this);
        _indentation--;

        if (ifStatement.FalseStatement is not null)
        {
            EmitIndentation();
            EmitLine("} else {");

            _indentation++;
            ifStatement.FalseStatement.Accept(this);
            _indentation--;
        }

        EmitIndentation();
        EmitLine("}");
    }

    public void VisitWhileStatement(WhileStatement whileStatement)
    {
        EmitIndentation();
        Emit("while (");

        whileStatement.Condition.Accept(this);

        EmitLine(") {");

        _indentation++;
        whileStatement.Statement.Accept(this);
        _indentation--;

        EmitIndentation();
        EmitLine("}");
    }

    public void VisitBinaryExpression(BinaryExpression binaryExpression)
    {
        Emit("(");

        binaryExpression.Left.Accept(this);

        Emit(" ");
        Emit(binaryExpression.Operator);
        Emit(" ");

        binaryExpression.Right.Accept(this);

        Emit(")");
    }

    public void VisitIntegerExpression(IntegerExpression integerExpression)
    {
        Emit(integerExpression.Value.ToString());
    }

    public void VisitStringExpression(StringExpression stringExpression)
    {
        Emit("\"");
        Emit(stringExpression.Value);
        Emit("\"");
    }
}
