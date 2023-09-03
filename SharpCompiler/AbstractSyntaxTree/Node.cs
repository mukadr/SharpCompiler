namespace SharpCompiler.AbstractSyntaxTree;

public abstract class Node
{
    public abstract void Accept(INodeVisitor visitor);
}
