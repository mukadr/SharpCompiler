using static ParseSharp.Parser;
using SharpCompiler.AbstractSyntaxTree;

namespace SharpCompiler;

public class Parser
{
    private static ParseSharp.Parser<Statement> StatementParser;

    static Parser()
    {
        var digit = Match('0', '9');
        var number = OneOrMore(digit).Skip(Whitespace);
        var letter = Match('a', 'z').Or(Match('A', 'Z')).Or(Match('_'));
        var ident = letter.Bind(first => ZeroOrMore(letter.Or(digit)).Map(rest => first + rest)).Skip(Whitespace);
        var plus = Token("+");
        var minus = Token("-");
        var star = Token("*");
        var slash = Token("/");
        var assign = Token("=");
        var semi = Token(";");
        var @if = Token("if");
        var @else = Token("else");
        var lparen = Token("(");
        var rparen = Token(")");

        var factor = number.Map<Expression>(n => new IntegerExpression(int.Parse(n)));

        var mulExpression = BinaryExpression(
            factor, star.Or(slash), (left, op, right, position) => new BinaryExpression(left, op, right));

        var expression = BinaryExpression(
            mulExpression, plus.Or(minus), (left, op, right, position) => new BinaryExpression(left, op, right));

        var assignment = ident.Bind(id => assign.And(expression.Map<Statement>(expr => new AssignmentStatement(id, expr))).Bind(expr => semi.Map(_ => expr)));

        var ifStatement = @if.And(lparen.And(expression.Bind(e => rparen.And(assignment.Bind(t => Optional(@else.And(assignment)).Map<Statement>(f => new IfStatement(e, t, f)))))));

        var statement = ifStatement.Or(assignment);

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
