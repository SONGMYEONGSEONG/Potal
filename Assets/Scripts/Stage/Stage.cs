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
    public StageClearDoor StageClearDoor; //���߿� ������������ ��ȯ�Ұ�
    public ObjectZone[] objectZone; //��͸���Ʈ
    public OnRewardObject[] RewardObjects; //��Ϳ� �����Ѱ�� �ش� ������Ʈ�� ������


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
        //�̹� ����� �Ϸ��Ͽ����� �ٽ� �������� �ʰ� ���� ���ǹ�
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
        Debug.Log($"{gameObject.name} �������� Ŭ���� ���� ����");
        StageClearDoor.OnEventStageClear -= Clear;
        foreach (ObjectZone objectzone in objectZone)
        {
            objectzone.OnEventSucces -= Reward;
        }


        StageManager.Instance.StageClearDataSave(StageNumber);

        //Debug.Log()
        Cursor.lockState = CursorLockMode.None; // ���ӿ��� ���콺 Ŀ���� ����� �ϴ� �޼���
        SceneManager.LoadScene("_TitleScene");

    }


}
