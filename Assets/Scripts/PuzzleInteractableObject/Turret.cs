using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Turret : PuzzleInteractableObject
{
    public override void InterAction()
    {

    }

    public override void Print() 
    {

    }

    private void Update()
    {
        transform.position += new Vector3(1f, 0, 0) * Time.deltaTime;
    }
}
