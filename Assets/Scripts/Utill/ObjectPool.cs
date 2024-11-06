using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour
{
    public Queue<T> pool = new Queue<T>();
    public int PoolCount;

    //CreateObject
    public void CreateObject(T _object)
    {
        if (_object == null)
        {
            Debug.Log("해당 오브젝트는 null 입니다.");
            return;
        }

        _object.gameObject.SetActive(false);
        pool.Enqueue(_object);
    }

    //GetFromPool
    public T GetFromPool(Vector2 spawnPos)
    {
        if (IsObjectAvailable())
        {
            //TO DO CODE : Pool 확장하는 코드 추가
        }

        T _object = pool.Dequeue();
        _object.transform.position = spawnPos;
        _object.gameObject.SetActive(true);
        return _object;
    }

    //ReturnToPool
    public void ReturnToPool(T _object)
    {
        _object.gameObject.SetActive(false);
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
    }
}
