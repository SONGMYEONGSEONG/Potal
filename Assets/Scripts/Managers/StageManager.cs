using System;
using System.Collections.Generic;
using UnityEngine;

public interface IStageManager
{
    public void Initialize();
    public void Clear();
}

public class StageManager : Singleton<StageManager>
{
    public IStageManager[] StagePrefebs;
    private Stage curStage;
    public int curStageNum = 0;

    protected override void Awake()
    {
        base.Awake();
        StagePrefebs = Resources.LoadAll<Stage>("prefebs/Stage");
<<<<<<< Updated upstream
=======

        for (int i = 0; i < StagePrefebs.Length; i++)
        {
            strBuilder.Clear();
            strBuilder.Append("Stage");
            strBuilder.Append((i + 1).ToString());

            if (PlayerPrefs.GetInt(strBuilder.ToString(), 0) > 0)
            {
                IsStageClear.Add(true);
            }
            else
            {
                IsStageClear.Add(false);
            }
        }

>>>>>>> Stashed changes
    }

    public void StageInitialize(int stageNum)
    {
        curStageNum = stageNum - 1;
        curStage = StagePrefebs[curStageNum] as Stage;
        Instantiate(curStage);
    }

    public Vector3 PlayerSpawnPos()
    {
        return curStage.PlayerStartPos;
    }

<<<<<<< Updated upstream
=======
    public void StageClearDataSave(int clearStageNum)
    {
        strBuilder.Clear();
        strBuilder.Append("Stage");
        strBuilder.Append((clearStageNum).ToString());

        PlayerPrefs.SetInt(strBuilder.ToString(), 1);
        IsStageClear[clearStageNum - 1] = true;
    }

    public bool StageClearDataLoad(int clearStageNum)
    {
        return IsStageClear[clearStageNum - 1];
    }
>>>>>>> Stashed changes
}