// See https://aka.ms/new-console-template for more information
using DynamicScriptTest;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;

Console.Write("测试基本算数表达式:(1+2)*3/4");
var res = await CSharpScript.EvaluateAsync("(1+2)*3/4");
Console.WriteLine(res);

Console.WriteLine("测试Math函数:Sqrt(2)");
res = await CSharpScript.EvaluateAsync("Sqrt(2)", ScriptOptions.Default.WithImports("System.Math"));
Console.WriteLine(res);

Console.WriteLine(@"测试字符串函数:""Hello"".Length");
res = await CSharpScript.EvaluateAsync(@"""Hello"".Length");
Console.WriteLine(res);

Console.WriteLine(@"测试输入输出函数:Directory.GetCurrentDirectory()");
res = await CSharpScript.EvaluateAsync("Directory.GetCurrentDirectory()",
     ScriptOptions.Default.WithImports("System.IO"));
Console.WriteLine(res);

Console.WriteLine(@"测试变量:");
var student = new Student { Height = 1.75M, Weight = 75 };
//student.BMI=await CSharpScript.EvaluateAsync<Decimal>("Weight/Height/Height", globals: student);
await CSharpScript.RunAsync("BMI=Weight/Height/Height", globals: student);
Console.WriteLine(student.BMI);

Console.WriteLine(@"测试脚本编译复用:");
var scriptBMI = CSharpScript.Create<Decimal>("Weight/Height/Height", globalsType: typeof(Student));
scriptBMI.Compile();

Console.WriteLine((await scriptBMI.RunAsync(new Student { Height = 1.72M, Weight = 65 })).ReturnValue);

Console.WriteLine(@"测试脚本中定义函数:");
string script1 = "decimal Bmi(decimal w,decimal h) { return w/h/h; } return Bmi(Weight,Height);";

var result = await CSharpScript.EvaluateAsync<decimal>(script1, globals: student);
Console.WriteLine(result);

Console.WriteLine(@"测试脚本中的变量:");
var script =  CSharpScript.Create("int x=1;");
script =  script.ContinueWith("int y=1;");
script =  script.ContinueWith("return x+y;");
Console.WriteLine((await script.RunAsync()).ReturnValue);


Console.WriteLine(@"测试脚本中的变量var:");
var script2 = CSharpScript.Create("var x=1;");
script2 = script2.ContinueWith("var y=1.5;");
script2 = script2.ContinueWith("return x+y;");
Console.WriteLine((await script2.RunAsync()).ReturnValue);


Console.WriteLine(@"测试脚本中的变量数组:");
var script3 = CSharpScript.Create("int[] x = new int[7] { 1, 2, 3, 4, 5, 6, 7 }; ");
script3 = script3.ContinueWith("int y=0;");
script3 = script3.ContinueWith("for(var i=0;i<7;i++) y+=x[i] ;");
script3 = script3.ContinueWith("return y;");
Console.WriteLine((await script3.RunAsync()).ReturnValue);