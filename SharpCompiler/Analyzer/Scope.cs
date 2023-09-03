using System;
using System.Collections.Generic;
using SharpCompiler.AbstractSyntaxTree;

namespace SharpCompiler.Analyzer;

public class Scope
{
    private IDictionary<string, Variable> Variables = new Dictionary<string, Variable>();

    public Scope? PreviousScope { get; }

    public Scope(Scope? previousScope = null)
    {
        PreviousScope = previousScope;
    }

    public void Add(Variable v)
    {
        if (Variables.ContainsKey(v.Name))
        {
            throw new Exception($"Redeclared variable {v.Name}.");
        }

        Variables.Add(v.Name, v);
    }

    public Variable? GetVariable(string name)
    {
        if (Variables.ContainsKey(name))
        {
            return Variables[name];
        }

        return PreviousScope?.GetVariable(name);
    }

    public Variable MustGetVariable(string name) => GetVariable(name) ?? throw new Exception($"Undeclared variable {name}.");
}
