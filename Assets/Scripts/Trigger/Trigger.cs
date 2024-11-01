using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//런타임시 동적생성하는 클래스 
public class Trigger : MonoBehaviour
{
    //[Header("Player")]
    //[SerializeField] private TestObject testObj;

    //오브젝트 풀링이 필요한 해당 클래스의 부모나 인터페이스를 저장
    [Header("Object Pooling Prefebs List")]
    [SerializeField] private List<ObjectPoolData<PuzzleInteractableObject>> PuzzleInteractableObjectPrefebs;

    private void Awake()
    {
        if (PuzzleInteractableObjectPrefebs.Count > 0)
        {
            GameManager.Instance.Print();
            ObjectPoolManager.Instance.PuzzleInteractableObjectPool.Initialize(PuzzleInteractableObjectPrefebs);
        }
        else
        {
            Debug.Log($"프리팹이 존재하지 않습니다.");
        }

        //if (testObj != null)
        //{
        //    GameManager.Instance.Player = testObj;
        //    ObjectPoolManager.Instance.PuzzleInteractableObjectPool.Initialize(PuzzleInteractableObjectPrefebs);
        //} 
        //else
        //{
        //    Debug.Log($"{testObj.name}이 존재하지 않습니다.");
        //}
    }
}
