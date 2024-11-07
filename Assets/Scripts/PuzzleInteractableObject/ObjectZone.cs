using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class ObjectZone : PuzzleInteractableObject
{
    [SerializeField] private int index;
    public int Index { get => index; set => index = value; }

    private int curCount = 0;
    public TextMeshPro objectName;
    public TextMeshPro objectCount;
    public LayerMask InterActionObjectLayerMask;
    StringBuilder stringBuilder = new StringBuilder();

    //1.������Ʈ ���� n�� �̻��� ������Ʈ�� ������ �ذ�Ǵ� ���
    public bool IsSucces = false;
    public int finishCount = 0;
    public event Action<int> OnEventSucces;

    
    private void Awake()
    {
        curCount = 0;
        IsSucces = false;
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
            Debug.Log($"Count ����! , ���� ī��Ʈ {curCount}");
            curCount++;
            curCountPrint();

            if(SuccesCheck())
            {
                //ToDoCode : ��Ϳ� �����Ѱ�� ������ ����
                Debug.Log($"����� �Ϸ��߽��ϴ�!");
                SoundManager.Instance.PlaySFX("GimicSucces");
                OnEventSucces?.Invoke(index);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (InterActionObjectLayerMask == (1 << other.gameObject.layer & InterActionObjectLayerMask))
        {
            Debug.Log($"Count ����! , ���� ī��Ʈ {curCount}");
            curCount--;
            curCountPrint();
        }
    }
}
