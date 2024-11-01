using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public interface IObjectPoolAble<T>
{
    public event Action<T> ReturnToPoolObject;
}

public class ObjectPool<T> where T : MonoBehaviour
{
    public Queue<T> pool;
    public int PoolCount;

    //InitializePool
    public void InitializePool()
    {
        pool = new Queue<T>();
    }

    //CreateObject
    public void CreateObject(T _object)
    {
        if (_object == null)
        {
            Debug.Log("�ش� ������Ʈ�� null �Դϴ�.");
            return;
        }

        pool.Enqueue(_object);
    }

    //GetFromPool
    public T GetFromPool()
    {
        if (IsObjectAvailable())
        {
            //TO DO CODE : Pool Ȯ���ϴ� �ڵ� �߰�
        }

        T _object = pool.Dequeue();
        return _object;
    }

    //ReturnToPool
    public void ReturnToPool(T _object)
    {
        pool.Enqueue(_object);
    }

    //IsObjectAvailable
    public bool IsObjectAvailable()
    {
        if (pool.Count <= 0)
        {
            return false;
        }

        return true;
    }

    //ClearPool
    public void ClearPool()
    {
        pool.Clear();

        //for (int i = 0; i < pool.Count; i++)
        //{
        //    pool.Dequeue();
        //}
    }
}
