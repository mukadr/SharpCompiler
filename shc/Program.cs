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

var program = SharpCompiler.Parser.Parse(File.ReadAllText(args[0]));

using (var writter = new StreamWriter(cppCodeFileName))
{
    var emitter = new SharpCompiler.CodeGen.CppCodeEmitter(writter);
    emitter.Compile(program);
}

var gcc = new Process
{
    StartInfo = new ProcessStartInfo
    {
        FileName = "g++.exe",
        Arguments = $"{cppCodeFileName} -o {exeFileName}",
        CreateNoWindow = true,
        RedirectStandardError = true
    }
};

gcc.Start();

while (!gcc.StandardError.EndOfStream)
{
    Console.Error.WriteLine(gcc.StandardError.ReadLine());
}
