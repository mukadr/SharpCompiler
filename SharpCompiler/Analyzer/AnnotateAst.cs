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
            throw new Exception("Can't pop global scope.");
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
            throw new Exception("Can't assign void.");
        }

        if (lhs != rhs)
        {
            throw new Exception("Incompatible types in assingment statement.");
        }

        return lhs;
    }

    public void VisitBinaryExpression(BinaryExpression binaryExpression)
    {
        binaryExpression.Left.Accept(this);
        binaryExpression.Right.Accept(this);

        // XXX: Handle types for binary operators
        binaryExpression.Type = TypeCheckAssignment(binaryExpression.Left.Type, binaryExpression.Right.Type);
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

        if (ifStatement.Condition.Type != SharpType.Integer)
        {
            // XXX: Add boolean type
            throw new Exception("Expected integer in if condition.");
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
            throw new Exception("Expected string in print statement");
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
            throw new Exception("Expected integer in while condition.");
        }

        PushScope();

        whileStatement.Statement.Accept(this);

        PopScope();
    }
}