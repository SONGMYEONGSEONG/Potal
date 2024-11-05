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
    private ObjectZone objectZone; //기믹리스트
    public Transform RewardObject; //기믹에 성공한경우 해당 오브젝트가 움직임

    private void Awake()
    {
        if(StageClearDoor == null)
        {
            StageClearDoor = GetComponentInChildren<StageClearDoor>();
        }
        if(objectZone == null)
        {
            objectZone = GetComponentInChildren<ObjectZone>();
        }
        
    }

    private void Start()
    {
        StageClearDoor.OnEventStageClear += Clear;
        objectZone.OnEventSucces += Reward;
    }

    private void Reward()
    {
        //Debug
        StartCoroutine(MoveObject(RewardObject.position , RewardObject.position + new Vector3(0,0,-10)));    
    }

    IEnumerator MoveObject(Vector3 startPos , Vector3 endPos)
    {
        float timer = 0;
        while (Vector3.Distance(RewardObject.position, endPos) >= 0.1f)
        {
            RewardObject.position = Vector3.Lerp(startPos, endPos, timer);
            yield return new WaitForSeconds(0.1f);
            timer += 0.1f;
        }
       
    }

    public void Initialize()
    {
        throw new NotImplementedException();
    }

    public void Clear()
    {
        Debug.Log($"{gameObject.name} 스테이지 클리어 정보 받음");
        StageClearDoor.OnEventStageClear -= Clear;
        objectZone.OnEventSucces -= Reward;
    }


}
