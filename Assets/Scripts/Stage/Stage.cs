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
    public StageClearDoor StageClearDoor; //���߿� ������������ ��ȯ�Ұ�
    public ObjectZone[] objectZone; //��͸���Ʈ
    //public List<RewardObject> RewardObjects; //��Ϳ� �����Ѱ�� �ش� ������Ʈ�� ������
    public List<GameObject> RewardObjects; //��Ϳ� �����Ѱ�� �ش� ������Ʈ�� ������

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
        Debug.Log($"{gameObject.name} �������� Ŭ���� ���� ����");
        StageClearDoor.OnEventStageClear -= Clear;
        foreach (ObjectZone objectzone in objectZone)
        {
            objectzone.OnEventSucces -= Reward;
        }

        StageManager.Instance.StageClearDataSave(StageNumber);
    }


}
