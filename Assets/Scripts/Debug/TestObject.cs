using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestObject : MonoBehaviour,IObjectPoolAble<TestObject>
{
    public event Action<TestObject> ReturnToPoolObject;

    private void Awake()
    {
        GameManager.Instance.TestObj = this;
    }

    public void Print()
    {
        Debug.Log("���ӸŴ��� ���� ����");
    }
}
