using System;
using System.Collections.Concurrent;

namespace Double_Click_Test.Core.Helpers;

public static class Singleton<T>
    where T : new()
{
    private static readonly ConcurrentDictionary<Type, T> _instances = new();

    public static T Instance
    {
        get
        {
            return _instances.GetOrAdd(typeof(T), (t) => new T());
        }
    }
}
