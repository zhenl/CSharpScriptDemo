//dotnet add  package Jint --prerelease

using Jint;
using System;

var e=new Engine();

Console.WriteLine("直接计算表达式：（1+2）*3");
var r1= e.Evaluate("(1+2)*3");
Console.WriteLine(r1);

Console.WriteLine("-----------------------");

Console.WriteLine("字符串操作：'abc'.length");
var r2=e.Evaluate("'abc'.length");
Console.WriteLine(r2);

Console.WriteLine("-----------------------");
Console.WriteLine("字符串操作：'abc'.substr(2)");
var r3=e.Evaluate("'abc'.substr(2)");
Console.WriteLine(r3);

Console.WriteLine("-----------------------");
Console.WriteLine("使用SetValue赋值给JS变量");
var e2 = new Engine()
    .SetValue("x", 1)
    .SetValue("y",2);
var r4=e2.Evaluate("x+y");
Console.WriteLine(r4);

Console.WriteLine("-----------------------");
Console.WriteLine("不能通过值变量传递数据");
var val=3;
var e3 = new Engine()
        .SetValue("v",val)
        .Execute("v=5");
Console.WriteLine(e3.Evaluate("v"));
Console.WriteLine(val);

Console.WriteLine("-----------------------");
Console.WriteLine("可以使用对象进行数据交换：张三变李四");
var myobj= new Student {
    Name="张三"
};
Console.WriteLine(myobj.Name);
var e4 = new Engine()
    .SetValue("student", myobj)
    .Execute("student.Name = '李四'");
Console.WriteLine(myobj.Name);

Console.WriteLine("-----------------------");
Console.Write("将CSharp函数设置给Js引擎");
var engine = new Engine()
    .SetValue("log", new Action<object>(Console.WriteLine));
 engine.Execute(@"
    function hello() { 
        log('Hello World');
    };
 
    hello();
");

Console.WriteLine("-----------------------");
Console.WriteLine("调用JS函数计算BMI");
var e5=new Engine()
    .Execute("function bmi(weight, height) { return weight/height/height; }");
Console.WriteLine(e5.Invoke("bmi",75,1.75));

Console.WriteLine("-----------------------");
Console.WriteLine("调用.Net函数写入文件");
var e6 = new Engine(cfg => cfg.AllowClr());
e6.Execute(@"var f=System.IO.StreamWriter('sayhello.log');
        f.WriteLine('你好 !');
        f.Dispose();");

//Console.WriteLine("-----------------------");
//Console.WriteLine("创建CSharp类");
//var e7= new Engine(cfg => cfg.AllowClr());
//e7.SetValue("Student", Jint.Runtime.Interop.TypedReference.CreateTypedReference(e7, typeof(Student)));
//e7.Execute(@"var s=new Student();
 //           s.Name='张三';");
//var s=e7.GetValue("s") as Student;
//Console.WriteLine(s.Name);