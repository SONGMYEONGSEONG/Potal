using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;

public class ObjectGrip : MonoBehaviour
{
    [Header("Object Grip")]
    public Transform GripPivotTr; //잡고있는 오브젝트의 위치
    public bool isGrip; //오브젝트를 잡고있는상태
    private GameObject target; //잡고있는 오브젝트의 정보
    public LayerMask GripObjectLayerMask;
    public float GripDistance = 3.0f;

    //잡힌 물체의 좌표를 계속 갱신해줘야한다.
    //이유 : 잡힌 물체의 물리 충돌연산을 해야되려면 잡힌 위치를 계속 갱신하고 키네마닉을 꺼야만 사용이 물리연산이 가능하기 때문
    //방법 1. 잡힌 물체가 플레이어를 계속 따라다니게 하는 방법
    //방법 2. 잡힌 물체의 위치를 고정 좌표를 만든뒤 Player가 이동시 좌표를 갱신 하는 방법

    private void Update()
    {
        //방법 2. 잡힌 물체의 위치를 고정 좌표를 만든뒤 Player가 이동시 따라가게 하는 방법
        if (isGrip)
        {
            target.transform.position = GripPivotTr.position + new Vector3 (0,1,0);
        }
        
    }

    public void OnGrip(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            if (!isGrip && target == null)
            {
                Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, GripDistance, GripObjectLayerMask))
                {
                    target = hit.transform.gameObject;
                    if(target.transform.TryGetComponent(out Rigidbody targetRigid))
                    {
                        targetRigid.freezeRotation = true;
                    }
                    
                    isGrip = true;
                }
            }
            else if (isGrip && target != null)
            {
                GripInit();
            }

        }

    }

    public void GripInit()
    {
        isGrip = false;
        if (target.transform.TryGetComponent<Rigidbody>(out Rigidbody targetRigid))
        {
            targetRigid.freezeRotation = false;
        }
        target = null;
    }

    public bool GripTargetCheck(GameObject gribCube)
    {
        if(gribCube == target)
        {
            return true;
        }


        return false;
    }
}
