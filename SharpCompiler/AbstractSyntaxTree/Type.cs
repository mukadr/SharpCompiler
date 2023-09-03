using System;

namespace SharpCompiler.AbstractSyntaxTree;

public class Type
{
    public enum BaseType
    {
        Unknown,
        Void,
        Integer,
        String
    }

    public static Type Unknown = new Type(BaseType.Unknown);
    public static Type Void = new Type(BaseType.Void);
    public static Type Integer = new Type(BaseType.Integer);
    public static Type String = new Type(BaseType.String);

    public BaseType Base { get; }

    public Type(BaseType baseType)
    {
        Base = baseType;
    }

    public string ToCType()
    {
        return Base switch
        {
            BaseType.Void => "void",
            BaseType.Integer => "int",
            BaseType.String => "char *",
            _ => throw new Exception("Unknown C type.")
        };
    }
}