// See https://aka.ms/new-console-template for more information
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;

do
{
    Console.Write("请输入表达式:");
    var command = Console.ReadLine();
    if (command == "exit") break;
    
    try
    {
        var res = await CSharpScript.EvaluateAsync(command);
        Console.WriteLine(res);
    }
    catch (CompilationErrorException e)
    {
        Console.WriteLine(string.Join(Environment.NewLine, e.Diagnostics));
    }
    
} while(true);

