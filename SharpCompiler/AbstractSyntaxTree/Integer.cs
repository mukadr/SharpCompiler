namespace SharpCompiler.AbstractSyntaxTree;

public class Integer : Expression
{
    public int Value { get; }

    public Integer(int value)
    {
        Value = value;
    }
}