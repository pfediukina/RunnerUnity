using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BaseFactory<T> : MonoBehaviour where T: MonoBehaviour
{
    [SerializeField] protected T Prefab;

    protected ObjectPool<T> factoryObjects;
}
