﻿using SharpCompiler;
using SharpCompiler.AbstractSyntaxTree;
using SharpCompiler.CodeGen;
using static SharpCompiler.Analyzer.SharpAnalyzer;
using static SharpCompiler.Parser.SharpParser;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

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
    program = Parse(File.ReadAllText(args[0]));
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

var isRunningOnWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

var gcc = new Process
{
    StartInfo = new ProcessStartInfo
    {
        FileName = isRunningOnWindows ? "g++.exe" : "g++",
        Arguments = $"{cppCodeFileName} -o {exeFileName} -w -O2",
        CreateNoWindow = true,
        RedirectStandardError = true
    }
};

gcc.Start();
gcc.WaitForExit();

if (!gcc.StandardError.EndOfStream)
{
    do
    {
        Console.Error.WriteLine(gcc.StandardError.ReadLine());
    } while (!gcc.StandardError.EndOfStream);

    Environment.Exit(1);
}