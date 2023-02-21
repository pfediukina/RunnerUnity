using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BaseFactory<T> : MonoBehaviour where T: MonoBehaviour
{
    [SerializeField] protected T Prefab;
    
    protected ObjectPool<T> factoryObjects;

    protected ObjectPool<T> InitPool(int count, Transform parent)
    {
        ObjectPool<T> list = new ObjectPool<T>(createFunc: () =>
            Instantiate(Prefab, parent),
            actionOnGet: (obj) => obj.gameObject.SetActive(true), 
            actionOnRelease: (obj) => obj.gameObject.SetActive(false), 
            actionOnDestroy: (obj) => Destroy(obj), 
            collectionCheck: false,
            maxSize: count);

        return list;
    }
}
