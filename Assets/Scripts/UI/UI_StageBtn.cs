using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_StageBtn : MonoBehaviour
{
    public int stageNum;
    public GameObject ButtonObject; //��ư ������Ʈ : �������� �÷��� ���ɽ� �ش� ������Ʈ Ȱ��ȭ
    public GameObject LockObject; //�� ������Ʈ : �������� �÷��� �Ұ��ɽ� �ش� ������Ʈ Ȱ��ȭ

    public void OnClickBtn()
    {
        GameManager.Instance.StageNum = stageNum;
        SceneManager.LoadScene("SampleScene");
    }
}
