using System;
using SharpCompiler.AbstractSyntaxTree;

namespace SharpCompiler.Analyzer;

public class AnnotateAst : INodeVisitor
{
    private Scope GlobalScope { get; }
    private Scope CurrentScope { get; set; }

    public AnnotateAst()
    {
        GlobalScope = new Scope();
        CurrentScope = GlobalScope;
    }

    public static void Analyze(Node program) => new AnnotateAst().Annotate(program);

    public void Annotate(Node program)
    {
        program.Accept(this);
    }

    private void PushScope()
    {
        CurrentScope = new Scope(CurrentScope);
    }

    private void PopScope()
    {
        if (CurrentScope == GlobalScope)
        {
            throw new CompileException("Can't pop global scope.");
        }

        CurrentScope = CurrentScope.PreviousScope!;
    }

    public void VisitAssignmentStatement(AssignmentStatement assignmentStatement)
    {
        assignmentStatement.Rhs.Accept(this);

        var variable = CurrentScope.GetVariable(assignmentStatement.Variable);

        if (variable is null)
        {
            variable = new Variable(assignmentStatement.Variable, assignmentStatement.Rhs.Type);

            CurrentScope.Add(variable);
        }
        else
        {
            TypeCheckAssignment(variable.Type, assignmentStatement.Rhs.Type);
        }
    }

    private SharpType TypeCheckAssignment(SharpType lhs, SharpType rhs)
    {
        if (lhs == SharpType.Unknown || rhs == SharpType.Unknown)
        {
            return SharpType.Unknown;
        }

        if (lhs == SharpType.Void || rhs == SharpType.Void)
        {
            throw new CompileException("Can't assign void.");
        }

        if (lhs != rhs)
        {
            throw new CompileException("Incompatible types in assingment statement.");
        }

        return lhs;
    }

    private SharpType TypeCheckBinaryExpression(SharpType lhs, string op, SharpType rhs)
    {
        if (lhs == SharpType.Unknown || rhs == SharpType.Unknown)
        {
            return SharpType.Unknown;
        }

        if (lhs == SharpType.Void || rhs == SharpType.Void)
        {
            throw new CompileException("Can't operate on void type.");
        }

        switch (op)
        {
            case "+":
                if (lhs == SharpType.String || rhs == SharpType.String)
                {
                    return SharpType.String;
                }
                if (lhs == SharpType.Integer && lhs == rhs)
                {
                    return SharpType.Integer;
                }
                break;

            case "-":
            case "*":
            case "/":
                if (lhs == SharpType.Integer && lhs == rhs)
                {
                    return SharpType.Integer;
                }
                break;

            case "==":
                if (lhs == rhs)
                {
                    return SharpType.Boolean;
                }
                break;
        }

        throw new CompileException("Incompatible types in binary expression.");
    }

    public void VisitBinaryExpression(BinaryExpression binaryExpression)
    {
        binaryExpression.Left.Accept(this);
        binaryExpression.Right.Accept(this);

        binaryExpression.Type = TypeCheckBinaryExpression(binaryExpression.Left.Type, binaryExpression.Operator, binaryExpression.Right.Type);
    }

    public void VisitFuncStatement(FuncStatement funcStatement)
    {
        PushScope();

        foreach (var child in funcStatement.Children)
        {
            child.Accept(this);
        }

        PopScope();
    }

    public void VisitIfStatement(IfStatement ifStatement)
    {
        ifStatement.Condition.Accept(this);

        if (ifStatement.Condition.Type != SharpType.Boolean)
        {
            throw new CompileException("Expected boolean expression in if condition.");
        }

        PushScope();

        ifStatement.TrueStatement.Accept(this);

        PopScope();

        if (ifStatement.FalseStatement is not null)
        {
            PushScope();

            ifStatement.FalseStatement.Accept(this);

            PopScope();
        }
    }

    public void VisitIntegerExpression(IntegerExpression integerExpression)
    {
        // NOP
    }

    public void VisitPrintStatement(PrintStatement printStatement)
    {
        printStatement.Expression.Accept(this);

        if (printStatement.Expression.Type != SharpType.String)
        {
            throw new CompileException("Expected string in print statement");
        }
    }

    public void VisitAssertStatement(AssertStatement assertStatement)
    {
        assertStatement.Expression.Accept(this);

        if (assertStatement.Expression.Type != SharpType.Boolean)
        {
            throw new CompileException("Expected boolean in assert statement.");
        }
    }

    public void VisitStringExpression(StringExpression stringExpression)
    {
        // NOP
    }

    public void VisitWhileStatement(WhileStatement whileStatement)
    {
        whileStatement.Condition.Accept(this);

        if (whileStatement.Condition.Type != SharpType.Integer)
        {
            // XXX: Add boolean type
            throw new CompileException("Expected integer in while condition.");
        }

        PushScope();

        whileStatement.Statement.Accept(this);

        PopScope();
    }
}