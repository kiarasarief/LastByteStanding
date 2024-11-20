using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using System.Runtime.Loader;

public class CodeCompiler
{
    public static (bool Success, string Output) CompileAndRun(string code, string methodName, string input)
    {
        try
        {
            // Full solution template
            string fullCode = $@"
using System;
using System.Linq;
using System.Collections.Generic;

public class Solution {{
    {code}
}}

public class Runner {{
    public static string RunMethod(string input) {{
        var solution = new Solution();
        var method = typeof(Solution).GetMethods()
            .FirstOrDefault(m => m.Name == ""{methodName}"");
        
        // Parse input - this is a basic implementation and might need expansion
        var parsedInput = ParseInput(input);
        
        var result = method.Invoke(solution, parsedInput);
        return result?.ToString() ?? string.Empty;
    }}

    private static object[] ParseInput(string input) {{
        // Basic input parsing - can be expanded for more complex scenarios
        if (input.Contains(""[""))
        {{
            var cleanInput = input.Trim('[', ']');
            var parts = cleanInput.Split(',').Select(p => p.Trim()).ToArray();
            
            // Try converting to int array first
            if (int.TryParse(parts[0], out _))
            {{
                return new object[] {{ parts.Select(int.Parse).ToArray() }};
            }}
            
            // If not integers, return as is
            return new object[] {{ parts }};
        }}
        return new object[] {{ input }};
    }}
}}";

            // Compile the code
            var compilation = CSharpCompilation.Create("DynamicAssembly")
                .WithOptions(new CSharpCompilationOptions(OutputKind.DynamicLibrary))
                .AddReferences(
                    MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                    MetadataReference.CreateFromFile(typeof(Enumerable).Assembly.Location)
                )
                .AddSyntaxTrees(CSharpSyntaxTree.ParseText(fullCode));

            using (var ms = new MemoryStream())
            {
                EmitResult result = compilation.Emit(ms);

                if (result.Success)
                {
                    ms.Seek(0, SeekOrigin.Begin);
                    var assembly = AssemblyLoadContext.Default.LoadFromStream(ms);
                    var type = assembly.GetType("Runner");
                    var method = type.GetMethod("RunMethod");

                    var output = method.Invoke(null, new object[] { input }) as string;
                    return (true, output);
                }
                else
                {
                    var errors = result.Diagnostics
                        .Where(d => d.Severity == DiagnosticSeverity.Error)
                        .Select(d => d.ToString());
                    return (false, string.Join(Environment.NewLine, errors));
                }
            }
        }
        catch (Exception ex)
        {
            return (false, $"Execution Error: {ex.Message}");
        }
    }
}