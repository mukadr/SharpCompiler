namespace SharpCompiler.AbstractSyntaxTree;

public class AssignmentStatement : Statement
{
    public string VariableName { get; }

    public Expression Rhs { get; }

    public Variable? Variable { get; set; }

    public AssignmentStatement(string variableName, Expression rhs)
    {
        VariableName = variableName;
        Rhs = rhs;
    }

    public override void Accept(INodeVisitor visitor)
    {
        visitor.VisitAssignmentStatement(this);
    }
}