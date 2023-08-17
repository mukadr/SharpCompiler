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

        var assignment = Assert.IsType<AssignmentStatement>(statement);

        Assert.Equal(id, assignment.Variable);
    }

    [Fact]
    public void Accepts_If_Statement()
    {
        var statement = ParseAllText("if (10) x = 30;");

        var ifStatement = Assert.IsType<IfStatement>(statement);
        Assert.IsType<IntegerExpression>(ifStatement.Condition);
        Assert.IsType<AssignmentStatement>(ifStatement.TrueStatement);
        Assert.Null(ifStatement.FalseStatement);
    }

    [Fact]
    public void Accepts_If_With_Else_Statement()
    {
        var statement = ParseAllText("if (1 + 2) y = 1; else y = 2;");

        var ifStatement = Assert.IsType<IfStatement>(statement);
        Assert.IsType<BinaryExpression>(ifStatement.Condition);
        Assert.IsType<AssignmentStatement>(ifStatement.TrueStatement);
        Assert.IsType<AssignmentStatement>(ifStatement.FalseStatement);
    }

    [Fact]
    public void Accepts_While_Statement()
    {
        var statement = ParseAllText("while (1) y = 2;");

        var whileStatement = Assert.IsType<WhileStatement>(statement);
        Assert.IsType<IntegerExpression>(whileStatement.Condition);
        Assert.IsType<AssignmentStatement>(whileStatement.Statement);
    }
}