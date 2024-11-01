using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestObject : MonoBehaviour,IObjectPoolAble<TestObject>
{
    public event Action<TestObject> ReturnToPoolObject;

    public void Print()
    {
        Debug.Log("게임매니저 정상 동작");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ObjectPoolManager.Instance.TestObjPool.PushObject(this);
        }
    }
}
