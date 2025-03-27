using System.Collections.Generic;

namespace SharpCompiler.AbstractSyntaxTree;

public class FunctionStatement(string name, List<Statement> children) : Statement
{
    public SharpType ReturnType { get; } = SharpType.Void;

    public string Name { get; } = name;

    public List<Statement> Children { get; } = children;

    public override void Accept(INodeVisitor visitor)
    {
        visitor.VisitFunctionStatement(this);
    }
}