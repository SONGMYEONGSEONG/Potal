using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelectController : MonoBehaviour
{
    public Transform StageSelectSlot;
    private UI_StageBtn stageButtonPrefab;
    private List<UI_StageBtn> stageButtons = new List<UI_StageBtn>();

    private void Start()
    {
        stageButtonPrefab = Resources.Load<UI_StageBtn>("Prefebs/UI/StageBtn");

        for (int i = 0; i < StageManager.Instance.StagePrefebs.Length; i++)
        {
            stageButtons.Add(Instantiate(stageButtonPrefab, StageSelectSlot));
            stageButtons[i].stageNum = i + 1;
        }

        //1스테이지 항상 해금 되어있어야되기에 
        stageButtons[0].ButtonObject.SetActive(true);
        stageButtons[0].LockObject.SetActive(false);
    }

    private void OnEnable()
    {
        for (int i = 1; i < StageManager.Instance.StagePrefebs.Length; i++)
        {
            if(StageManager.Instance.StageClearDataLoad(i))
            {
                stageButtons[i + 1].ButtonObject.SetActive(true);
                stageButtons[i + 1].LockObject.SetActive(false);
            }
        }
    }

    public void OnClickCancle()
    {
        gameObject.SetActive(false);
    }
}
