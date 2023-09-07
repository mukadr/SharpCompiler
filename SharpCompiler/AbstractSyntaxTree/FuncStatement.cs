using System.Collections.Generic;

namespace SharpCompiler.AbstractSyntaxTree;

public class FuncStatement : Statement
{
    public SharpType ReturnType { get; }

    public string Name { get; }

    public List<Statement> Children { get; }

    public FuncStatement(string name, List<Statement> children)
    {
        ReturnType = SharpType.Void;
        Name = name;
        Children = children;
    }

    public override void Accept(INodeVisitor visitor)
    {
        visitor.VisitFuncStatement(this);
    }
}