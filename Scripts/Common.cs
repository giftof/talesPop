using UnityEngine;
using System;
using System.Linq;
using System.Reflection;


    public static class Common
    {
        public const string TP_ITEMS = "TalesPop.Items";

        public static void ForceQuit()
        {
    #if UNITY_EDITOR
            Debug.LogWarning("call quit on UNITY_EDITOR");
            UnityEditor.EditorApplication.isPlaying = false;
    #else
            Debug.LogWarning("call quit on not UNITY_EDITOR");
            Application.Quit();
    #endif
        }

        public static T StringToEnum<T>(string from) where T : struct
        {
            string name = Enum.GetNames(typeof(T)).FirstOrDefault(e => e.Equals(from));
            T result = default;

            if (name != null)
                result = (T)Enum.Parse(typeof(T), name);
            else
                ForceQuit();

            return result;
        }

        private static AssemblyName lastAssemblyName;

        public static bool GetTypeFromEnumName(string typeName, out Type type)
        {
            type = Type.GetType(typeName) ?? FindFromAssembly(lastAssemblyName, typeName);

            if (type != null)
                return true;

            Assembly currentAssembly = Assembly.GetExecutingAssembly();
            AssemblyName[] referencedAssemblieArray = currentAssembly.GetReferencedAssemblies();

            foreach (AssemblyName assemblyName in referencedAssemblieArray)
            {
                if (assemblyName.Equals(lastAssemblyName))
                    continue;

                type = FindFromAssembly(assemblyName, typeName);

                if (type != null)
                {
                    lastAssemblyName = assemblyName;
                    return true;
                }
            }

            return false;
        }

        private static Type FindFromAssembly(AssemblyName assemblyName, string typeName)
        {
            Assembly assembly = Assembly.Load(lastAssemblyName);
            Type type = null;

            if (assembly != null)
            {
                type = assembly.GetType(typeName);
                if (type != null)
                {
                    lastAssemblyName = assemblyName;
                }
            }

            return type;
        }

        public static void Swap<T>(ref T a, ref T b)
        {
            T temp = a;
            a = b;
            b = temp;
        }

    }

