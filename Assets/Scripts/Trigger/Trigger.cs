using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ObjectPoolData<T>
{
    public T Type;
    public int PoolCount;
}

public class Trigger : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private TestObject testObj;

    //오브젝트 풀링이 필요한 해당 클래스의 부모나 인터페이스를 저장
    [Header("Object Pooling Prefebs List")]
    [SerializeField] private List<ObjectPoolData<TestObject>> testObjectPrefabesList;

    private void Awake()
    {
        if(testObj != null)
        {
            GameManager.Instance.Player = testObj;
            ObjectPoolManager.Instance.TestObjPool.Initialize(testObjectPrefabesList);
        } 
        else
        {
            Debug.Log($"{testObj.name}이 존재하지 않습니다.");
        }
    }
}
