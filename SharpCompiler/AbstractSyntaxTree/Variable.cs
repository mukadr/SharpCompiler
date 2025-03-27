namespace SharpCompiler.AbstractSyntaxTree;

public class Variable(string name, SharpType type)
{
    public string Name { get; } = name;

    public SharpType Type { get; } = type;

    public bool Initialized = false;
}
