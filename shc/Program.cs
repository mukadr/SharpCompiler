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

var emitter = new SharpCompiler.CodeGen.CCodeEmitter(new StreamWriter(cCodeFileName));

var program = SharpCompiler.Parser.Parse(File.ReadAllText(args[0]));

emitter.Generate(program);

var gcc = new Process
{
    StartInfo = new ProcessStartInfo
    {
        FileName = "gcc.exe",
        Arguments = $"{cCodeFileName} -o {exeFileName}",
        CreateNoWindow = true
    }
};

gcc.Start();