using System.Collections.Generic;
using UnityEngine;


//런타임시 동적생성하는 클래스 
public class Trigger : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private Player player;

    //오브젝트 풀링이 필요한 해당 클래스의 부모나 인터페이스를 저장
    [Header("Object Pooling Prefebs List")]
    [SerializeField] private List<ObjectPoolData<PuzzleInteractableObject>> PuzzleInteractableObjectPrefebs;

    private void Start()
    {
        if(player !=null)
        {
            GameManager.Instance.InitStart();
            //GameManager.Instance.Player = player; //플레이어 데이터 셋팅
            //GameManager.Instance.PlayerInit(); //플레이어 생성 
        }

        if (PuzzleInteractableObjectPrefebs.Count > 0)
        {
            ObjectPoolManager.Instance.PuzzleInteractableObjectPool.Initialize(PuzzleInteractableObjectPrefebs);
        }

        Destroy(this);
    }


}
