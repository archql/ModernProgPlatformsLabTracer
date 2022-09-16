using System.Reflection;

namespace lab1Tracer.Serialization
{
    using lab1Tracer.Serialization.Abstractions;
    public class Loader
    {
        public static bool LoadSerializersFromPath(string path, ref List<ITraceResultSerializer> results)
        {
            DirectoryInfo Directory = new DirectoryInfo(path);
            if (!Directory.Exists)
            {
                return false; // failed to open dir
            }

            var files = Directory.GetFiles("*.dll");
            if (files == null)
            {
                return false; // failed to found any dll
            }

            foreach (var fi in files)
            {
                LoadPluginFromAssembly(fi.FullName, ref results);
            }

            return results.Count > 0;
        }

        private static void LoadPluginFromAssembly(string Filename, ref List<ITraceResultSerializer> results)
        {
            Type pluginType = typeof(ITraceResultSerializer);

            Assembly assembly = Assembly.LoadFrom(Filename);
            if (assembly == null)
            {
                return;
            }

            Type[] types = assembly.GetExportedTypes();
            foreach (Type t in types)
            {
                if (!t.IsClass || t.IsNotPublic)
                {
                    continue;
                }

                if (pluginType.IsAssignableFrom(t))
                {
                    ITraceResultSerializer ?plugin = Activator.CreateInstance(t) as ITraceResultSerializer;
                    if (plugin != null)
                    {
                        results.Add(plugin);
                    }
                }
            }
        }
    }
}