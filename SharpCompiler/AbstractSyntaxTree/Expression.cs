namespace SharpCompiler.AbstractSyntaxTree;

public abstract class Expression : Ast
{
    public Type Type { get; set; } = Type.Unknown;
}