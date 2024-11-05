using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IStageManager
{
    public void Initialize();
    public void Clear();
}

public class Stage : MonoBehaviour, IStageManager
{
    public int StageNumber;
    public StageClearDoor StageClearDoor; //나중에 동적생성으로 변환할것

    private void Awake()
    {
        if(StageClearDoor == null)
        {
            StageClearDoor = GetComponentInChildren<StageClearDoor>();
        }
        
    }

    private void Start()
    {
        StageClearDoor.OnEventStageClear += Clear;
    }
    public void Initialize()
    {
        throw new NotImplementedException();
    }

    public void Clear()
    {
        Debug.Log($"{gameObject.name} 스테이지 클리어 정보 받음");
        StageClearDoor.OnEventStageClear -= Clear;
    }


}
