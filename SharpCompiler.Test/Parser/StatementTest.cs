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
        var statement = Parse($"{id} = 1;");

        var assignment = Assert.IsType<AssignmentStatement>(statement);

        Assert.Equal(id, assignment.Variable);
    }

    [Fact]
    public void Accepts_If_Statement()
    {
        var statement = Parse("if (10) x = 30;");

        var ifStatement = Assert.IsType<IfStatement>(statement);
        Assert.IsType<IntegerExpression>(ifStatement.Condition);
        Assert.IsType<AssignmentStatement>(ifStatement.TrueStatement);
        Assert.Null(ifStatement.FalseStatement);
    }

    [Fact]
    public void Accepts_If_With_Else_Statement()
    {
        var statement = Parse("if (1 + 2) y = 1; else y = 2;");

        var ifStatement = Assert.IsType<IfStatement>(statement);
        Assert.IsType<BinaryExpression>(ifStatement.Condition);
        Assert.IsType<AssignmentStatement>(ifStatement.TrueStatement);
        Assert.IsType<AssignmentStatement>(ifStatement.FalseStatement);
    }

    [Fact]
    public void Accepts_While_Statement()
    {
        var statement = Parse("while (1) y = 2;");

        var whileStatement = Assert.IsType<WhileStatement>(statement);
        Assert.IsType<IntegerExpression>(whileStatement.Condition);
        Assert.IsType<AssignmentStatement>(whileStatement.Statement);
    }

    [Fact]
    public void Accepts_Empty_Func()
    {
        var statement = Parse("func empty() {}");

        var funcStatement = Assert.IsType<FuncStatement>(statement);
        Assert.Equal("empty", funcStatement.Name);
    }

    [Fact]
    public void Accepts_Func_With_Statements()
    {
        var statement = Parse("func empty() { x = 1; y = 2; }");

        var funcStatement = Assert.IsType<FuncStatement>(statement);

        Assert.Equal("empty", funcStatement.Name);
        Assert.Equal(2, funcStatement.Children.Count);

        var xAssignment = Assert.IsType<AssignmentStatement>(funcStatement.Children[0]);
        var yAssignment = Assert.IsType<AssignmentStatement>(funcStatement.Children[1]);

        Assert.Equal("x", xAssignment.Variable);
        Assert.Equal("y", yAssignment.Variable);
    }

    [Fact]
    public void Accepts_Func_With_Statements_Two()
    {
        var statement = Parse(@"
            func sample() {
                x = 0;
                if (1) x = 2; else x = 1;
                while (2) x = 0;
            }");

        var funcStatement = Assert.IsType<FuncStatement>(statement);

        Assert.Equal(3, funcStatement.Children.Count);
    }
}