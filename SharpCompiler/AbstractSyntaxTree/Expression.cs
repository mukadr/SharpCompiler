namespace SharpCompiler.AbstractSyntaxTree;

public class Expression
{
    public string Value { get; set; }

    public Expression(string value)
    {
        Value = value;
    }
}