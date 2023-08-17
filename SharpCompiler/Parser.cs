using static ParseSharp.Parser;
using SharpCompiler.AbstractSyntaxTree;

namespace SharpCompiler;

public class Parser
{
    private static ParseSharp.Parser<Expression> ExpressionParser;

    static Parser()
    {
        var digit = Match('0', '9');
        var number = OneOrMore(digit).Skip(Whitespace);
        var plus = Token("+");
        var expression = number.Map<Expression>(n => new Integer(int.Parse(n)));
        var binaryExpression = BinaryExpression(expression, plus, (left, op, right, position) => new BinaryExpression(left, op, right));

        ExpressionParser = binaryExpression;
    }

    public static Expression ParseAllText(string sourceText) => new Parser(sourceText).Parse();

    private string _sourceText;

    public Parser(string sourceText)
    {
        _sourceText = sourceText;
    }

    public Expression Parse() => ExpressionParser.ParseAllText(_sourceText);   
}
