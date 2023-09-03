namespace SharpCompiler.AbstractSyntaxTree;

public interface IAstVisitor
{
    void VisitAssignmentStatement(AssignmentStatement assignmentStatement);
    void VisitFuncStatement(FuncStatement funcStatement);
    void VisitIfStatement(IfStatement ifStatement);
    void VisitWhileStatement(WhileStatement whileStatement);
    void VisitBinaryExpression(BinaryExpression binaryExpression);
    void VisitIntegerExpression(IntegerExpression integerExpression);
}