using System;
using System.Collections.Generic;
using UnityEngine;

public class ServicesLocator : MonoBehaviour
{
    private static readonly Dictionary<Type, object> service = new Dictionary<Type, object>();

    public static void Register<TInterface>(TInterface serviceInstance)
    {
        Type interfaceType = typeof(TInterface);
        service[interfaceType] = serviceInstance;
        Debug.Log($"Service {interfaceType.Name} enregistré");
    }

    public static TInterface Get<TInterface>()
    {
        Type interfaceType = typeof(TInterface);

        if (service.ContainsKey(interfaceType))
        {
            return (TInterface)service[interfaceType];
        }

        Debug.LogError($"Service {interfaceType.Name} non trouvé");
        return default(TInterface);
    }

    public static void Unregister<TInterface>()
    {
        bool interfaceRemoved = service.Remove(typeof(TInterface));

        if(interfaceRemoved)
        {
            Debug.Log($"Service {typeof(TInterface).Name} Suprimé");
        }
        else
        {
            Debug.LogWarning($"Service {typeof(TInterface).Name} n'etais pas enregistré");
        }

    }
}
