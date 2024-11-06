using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;

public class ObjectGrip : MonoBehaviour
{
    [Header("Object Grip")]
    public Transform GripPivotTr; //����ִ� ������Ʈ�� ��ġ
    public bool isGrip; //������Ʈ�� ����ִ»���
    private GameObject target; //����ִ� ������Ʈ�� ����
    public LayerMask GripObjectLayerMask;
    public float GripDistance = 3.0f;

    //���� ��ü�� ��ǥ�� ��� ����������Ѵ�.
    //���� : ���� ��ü�� ���� �浹������ �ؾߵǷ��� ���� ��ġ�� ��� �����ϰ� Ű�׸����� ���߸� ����� ���������� �����ϱ� ����
    //��� 1. ���� ��ü�� �÷��̾ ��� ����ٴϰ� �ϴ� ���
    //��� 2. ���� ��ü�� ��ġ�� ���� ��ǥ�� ����� Player�� �̵��� ��ǥ�� ���� �ϴ� ���

    private void Update()
    {
        //��� 2. ���� ��ü�� ��ġ�� ���� ��ǥ�� ����� Player�� �̵��� ���󰡰� �ϴ� ���
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
