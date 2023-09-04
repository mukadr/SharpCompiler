using System;

namespace SharpCompiler.AbstractSyntaxTree;

public class SharpType
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

    public BaseType Base { get; }

    public SharpType(BaseType baseType)
    {
        Base = baseType;
    }

    public string ToCppType()
    {
        return Base switch
        {
            BaseType.Void => "void",
            BaseType.Boolean => "bool",
            BaseType.Integer => "int",
            BaseType.String => "std::string",
            _ => throw new Exception("Unknown base type.")
        };
    }
}