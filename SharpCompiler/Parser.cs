using static ParseSharp.Parser;
using SharpCompiler.AbstractSyntaxTree;

namespace SharpCompiler;

public class Parser
{
    private static ParseSharp.Parser<Expression> ExpressionParser;

    static Parser()
    {
        var digit = Match('0', '9');
        var number = OneOrMore(digit);

        ExpressionParser = number.Map<Expression>(n => new Integer(int.Parse(n)));
    }

    public static Expression ParseAllText(string sourceText) => new Parser(sourceText).Parse();

    private string _sourceText;

    public Parser(string sourceText)
    {
        _sourceText = sourceText;
    }

    public Expression Parse() => ExpressionParser.ParseAllText(_sourceText);   
}
