using System;

namespace SharpCompiler;

public class CompileException : Exception
{
    public CompileException(string message)
        : base(message)
    { }
}