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

        //1�������� �׻� �ر� �Ǿ��־�ߵǱ⿡ 
        stageButtons[0].ButtonObject.SetActive(true);
        stageButtons[0].LockObject.SetActive(false);
    }

    private void OnEnable()
    {
        for (int i = 1; i < StageManager.Instance.StagePrefebs.Length; i++)
        {
            //�� �������� Ŭ���� ����� üũ�ϰ� �������� ��ư�� �ر���
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
