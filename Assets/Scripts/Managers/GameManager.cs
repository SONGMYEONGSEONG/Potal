using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    //Debug
    public TestObject Player;

    //Debug
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            ObjectPoolManager.Instance.TestObjPool.PoolObject("TestPrefebs",new Vector3(0,0,0));
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            ObjectPoolManager.Instance.TestObjPool.PoolObject("TestPrefebs2", new Vector3(0, 1, 0));
        }
    }


}
