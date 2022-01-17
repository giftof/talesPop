using UnityEngine;
using System;
using System.Linq;
using System.Reflection;



public static class Common
{
    /*
     * enum names MUST SAME with class(type) name
     * enum value MUST MATCH with slotType value
     */
    public enum ItemType
    {
        Weapon = 0x0001,
        Shield = 0x0002,
        TwoHand = 0x0003,
        Armor = 0x0004,
        Helmet = 0x0008,
        Amulet = 0x0010,

        Material = 0x0100,
        Potion = 0x0200,
        Bag = 0x1FFF,
    }

    public const string TALESPOP_ITEMS = "TalesPop.Items";
    public const int NULL_ID = -1;
    public const string EMPTY_STRING = "";
    public const ItemType ITEM_EQUIP_CAP = ItemType.Amulet;
    public const ItemType ITEM_LAST_ENUM = ItemType.Bag;

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

    /*
    private static AssemblyName lastAssemblyName;

    public static bool GetTypeFromEnumName(string typeName, out Type type)
    {
        type = Type.GetType(typeName);
        //return type != null;
        if (type != null)
        {
            return true;
        }

        type = FindFromAssembly(lastAssemblyName, typeName);

        if (type != null)
        {
            return true;
        }

        Assembly currentAssembly = Assembly.GetExecutingAssembly();
        AssemblyName[] referencedAssemblieArray = currentAssembly.GetReferencedAssemblies();

        Debug.Log($"assemblyNameArraySize = {referencedAssemblieArray.Length}");

        foreach (AssemblyName assemblyName in referencedAssemblieArray)
        {
            //if (assemblyName.Equals(lastAssemblyName))
            //    continue;

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
        if (assemblyName == null)
            return null;

        Debug.Log($"assemblyName = {assemblyName}");

        Assembly assembly = Assembly.Load(assemblyName);
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
    */

    /*
    public static void Swap<T>(ref T a, ref T b)
    {
        T temp = a;
        a = b;
        b = temp;
    }
    */

    public static bool IsSame<T>(T a, T b)
    {
        return a?.Equals(b) ?? false;
    }
}
