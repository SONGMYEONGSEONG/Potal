using UnityEngine;
using UnityEngine.SceneManagement;
public enum PuzzleObject
{
    LeverSwitch = 0,
    Turret = 1,
}

public class GameManager : Singleton<GameManager>
{
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

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // ���� �ε�� ������ ȣ��Ǵ� �ڵ�
        Debug.Log("���� �ε�Ǿ����ϴ�!");
        Initialize();
    }

    private void Initialize()
    {
        // �ʱ�ȭ ����
        //�÷��̾ null�̸� Resouces�� ������ �ε�
        if (player == null)
        {
            player = Resources.Load<Player>("Prefebs/Player");
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
        Instantiate(player,spawnPos,Quaternion.identity);
    }

    //Debug
    public void InitStart()
    {
        Debug.Log("���ӸŴ��� ���� ����");
        Initialize();
    }



    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            StageNum++;
            SceneManager.LoadScene("SampleScene");
        }
    }
}
