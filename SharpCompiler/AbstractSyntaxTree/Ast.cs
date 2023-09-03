namespace SharpCompiler.AbstractSyntaxTree;

public abstract class Ast
{
    public abstract void Accept(IAstVisitor visitor);
}
