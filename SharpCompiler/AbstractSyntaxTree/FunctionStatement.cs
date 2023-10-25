using System.Collections.Generic;

namespace SharpCompiler.AbstractSyntaxTree;

public class FunctionStatement : Statement
{
    public SharpType ReturnType { get; }

    public string Name { get; }

    public List<Statement> Children { get; }

    public FunctionStatement(string name, List<Statement> children)
    {
        ReturnType = SharpType.Void;
        Name = name;
        Children = children;
    }

    public override void Accept(INodeVisitor visitor)
    {
        visitor.VisitFunctionStatement(this);
    }
}