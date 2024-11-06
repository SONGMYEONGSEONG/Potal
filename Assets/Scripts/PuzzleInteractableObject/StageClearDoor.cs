using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageClearDoor : PuzzleInteractableObject
{
    public Transform ClearCheckPivot;
    public float RayDistance;
    public LayerMask PlayerLayerMask;
    private Ray ray;

    public event Action OnEventStageClear;

    private void Start()
    {
        ray = new Ray(ClearCheckPivot.position, ClearCheckPivot.up);
        
    }

    private void Update()
    {
        StageClearCheck();
    }

    public void StageClearCheck()
    {
        if (Physics.Raycast(ray, out RaycastHit hit, RayDistance, PlayerLayerMask))
        {
            OnEventStageClear?.Invoke();
            //To Do Code : 스테이지 클리어 
        }
    }
}
