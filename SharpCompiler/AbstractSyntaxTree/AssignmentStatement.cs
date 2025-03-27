namespace SharpCompiler.AbstractSyntaxTree;

public class AssignmentStatement(string variableName, Expression rhs) : Statement
{
    public string VariableName { get; } = variableName;

    public Expression Rhs { get; } = rhs;

    public Variable? Variable { get; set; }

    public override void Accept(INodeVisitor visitor)
    {
        visitor.VisitAssignmentStatement(this);
    }
}