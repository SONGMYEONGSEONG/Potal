using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct RewardObject
{
    public Transform transform;
    public Vector3 position;
}

public class Stage : MonoBehaviour, IStageManager
{
    public Vector3 PlayerStartPos;
    public int StageNumber;
    public StageClearDoor StageClearDoor; //나중에 동적생성으로 변환할것
    public ObjectZone[] objectZone; //기믹리스트
    //public List<RewardObject> RewardObjects; //기믹에 성공한경우 해당 오브젝트가 움직임
    public List<GameObject> RewardObjects; //기믹에 성공한경우 해당 오브젝트가 움직임

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

        int index = 0;
        foreach(ObjectZone objectzone in objectZone)
        {
            objectzone.Index = index;
            index++;
            objectzone.OnEventSucces += Reward;
        }
       
    }

    private void Reward(int index)
    {
        RewardObjects[index].SetActive(true);

        //StartCoroutine(MoveObject(RewardObjects[index].transform, RewardObjects[index].transform.position, RewardObjects[index].position));

        //Debug
        //StartCoroutine(MoveObject(RewardObject.position , RewardObject.position + new Vector3(0,0,-10)));    
    }

    IEnumerator MoveObject(Transform RewardObject, Vector3 startPos , Vector3 endPos)
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
        
    }

    public void Clear()
    {
        Debug.Log($"{gameObject.name} 스테이지 클리어 정보 받음");
        StageClearDoor.OnEventStageClear -= Clear;
        foreach (ObjectZone objectzone in objectZone)
        {
            objectzone.OnEventSucces -= Reward;
        }

        StageManager.Instance.StageClearDataSave(StageNumber);
    }


}
