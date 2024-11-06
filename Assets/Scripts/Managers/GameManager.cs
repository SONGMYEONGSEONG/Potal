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
        // �ʱ�ȭ ����
        //�÷��̾ null�̸� Resouces�� ������ �ε�
        if (playerPrefeb == null)
        {
            playerPrefeb = Resources.Load<Player>("Prefebs/Player");
        }
       
        //���ӸŴ��� player ��ü ������ ���������Ŵ��� ����
        StageManagerInit(StageNum);

        //�������� �Ŵ����κ��� �÷��̾��� ���� ��ġ�� ȣ��
        Vector3 playerPos = StageManager.Instance.PlayerSpawnPos();
        PlayerInit(playerPos);
        Debug.Log($"�÷��̾� ��ġ : {player.transform.position}");
    }

    public void StageManagerInit(int stageNum)
    {
        //ToDoCode : UI�� ���ؼ� �������� ������ ���� �����ϱ�
        StageManager.Instance.StageInitialize(stageNum);
    }

    public void PlayerInit(Vector3 spawnPos)
    {
        player = Instantiate(playerPrefeb, spawnPos,Quaternion.identity);
    }

    //Debug
    public void InitStart()
    {
        Debug.Log("���ӸŴ��� ���� ����");
        Initialize();
    }

}
