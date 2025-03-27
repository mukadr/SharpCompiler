using System;

namespace SharpCompiler.AbstractSyntaxTree;

public class SharpType(SharpType.BaseType baseType)
{
    public enum BaseType
    {
        Unknown,
        Void,
        Boolean,
        Integer,
        String
    }

    public static SharpType Unknown = new SharpType(BaseType.Unknown);
    public static SharpType Void = new SharpType(BaseType.Void);
    public static SharpType Boolean = new SharpType(BaseType.Boolean);
    public static SharpType Integer = new SharpType(BaseType.Integer);
    public static SharpType String = new SharpType(BaseType.String);

    public BaseType Base { get; } = baseType;

    public string ToCppType()
    {
        return Base switch
        {
            BaseType.Void => "void",
            BaseType.Boolean => "bool",
            BaseType.Integer => "int",
            BaseType.String => "std::string",
            _ => throw new CompileException("Unknown base type.")
        };
    }
}