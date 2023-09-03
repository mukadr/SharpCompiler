namespace SharpCompiler.AbstractSyntaxTree;

public interface INodeVisitor
{
    void VisitAssignmentStatement(AssignmentStatement assignmentStatement);
    void VisitFuncStatement(FuncStatement funcStatement);
    void VisitIfStatement(IfStatement ifStatement);
    void VisitWhileStatement(WhileStatement whileStatement);
    void VisitPrintStatement(PrintStatement printStatement);
    void VisitBinaryExpression(BinaryExpression binaryExpression);
    void VisitIntegerExpression(IntegerExpression integerExpression);
    void VisitStringExpression(StringExpression stringExpression);
}