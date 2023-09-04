using SharpCompiler;
using SharpCompiler.AbstractSyntaxTree;
using SharpCompiler.CodeGen;
using static SharpCompiler.Analyzer.AnnotateAst;
using System;
using System.Diagnostics;
using System.IO;

if (args.Length < 1)
{
    Console.WriteLine("Usage: shc <filename.shl>");
    return;
}

var cppCodeFileName = Path.ChangeExtension(args[0], ".cpp");
var exeFileName = Path.ChangeExtension(args[0], ".exe");

Node program;

try
{
    program = Parser.Parse(File.ReadAllText(args[0]));
    Analyze(program);
}
catch (CompileException ex)
{
    Console.Error.WriteLine("Compilation failed: " + ex.Message);
    Environment.Exit(1);
    return;
}

using (var writter = new StreamWriter(cppCodeFileName))
{
    var emitter = new CppCodeEmitter(writter);
    emitter.Compile(program);
}

var gcc = new Process
{
    StartInfo = new ProcessStartInfo
    {
        FileName = "g++",
        Arguments = $"{cppCodeFileName} -o {exeFileName} -Wall -O2",
        CreateNoWindow = true,
        RedirectStandardError = true
    }
};

gcc.Start();

while (!gcc.StandardError.EndOfStream)
{
    Console.Error.WriteLine(gcc.StandardError.ReadLine());
}
