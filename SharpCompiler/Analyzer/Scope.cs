using System;
using System.Collections.Generic;
using SharpCompiler.AbstractSyntaxTree;

namespace SharpCompiler.Analyzer;

public class Scope(Scope? previousScope = null)
{
    private IDictionary<string, Variable> Variables = new Dictionary<string, Variable>();

    public Scope? PreviousScope { get; } = previousScope;

    public void Add(Variable v)
    {
        if (!Variables.TryAdd(v.Name, v))
        {
            throw new CompileException($"Redeclared variable {v.Name}.");
        }
    }

    public Variable? GetVariable(string name)
    {
        if (Variables.TryGetValue(name, out var variable))
        {
            return variable;
        }

        return PreviousScope?.GetVariable(name);
    }

    public Variable MustGetVariable(string name) => GetVariable(name) ?? throw new CompileException($"Undeclared variable {name}.");
}
