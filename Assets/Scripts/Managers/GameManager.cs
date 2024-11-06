using UnityEngine;
using UnityEngine.SceneManagement;
public enum PuzzleObject
{
    LeverSwitch = 0,
    Turret = 1,
}

public class GameManager : Singleton<GameManager>
{
    private Player playerPrefeb;
    private Player player;
    public Player Player
    {
        get { return player; }
        set { player = value; }
    }

    public int StageNum = 1;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Initialize()
    {
        // 초기화 로직
        //플레이어가 null이면 Resouces로 프리팹 로드
        if (playerPrefeb == null)
        {
            playerPrefeb = Resources.Load<Player>("Prefebs/Player");
        }
       
        //게임매니저 player 객체 생성후 스테이지매니저 생성
        StageManagerInit(StageNum);

        //스테이지 매니저로부터 플레이어의 시작 위치를 호출
        Vector3 playerPos = StageManager.Instance.PlayerSpawnPos();
        PlayerInit(playerPos);
        Debug.Log($"플레이어 위치 : {player.transform.position}");
    }

    public void StageManagerInit(int stageNum)
    {
        //ToDoCode : UI를 통해서 스테이지 선택후 퍼즐 입장하기
        StageManager.Instance.StageInitialize(stageNum);
    }

    public void PlayerInit(Vector3 spawnPos)
    {
        player = Instantiate(playerPrefeb, spawnPos,Quaternion.identity);
    }

    //Debug
    public void InitStart()
    {
        Debug.Log("게임매니저 생성 시작");
        Initialize();
    }

}
