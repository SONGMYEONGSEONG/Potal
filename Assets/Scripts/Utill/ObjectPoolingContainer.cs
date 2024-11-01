using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectPoolingContainer<T> : MonoBehaviour where T : MonoBehaviour,IObjectPoolAble<T>
{
    private Dictionary<string, ObjectPool<T>> objectPools = new Dictionary<string, ObjectPool<T>>();
    public void Initialize(List<ObjectPoolData<T>> prefabes)
    {
        T gameObj;

        foreach (ObjectPoolData<T> prefab in prefabes)
        {
            GameObject poolContainer = new GameObject("Pool_Container_" + prefab.Type.name);

            ObjectPool<T> objectPool = new ObjectPool<T>();

            for (int i = 0; i < prefab.PoolCount; i++)
            {
                gameObj = Instantiate(prefab.Type, poolContainer.transform);
                gameObj.ReturnToPoolObject += PushObject;
                objectPool.CreateObject(gameObj);
            }

            objectPools.Add(prefab.Type.name, objectPool);
            Debug.Log(prefab.Type.name + " 오브젝트 풀링 Initalize 완료!");
        }  
    }
    
    public T PoolObject(string objName, Vector2 spawnPos)
    {
        return objectPools[objName].GetFromPool(spawnPos);
    }

    public void PushObject(T obj)
    {
        int index = obj.name.IndexOf("(Clone)");
        string objName = obj.name.Substring(0, index);

        objectPools[objName].ReturnToPool(obj);
    }

}
