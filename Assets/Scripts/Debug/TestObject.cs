using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestObject : MonoBehaviour,IObjectPoolAble<TestObject>
{
    public event Action<TestObject> ReturnToPoolObject;

    public void Print()
    {
        Debug.Log("���ӸŴ��� ���� ����");
    }
}
