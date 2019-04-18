using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _SigletonBehaviour<T> : MonoBehaviour where T: MonoBehaviour {

    public static T instance = null;

    public virtual void Awake() {

        if(instance == null) {
            instance = this as T;
            Debug.Log("Sigleton: " + typeof(T) + " has been created!");
        }
        else {
            Debug.LogError("Duplicate subclass of type " + typeof(T) + "! eliminating " + name + " while preserving " + instance.name);
            Destroy(gameObject);
        }

        DontDestroyOnLoad(instance);
        Initialize();
    }

    public virtual void Initialize() {

    }

    public virtual void OnDestroy() {

        if(instance == this) instance = null;
    }
}
