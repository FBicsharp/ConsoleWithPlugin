using PluginBase;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleWithPlugin
{
    public class PluginLoader
    {
        public static List<ICommand> Plugins { get; set; }

        public void LoadPlugins(string folder)
        {
            Plugins = new List<ICommand>();

            //Load the DLLs from the Plugins directory
            if (Directory.Exists(folder))
            {
                string[] files = Directory.GetFiles(folder);
                foreach (string file in files)
                {
                    if (file.EndsWith(".dll"))
                    {
                        Assembly.LoadFile(Path.GetFullPath(file));
                    }
                }
            }

            Type interfaceType = typeof(ICommand);
            //Fetch all types that implement the interface ICommand and are a class
            Type[] types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(p => interfaceType.IsAssignableFrom(p) && p.IsClass)
                .ToArray();
            foreach (Type type in types)
            {
                //Create a new instance of all found types
                Plugins.Add((ICommand)Activator.CreateInstance(type));
            }
        }
    }
}
