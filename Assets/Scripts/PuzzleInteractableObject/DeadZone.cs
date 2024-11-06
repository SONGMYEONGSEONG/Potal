using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    public LayerMask PlayerLayerMask;
    public LayerMask InterActableObjectLayerMask;

    public void OnTriggerEnter(Collider other)
    {
        //플레이어 레이어와 충돌한경우
        if (IsLayerMatcted(other.gameObject.layer, PlayerLayerMask))
        {
            //ToDoCode : 플레이어 동작 작성
            other.transform.position = StageManager.Instance.PlayerSpawnPos();
        }
        //상호작용오브젝트와 충돌한경우
        else if(IsLayerMatcted(other.gameObject.layer, InterActableObjectLayerMask))
        {
            if(GameManager.Instance.Player.ObjectGrip.isGrip) //데드존에 충돌한 오브젝트가 플레이어 손에 있는지 체크
            {
                if(GameManager.Instance.Player.ObjectGrip.GripTargetCheck(other.gameObject)) //플레이어 손에 있는 오브젝트와 현재 충돌한 오브젝트가 같은지 체크
                {
                    GameManager.Instance.Player.ObjectGrip.GripInit(); //잡은상태 초기화
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
