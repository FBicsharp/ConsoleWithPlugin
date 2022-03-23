using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using PluginBase;

namespace ConsoleWithPlugin
{
    class Program
    {
        static void Main(string[] args)
        {
            
                Console.WriteLine("Started plugin app..");
                try
                {
                    PluginLoader loader = new PluginLoader();
                    loader.LoadPlugins("C:\\Plugin");
                }
                catch (Exception e)
                {
                    Console.WriteLine(string.Format("Plugins couldn't be loaded: {0}", e.Message));
                    Console.WriteLine("Press any key to exit...");
                    Console.ReadKey();
                    Environment.Exit(0);
                }

                while (true)
                {
                    try
                    {                        
                        Console.Write("> ");
                        string line = Console.ReadLine();
                        if (line == "exit")
                        {
                            Environment.Exit(0);
                        }

                    List<Task<int>> tasklist = new List<Task<int>>() ;
                    PluginLoader.Plugins.ForEach((p) =>
                        tasklist.Add(
                            Task.Factory.StartNew(() => p.Execute())
                        )
                    );
                    Task.WaitAll(tasklist.ToArray());

                       
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(string.Format("Caught exception: {0}", e.Message));
                    }
                }
            
        }



    }







}
