using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ObjectGrip : MonoBehaviour
{
    [Header("Object Grip")]
    public Transform GripPivotTr; //����ִ� ������Ʈ�� ��ġ
    private bool isGrip; //������Ʈ�� ����ִ»���
    private GameObject target; //����ִ� ������Ʈ�� ����
    public LayerMask GripObjectLayerMask;
    public float GripDistance = 3.0f;


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

                    hit.transform.position = GripPivotTr.position;
                    hit.transform.GetComponent<Rigidbody>().isKinematic = true;
                    hit.transform.SetParent(GripPivotTr);
                    isGrip = true;
                }
            }
            else if (isGrip && target != null)
            {
                isGrip = false;
                target.transform.SetParent(null);
                target.transform.GetComponent<Rigidbody>().isKinematic = false;
                target = null;

            }

        }

    }
}
