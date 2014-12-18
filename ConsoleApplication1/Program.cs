using System;
using System.Configuration;
using System.Xml;
using System.Collections;
using System.Collections.Specialized;
using CSScriptLibrary;
using csscript;
using Yko.Game;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using System.IO;
class Script
{
    static public void Main(string[] args)
    {
        var code = @"public static void Hello(string greeting)
              {
                  Console.WriteLine(greeting);
              }";

        var SayHello = new AsmHelper(CSScript.LoadMethod(code))
                                    .GetStaticMethod();

        var helper = new AsmHelper(CSScript.Load("Script\\script.ccs"));
        helper.Invoke("Script.Hello", "Hello ");

        SayHello("Hello World!");
        if (File.Exists("Script\\Dragonspawn.json"))
        {
            //StreamReader sr = new StreamReader("Script\\Dragonspawn.json");
            StreamReader sr = File.OpenText("Script\\Dragonspawn.json");
           // string t = sr.ReadToEnd();
           // Console.WriteLine(t);
            //JsonReader reader = new JsonTextReader(sr);
            JObject o = (JObject)JToken.ReadFrom(new JsonTextReader(sr));
            Console.WriteLine((string)o["Resources"]["Modle"]);
            Console.WriteLine( (string)o[0].ToString());
            //while (reader.Read())
            //{
                //if (reader.Value != null)
                //{
                //    Console.WriteLine("Token: {0}, Value: {1}", reader.TokenType, reader.Value);
                //}
                //else
                //{
                //    Console.WriteLine("Token: {0}", reader.TokenType);
                //}
            //}
        }
        System.Threading.Thread.Sleep(100000);
    }
}