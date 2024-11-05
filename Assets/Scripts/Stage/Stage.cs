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
    public StageClearDoor StageClearDoor; //���߿� ������������ ��ȯ�Ұ�

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
        Debug.Log($"{gameObject.name} �������� Ŭ���� ���� ����");
        StageClearDoor.OnEventStageClear -= Clear;
    }


}
