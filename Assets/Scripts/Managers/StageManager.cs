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

}