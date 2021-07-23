using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonBehaviour<T> : MonoBehaviour where T : SingletonBehaviour<T>
{
    private static T s_instance;

    public static T Instance => s_instance;

    protected virtual void Awake()
    {
        if (s_instance == null)
        {
            s_instance = (T)this;
        }
        else
        {
            Debug.LogWarning("Second instance found for " + nameof(T));
            Destroy(this);
        }
    }

    protected virtual void OnDestroy()
    {
        if (s_instance == this)
            s_instance = null;
    }

}
