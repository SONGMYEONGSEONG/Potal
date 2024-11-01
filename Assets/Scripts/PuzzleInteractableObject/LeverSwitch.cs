using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverSwitch : PuzzleInteractableObject
{

    public override void InterAction()
    {

    }

    public override void Print()
    {

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ObjectPoolManager.Instance.PuzzleInteractableObjectPool.PushObject(this);
        }
        transform.position += new Vector3(0f, 1f, 0) * Time.deltaTime;
    }
}
