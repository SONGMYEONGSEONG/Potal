using System.Collections.Generic;
using UnityEngine;


//��Ÿ�ӽ� ���������ϴ� Ŭ���� 
public class Trigger : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private Player player;

    //������Ʈ Ǯ���� �ʿ��� �ش� Ŭ������ �θ� �������̽��� ����
    [Header("Object Pooling Prefebs List")]
    [SerializeField] private List<ObjectPoolData<PuzzleInteractableObject>> PuzzleInteractableObjectPrefebs;

    private void Start()
    {
        if(player !=null)
        {
            GameManager.Instance.InitStart();
            //GameManager.Instance.Player = player; //�÷��̾� ������ ����
            //GameManager.Instance.PlayerInit(); //�÷��̾� ���� 
        }

        if (PuzzleInteractableObjectPrefebs.Count > 0)
        {
            ObjectPoolManager.Instance.PuzzleInteractableObjectPool.Initialize(PuzzleInteractableObjectPrefebs);
        }

        Destroy(this);
    }


}
