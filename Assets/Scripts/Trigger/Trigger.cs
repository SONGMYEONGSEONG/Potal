using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//��Ÿ�ӽ� ���������ϴ� Ŭ���� 
public class Trigger : MonoBehaviour
{
    //[Header("Player")]
    //[SerializeField] private TestObject testObj;

    //������Ʈ Ǯ���� �ʿ��� �ش� Ŭ������ �θ� �������̽��� ����
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
            Debug.Log($"�������� �������� �ʽ��ϴ�.");
        }

        //if (testObj != null)
        //{
        //    GameManager.Instance.Player = testObj;
        //    ObjectPoolManager.Instance.PuzzleInteractableObjectPool.Initialize(PuzzleInteractableObjectPrefebs);
        //} 
        //else
        //{
        //    Debug.Log($"{testObj.name}�� �������� �ʽ��ϴ�.");
        //}
    }
}
