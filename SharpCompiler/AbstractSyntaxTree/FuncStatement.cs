using System.Collections.Generic;

namespace SharpCompiler.AbstractSyntaxTree
{
    public class FuncStatement : Statement
    {
        public string Name { get; }

        public List<Statement> Children { get; }

        public FuncStatement(string name, List<Statement> children)
        {
            Name = name;
            Children = children;
        }

        public override void Accept(IAstVisitor visitor)
        {
            visitor.VisitFuncStatement(this);
        }
    }
}
