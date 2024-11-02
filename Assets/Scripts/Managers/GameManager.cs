using UnityEngine;

public enum PuzzleObject
{
    LeverSwitch = 0,
    Turret = 1,
}

public class GameManager : Singleton<GameManager>
{
    //Debug
    private Player player;
    public Player Player
    {
        get { return player; }
        set { player = value; }
    }
    protected override void Awake()
    {
        base.Awake();
    }


    //Debug
    public void Print()
    {
        Debug.Log("게임매니저 생성 완료");
    }

}
