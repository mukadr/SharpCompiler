using System;
using System.Diagnostics;
using System.IO;

if (args.Length < 1)
{
    Console.WriteLine("Usage: shc <filename.shl>");
    return;
}

var cCodeFileName = Path.ChangeExtension(args[0], ".c");
var exeFileName = Path.ChangeExtension(args[0], ".exe");

var program = SharpCompiler.Parser.Parse(File.ReadAllText(args[0]));

using (var writter = new StreamWriter(cCodeFileName))
{
    var emitter = new SharpCompiler.CodeGen.CCodeEmitter(writter);
    emitter.Generate(program);
}

var gcc = new Process
{
    StartInfo = new ProcessStartInfo
    {
        FileName = "gcc.exe",
        Arguments = $"{cCodeFileName} -o {exeFileName}",
        CreateNoWindow = true,
        RedirectStandardError = true
    }
};

gcc.Start();

while (!gcc.StandardError.EndOfStream)
{
    Console.Error.WriteLine(gcc.StandardError.ReadLine());
}
