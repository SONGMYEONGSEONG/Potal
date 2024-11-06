using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    public LayerMask PlayerLayerMask;
    public LayerMask InterActableObjectLayerMask;

    public void OnTriggerEnter(Collider other)
    {
        //�÷��̾� ���̾�� �浹�Ѱ��
        if (IsLayerMatcted(other.gameObject.layer, PlayerLayerMask))
        {
            //ToDoCode : �÷��̾� ���� �ۼ�
            other.transform.position = StageManager.Instance.PlayerSpawnPos();
        }
        //��ȣ�ۿ������Ʈ�� �浹�Ѱ��
        else if(IsLayerMatcted(other.gameObject.layer, InterActableObjectLayerMask))
        {
            if(GameManager.Instance.Player.ObjectGrip.isGrip) //�������� �浹�� ������Ʈ�� �÷��̾� �տ� �ִ��� üũ
            {
                if(GameManager.Instance.Player.ObjectGrip.GripTargetCheck(other.gameObject)) //�÷��̾� �տ� �ִ� ������Ʈ�� ���� �浹�� ������Ʈ�� ������ üũ
                {
                    GameManager.Instance.Player.ObjectGrip.GripInit(); //�������� �ʱ�ȭ
                }
            }

            if(other.transform.TryGetComponent(out PuzzleInteractableObject interactionObj))
            {
                other.transform.position = interactionObj.OrginPos;
            }
        }
        
    }

    private bool IsLayerMatcted(int layer, LayerMask targetLayer)
    {
        if (targetLayer == (1 << layer | targetLayer))
        {
            return true;
        }
        return false;
    }
}
