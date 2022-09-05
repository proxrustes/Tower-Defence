using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Singleton
{
    static Dictionary<Type, object> objects_by_type = new Dictionary<Type, object>();
    static Dictionary<string, object> objects_by_name = new Dictionary<string, object>();

    public static void AddObject<T>(T obj)
    {
        objects_by_type.Add(typeof(T), obj);
    }

    public static T GetObject<T>()
    {
        return (T)objects_by_type[typeof(T)];
    }

    public static void AddObject<T>(string name, T obj)
    {
        objects_by_name.Add(name, obj);
    }
    
    public static T GetObject<T>(string name)
    {
        return (T)objects_by_name[name];
    }
}