using static ParseSharp.Parser;
using SharpCompiler.AbstractSyntaxTree;

namespace SharpCompiler;

public class Parser
{
    private static ParseSharp.Parser<Statement> StatementParser;

    static Parser()
    {
        var digit = Match('0', '9');
        var letter = Match('a', 'z').Or(Match('A', 'Z')).Or(Match('_'));

        var number = Token(OneOrMore(digit));
        var ident = Token(letter.Bind(first => ZeroOrMore(letter.Or(digit)).Map(rest => first + rest)));
        var plus = Token("+");
        var minus = Token("-");
        var star = Token("*");
        var slash = Token("/");
        var assign = Token("=");
        var semi = Token(";");
        var @if = Token("if");
        var @else = Token("else");
        var @while = Token("while");
        var lparen = Token("(");
        var rparen = Token(")");

        var factor = number.Map<Expression>(n => new IntegerExpression(int.Parse(n.Value)));

        var mulExpression = BinaryExpression(
            factor,
            star.Or(slash),
            (left, op, right, position) => new BinaryExpression(left, op, right));

        var expression = BinaryExpression(
            mulExpression,
            plus.Or(minus),
            (left, op, right, position) => new BinaryExpression(left, op, right));

        var assignment = ident.Bind(id =>
            assign.And(expression.Map<Statement>(expr =>
                new AssignmentStatement(id.Value, expr))).Bind(expr => semi.Map(_ => expr)));

        var ifStatement = @if.And(lparen.And(expression.Bind(e =>
            rparen.And(assignment.Bind(t =>
                Optional(@else.And(assignment)).Map<Statement>(f => new IfStatement(e, t, f)))))));

        var whileStatement = @while.And(lparen.And(expression.Bind(e =>
            rparen.And(assignment.Map<Statement>(s => new WhileStatement(e, s))))));

        var statement = assignment.Or(ifStatement).Or(whileStatement);

        StatementParser = statement;
    }

    public static Statement ParseAllText(string sourceText) => new Parser(sourceText).Parse();

    private string _sourceText;

    public Parser(string sourceText)
    {
        _sourceText = sourceText;
    }

    public Statement Parse() => StatementParser.ParseAllText(_sourceText);   
}
