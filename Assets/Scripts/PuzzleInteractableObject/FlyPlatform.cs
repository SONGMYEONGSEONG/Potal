using System;
using UnityEngine;

public class FlyPlatform : PuzzleInteractableObject
{

    public FlyPlatformCollider collider;

    public LayerMask TargetLayerMask;
    public float Angle = 45.0f;
    public float RunPlatformTime = 2.0f;
    public float curTimer = 0.0f;
    public float MoveSpeed = 6;
    public float JumpPower = 50;

    public void RunPlatform(Rigidbody rigid)
    {
        //TO DO CODE : ������Ʈ �߻�

        Quaternion rotZ = Quaternion.AngleAxis(Angle, Vector3.right);
        Vector3 dir = rotZ * (transform.up);

        //�ش� �÷����� ȸ���� �Ǿ����� üũ�ϰ� ȸ���� �Ǿ��ٸ� �ش� ������ŭ �����ϱ�
        Quaternion rotY;
        if (transform.localEulerAngles.y != 0)
        {
            rotY = Quaternion.AngleAxis(transform.localEulerAngles.y, Vector3.up);
            dir = rotY * dir;
        }

        Debug.Log($"{rigid.transform.name} : {dir} �߻�");

        //�÷��̾��� ��� ����Ű�� ���� rigid.velocity���� �ٲ� �� �ֱ⿡ �ӵ��������� ����
        if(rigid.transform.TryGetComponent(out PlayerController playerController))
        {
            playerController.ExtraDir = new Vector3(dir.x, 0, dir.z) * MoveSpeed;
        }
        else
        {
            rigid.velocity += new Vector3(dir.x, 0, dir.z) * MoveSpeed; // �̵����� ����
        }
        rigid.AddForce(dir * JumpPower, ForceMode.Impulse); // �����̱⿡ y�� ���͸� ����
    }

}
