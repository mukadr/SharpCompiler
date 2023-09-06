namespace SharpCompiler.AbstractSyntaxTree;

public interface INodeVisitor
{
    void VisitAssignmentStatement(AssignmentStatement assignmentStatement);
    void VisitFuncStatement(FuncStatement funcStatement);
    void VisitIfStatement(IfStatement ifStatement);
    void VisitWhileStatement(WhileStatement whileStatement);
    void VisitPrintStatement(PrintStatement printStatement);
    void VisitAssertStatement(AssertStatement assertStatement);
    void VisitBinaryExpression(BinaryExpression binaryExpression);
    void VisitBooleanExpression(BooleanExpression booleanExpression);
    void VisitIntegerExpression(IntegerExpression integerExpression);
    void VisitStringExpression(StringExpression stringExpression);
    void VisitVariableExpression(VariableExpression variableExpression);
}