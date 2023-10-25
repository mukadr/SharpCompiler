namespace SharpCompiler.AbstractSyntaxTree;

public interface INodeVisitor
{
    void VisitAssignmentStatement(AssignmentStatement assignmentStatement);
    void VisitFunctionStatement(FunctionStatement functionStatement);
    void VisitIfStatement(IfStatement ifStatement);
    void VisitWhileStatement(WhileStatement whileStatement);
    void VisitPrintStatement(PrintStatement printStatement);
    void VisitReadStatement(ReadStatement readStatement);
    void VisitAssertStatement(AssertStatement assertStatement);
    void VisitBinaryExpression(BinaryExpression binaryExpression);
    void VisitBooleanExpression(BooleanExpression booleanExpression);
    void VisitIntegerExpression(IntegerExpression integerExpression);
    void VisitStringExpression(StringExpression stringExpression);
    void VisitVariableExpression(VariableExpression variableExpression);
}