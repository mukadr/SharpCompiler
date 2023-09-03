namespace SharpCompiler.AbstractSyntaxTree;

public abstract class Expression : Node
{
    public Type Type { get; set; } = Type.Unknown;
}