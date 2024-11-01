using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManger : Singleton<ObjectPoolManger>
{
    public ObjectPools<TestObject> testObjpool;
    //public ObjectPools<TestObject1> testObj1pool;
    //public ObjectPools<TestObject2> testObj2pool;
}
