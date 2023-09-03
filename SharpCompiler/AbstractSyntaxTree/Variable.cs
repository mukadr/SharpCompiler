namespace SharpCompiler.AbstractSyntaxTree;

public class Variable
{
    public string Name { get; }

    public SharpType Type { get; }

    public Variable(string name, SharpType type)
    {
        Name = name;
        Type = type;
    }
}
