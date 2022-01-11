using UnityEngine;
using System;
using System.Linq;
using Newtonsoft.Json.Linq;



public static class Common
{
    public static T StringToEnum<T>(string from) where T : struct
    {
        var name = Enum.GetNames(typeof(T)).FirstOrDefault(e => e.Equals(from));
        T result = default;

        if (name != null)
            result = (T)Enum.Parse(typeof(T), name);
        else
            Assert();

        return result;
    }

    public static void Assert()
    {
#if UNITY_EDITOR
        Debug.LogWarning("call quit on UNITY_EDITOR");
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Debug.LogWarning("call quit on not UNITY_EDITOR");
        Application.Quit();
#endif
    }
}
