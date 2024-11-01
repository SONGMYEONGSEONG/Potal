using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Trigger : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private TestObject testObj;

    //������Ʈ Ǯ���� �ʿ��� �ش� Ŭ������ �θ� �������̽��� ����
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
            Debug.Log($"{testObj.name}�� �������� �ʽ��ϴ�.");
        }
    }
}
