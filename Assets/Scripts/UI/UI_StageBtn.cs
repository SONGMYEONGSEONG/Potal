using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_StageBtn : MonoBehaviour
{
    public int stageNum;
    public GameObject ButtonObject; //버튼 오브젝트 : 스테이지 플레이 가능시 해당 오브젝트 활성화
    public GameObject LockObject; //락 오브젝트 : 스테이지 플레이 불가능시 해당 오브젝트 활성화

    public void OnClickBtn()
    {
        GameManager.Instance.StageNum = stageNum;
        SceneManager.LoadScene("SampleScene");
    }
}
