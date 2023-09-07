using SharpCompiler.AbstractSyntaxTree;
using static SharpCompiler.Parser.SharpParser;
using static SharpCompiler.Test.Parser.Util;

namespace SharpCompiler.Test.Parser;

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
        var statement = GetSingleStatement(Parse($"void t() {{ {id} = 1; }}"));

        var assignment = Assert.IsType<AssignmentStatement>(statement);

        Assert.Equal(id, assignment.VariableName);
    }

    [Fact]
    public void Accepts_Print_Statement()
    {
        var statement = GetSingleStatement(Parse("void t() { print \"Hello World!\"; }"));

        var printStatement = Assert.IsType<PrintStatement>(statement);
        var stringExpression = Assert.IsType<StringExpression>(printStatement.Expression);

        Assert.Equal("Hello World!", stringExpression.Value);
    }

    [Fact]
    public void Accepts_If_Statement()
    {
        var statement = GetSingleStatement(Parse("void t() { if (true) x = 30; }"));

        var ifStatement = Assert.IsType<IfStatement>(statement);
        Assert.IsType<BooleanExpression>(ifStatement.Condition);
        Assert.IsType<AssignmentStatement>(ifStatement.TrueStatement);
        Assert.Null(ifStatement.FalseStatement);
    }

    [Fact]
    public void Accepts_If_With_Else_Statement()
    {
        var statement = GetSingleStatement(Parse("void t() { if (1 + 2 == 3) y = 1; else y = 2; }"));

        var ifStatement = Assert.IsType<IfStatement>(statement);
        Assert.IsType<BinaryExpression>(ifStatement.Condition);
        Assert.IsType<AssignmentStatement>(ifStatement.TrueStatement);
        Assert.IsType<AssignmentStatement>(ifStatement.FalseStatement);
    }

    [Fact]
    public void Accepts_While_Statement()
    {
        var statement = GetSingleStatement(Parse("void t() { while (false) y = 2; }"));

        var whileStatement = Assert.IsType<WhileStatement>(statement);
        Assert.IsType<BooleanExpression>(whileStatement.Condition);
        Assert.IsType<AssignmentStatement>(whileStatement.Statement);
    }

    [Fact]
    public void Accepts_Empty_Func()
    {
        var statement = Parse("void t() {}");

        var funcStatement = Assert.IsType<FuncStatement>(statement);
        Assert.Equal("t", funcStatement.Name);
    }

    [Fact]
    public void Accepts_Func_With_Statements()
    {
        var statement = Parse("void t() { x = 1; y = 2; }");

        var funcStatement = Assert.IsType<FuncStatement>(statement);

        Assert.Equal("t", funcStatement.Name);
        Assert.Equal(2, funcStatement.Children.Count);

        var xAssignment = Assert.IsType<AssignmentStatement>(funcStatement.Children[0]);
        var yAssignment = Assert.IsType<AssignmentStatement>(funcStatement.Children[1]);

        Assert.Equal("x", xAssignment.VariableName);
        Assert.Equal("y", yAssignment.VariableName);
    }

    [Fact]
    public void Accepts_Func_With_Statements_Two()
    {
        var statement = Parse(@"
            void t() {
                x = 0;
                if (true) x = 2; else x = 1;
                while (false) x = 0;
            }");

        var funcStatement = Assert.IsType<FuncStatement>(statement);

        Assert.Equal(3, funcStatement.Children.Count);
    }
}