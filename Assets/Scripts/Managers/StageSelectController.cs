using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class StageSelectController : MonoBehaviour
{
    public Transform StageSelectSlot;
    private UI_StageBtn stageButtonPrefab;
    private List<UI_StageBtn> stageButtons = new List<UI_StageBtn>();

    private void Awake()
    {
        stageButtonPrefab = Resources.Load<UI_StageBtn>("Prefebs/UI/StageBtn");

        for (int i = 0; i < StageManager.Instance.StagePrefebs.Length; i++)
        {
            stageButtons.Add(Instantiate(stageButtonPrefab, StageSelectSlot));
            stageButtons[i].stageNum = i + 1;
            stageButtons[i].StageName.text = "Stage" + (i + 1).ToString();

        }

        //1스테이지 항상 해금 되어있어야되기에 
        stageButtons[0].ButtonObject.SetActive(true);
        stageButtons[0].LockObject.SetActive(false);
    }

    private void OnEnable()
    {
        for (int i = 1; i < StageManager.Instance.StagePrefebs.Length; i++)
        {
            //전 스테이지 클리어 기록을 체크하고 스테이지 버튼을 해금함
            if(StageManager.Instance.StageClearDataLoad(i))
            {
                stageButtons[i].ButtonObject.SetActive(true);
                stageButtons[i].LockObject.SetActive(false);
            }
        }
    }

    public void OnClickCancle()
    {
        gameObject.SetActive(false);
    }
}
