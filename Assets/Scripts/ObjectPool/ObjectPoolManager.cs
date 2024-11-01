using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : Singleton<ObjectPoolManager>
{
    private ObjectPoolingContainer<TestObject> testObjPool = new ObjectPoolingContainer<TestObject>();
    public ObjectPoolingContainer<TestObject> TestObjPool { get => testObjPool; }


}
