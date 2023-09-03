namespace SharpCompiler.AbstractSyntaxTree;

public abstract class Expression : Node
{
    public SharpType Type { get; set; } = SharpType.Unknown;
}