using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public struct RewardObject
{
    public Transform transform;
    public Vector3 position;
}


[System.Serializable]
public class OnRewardObject
{
    public bool isReward;
    public GameObject[] RewardObject;
}


public class Stage : MonoBehaviour, IStageManager
{
    public Vector3 PlayerStartPos;
    public int StageNumber;
    public StageClearDoor StageClearDoor; //나중에 동적생성으로 변환할것
    public ObjectZone[] objectZone; //기믹리스트
    public OnRewardObject[] RewardObjects; //기믹에 성공한경우 해당 오브젝트가 움직임


    private void Awake()
    {
        if (StageClearDoor == null)
        {
            StageClearDoor = GetComponentInChildren<StageClearDoor>();
        }
    }

    private void Start()
    {
        StageClearDoor.OnEventStageClear += Clear;

        int index = 0;
        foreach (ObjectZone objectzone in objectZone)
        {
            objectzone.Index = index;
            index++;
            objectzone.OnEventSucces += Reward;
        }

    }

    private void Reward(int index)
    {
        //이미 기믹을 완료하였으면 다시 동작하지 않게 막는 조건문
        if (!RewardObjects[index].isReward)
        {
            for (int i = 0; i < RewardObjects[index].RewardObject.Length; i++)
            {
                if (RewardObjects[index].RewardObject[i].activeSelf)
                {
                    RewardObjects[index].RewardObject[i].SetActive(false);
                }
                else
                {
                    RewardObjects[index].RewardObject[i].SetActive(true);
                }
                

            }

            RewardObjects[index].isReward = true;
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

        //Debug.Log()
        Cursor.lockState = CursorLockMode.None; // 게임에서 마우스 커서를 숨기게 하는 메서드
        SceneManager.LoadScene("_TitleScene");

    }


}
