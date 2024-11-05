using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class ObjectZone : MonoBehaviour
{
    [SerializeField] private int index;
    public int Index { get => index; set => index = value; }

    private int curCount = 0;
    private TextMeshPro objectCount;
    public LayerMask InterActionObjectLayerMask;
    StringBuilder stringBuilder = new StringBuilder();

    //1.오브젝트 존에 n개 이상의 오브젝트를 놓으면 해결되는 기믹
    public bool IsSucces = false;
    public int finishCount = 0;
    public event Action<int> OnEventSucces;

    private void Awake()
    {
        curCount = 0;
        IsSucces = false;
        objectCount = GetComponentInChildren<TextMeshPro>();
        curCountPrint();
    }

    private void curCountPrint()
    {
        stringBuilder.Clear();
        stringBuilder.Append(curCount + " / " + finishCount);
        objectCount.text = stringBuilder.ToString();
    }

    private bool SuccesCheck()
    {
        if(curCount >= finishCount)
        {
            return true;
        }
        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(InterActionObjectLayerMask == (1 << other.gameObject.layer & InterActionObjectLayerMask))
        {
            Debug.Log($"Count 증가! , 현재 카운트 {curCount}");
            curCount++;
            curCountPrint();

            if(SuccesCheck())
            {
                //ToDoCode : 기믹에 성공한경우 리워드 지급
                Debug.Log($"기믹을 완료했습니다!");
                OnEventSucces?.Invoke(index);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (InterActionObjectLayerMask == (1 << other.gameObject.layer & InterActionObjectLayerMask))
        {
            Debug.Log($"Count 감소! , 현재 카운트 {curCount}");
            curCount--;
            curCountPrint();
        }
    }
}
