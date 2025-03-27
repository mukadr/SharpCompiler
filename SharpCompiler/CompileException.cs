using System;

namespace SharpCompiler;

public class CompileException(string message) : Exception(message)
{
}