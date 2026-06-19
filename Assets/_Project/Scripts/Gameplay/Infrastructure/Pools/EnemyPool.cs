using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;


public class MonoBehaviourPool<T> : IObjectPool<T> where T : MonoBehaviour, IPoolObject
{
    private readonly ObjectPool<T> _pool;
    private readonly T _prefab;

    public int CountInactive => ((IObjectPool<T>)_pool).CountInactive;

    public MonoBehaviourPool(T prefab)
    {
        _prefab = prefab;
        _pool = new ObjectPool<T>(
            createFunc: Create,
            actionOnGet: OnGet,
            actionOnRelease: OnRelease,
            actionOnDestroy: OnDestroy
            );
    }

    private T Create()
    {
        T obj = UnityEngine.Object.Instantiate(_prefab);
        obj.gameObject.SetActive(false);
        return obj;
    }

    private void OnGet(T obj)
    {
        obj.Enable();
    }

    private void OnRelease(T obj)
    {
        obj.Disable();
    }

    private void OnDestroy(T obj)
    {
        obj.Destroy();
    }

    public T Get()
    {
        return ((IObjectPool<T>)_pool).Get();
    }

    public PooledObject<T> Get(out T v)
    {
        return ((IObjectPool<T>)_pool).Get(out v);
    }

    public void Release(T element)
    {
        ((IObjectPool<T>)_pool).Release(element);
    }

    public void Clear()
    {
        ((IObjectPool<T>)_pool).Clear();
    }
}

