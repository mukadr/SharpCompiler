namespace SharpCompiler.AbstractSyntaxTree;

public class Assignment : Statement
{
    public string Variable { get; }

    public Expression Rhs { get; }

    public Assignment(string variable, Expression rhs)
    {
        Variable = variable;
        Rhs = rhs;
    }
}