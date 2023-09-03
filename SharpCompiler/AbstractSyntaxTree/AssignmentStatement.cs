namespace SharpCompiler.AbstractSyntaxTree;

public class AssignmentStatement : Statement
{
    public string Variable { get; }

    public Expression Rhs { get; }

    public AssignmentStatement(string variable, Expression rhs)
    {
        Variable = variable;
        Rhs = rhs;
    }

    public override void Accept(INodeVisitor visitor)
    {
        visitor.VisitAssignmentStatement(this);
    }
}