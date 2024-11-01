using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum PuzzleObject
{
    LeverSwitch = 0,
    Turret = 1,
}

public class GameManager : Singleton<GameManager>
{
    //Debug
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ObjectPoolManager.Instance.PuzzleInteractableObjectPool.PoolObject(PuzzleObject.LeverSwitch, new Vector3(0, 0, 0));
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            ObjectPoolManager.Instance.PuzzleInteractableObjectPool.PoolObject(PuzzleObject.Turret, new Vector3(0, 1, 0));
        }
    }

    //Debug
    public void Print()
    {
        Debug.Log("게임매니저 생성 완료");
    }

}
