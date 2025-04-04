﻿using ParseSharp;
using static ParseSharp.Parser;
using SharpCompiler.AbstractSyntaxTree;
using System.Threading;

namespace SharpCompiler.Parser;

public static class SharpParser
{
    private static readonly Parser<Statement> _programParser;

    static SharpParser()
    {
        var comment = Match('#').And(Until(Match('\n'))).Map(value => value.Prefix);

        SkipWhitespace = ZeroOrMore(Whitespace.Or(comment));

        var digit = Match('0', '9');
        var letter = Match('a', 'z').Or(Match('A', 'Z')).Or(Match('_'));
        var number = Token(OneOrMore(digit));
        var identifier = Token(letter.Bind(first => ZeroOrMore(letter.Or(digit)).Map(rest => first + rest)));
        var @string = Token(Match('"').And(Until(Match('"')).Map(value => value.Prefix)));
        var plus = Token("+");
        var minus = Token("-");
        var star = Token("*");
        var slash = Token("/");
        var assign = Token("=");
        var equals = Token("==");
        var semi = Token(";");
        var lbrace = Token("{");
        var rbrace = Token("}");
        var lparen = Token("(");
        var rparen = Token(")");
        var assert = Token("assert");
        var @else = Token("else");
        var @false = Token("false");
        var @if = Token("if");
        var print = Token("print");
        var read = Token("read");
        var @true = Token("true");
        var @void = Token("void");
        var @while = Token("while");

        var booleanExpression = @true.Or(@false).Map<Expression>(b => new BooleanExpression(b.Value == "true"));

        var integerExpression = number.Map<Expression>(n => new IntegerExpression(int.Parse(n.Value)));

        var stringExpression = @string.Map<Expression>(s => new StringExpression(s.Value));

        var variableExpression = identifier.Map<Expression>(v => new VariableExpression(v.Value));

        var factor = booleanExpression
            .Or(integerExpression)
            .Or(stringExpression)
            .Or(variableExpression);

        var mulExpression = BinaryExpression(
            factor,
            star.Or(slash),
            (left, op, right, position) => new BinaryExpression(left, op, right));

        var addExpression = BinaryExpression(
            mulExpression,
            plus.Or(minus),
            (left, op, right, position) => new BinaryExpression(left, op, right));

        var expression = BinaryExpression(
            addExpression,
            equals,
            (left, op, right, position) => new BinaryExpression(left, op, right));

        var statement = Forward<Statement>();

        var printStatement = print.And(expression).Bind(e => semi.Map<Statement>(_ => new PrintStatement(e)));

        var readStatement = read.And(variableExpression).Bind(v => semi.Map<Statement>(_ => new ReadStatement((VariableExpression)v)));

        var assertStatement = assert.And(expression).Bind(e => semi.Map<Statement>(_ => new AssertStatement(e)));

        var assignmentStatement = identifier.Bind(id =>
            assign.And(expression.Map<Statement>(expr =>
                new AssignmentStatement(id.Value, expr))).Bind(expr => semi.Map(_ => expr)));

        var ifStatement = @if.And(lparen.And(expression.Bind(e =>
            rparen.And(statement.Bind(t =>
                Optional(@else.And(statement)).Map<Statement>(f => new IfStatement(e, t, f)))))));

        var whileStatement = @while.And(lparen.And(expression.Bind(e =>
            rparen.And(statement.Map<Statement>(s => new WhileStatement(e, s))))));

        var functionStatement = @void.And(identifier.Bind(name =>
            lparen.And(rparen).And(lbrace.Bind(_ =>
                ZeroOrMore(statement).Bind(children =>
                    rbrace.Map<Statement>(_ => new FunctionStatement(name.Value, children)))))));

        statement.Attach(
            printStatement
            .Or(readStatement)
            .Or(assertStatement)
            .Or(assignmentStatement)
            .Or(ifStatement)
            .Or(whileStatement));

        _programParser = SkipWhitespace.And(functionStatement);
    }

    public static Statement Parse(string sourceText) => _programParser.ParseAllText(sourceText);
}
