using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    //Debug
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ObjectPoolManager.Instance.PuzzleInteractableObjectPool.PoolObject("LeverSwitch", new Vector3(0, 0, 0));
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            ObjectPoolManager.Instance.PuzzleInteractableObjectPool.PoolObject("Turret", new Vector3(0, 1, 0));
        }
    }

    //Debug
    public void Print()
    {
        Debug.Log("게임매니저 생성 완료");
    }

}
