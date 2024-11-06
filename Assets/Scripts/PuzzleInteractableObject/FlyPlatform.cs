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
        //TO DO CODE : 오브젝트 발사

        Quaternion rotZ = Quaternion.AngleAxis(Angle, Vector3.right);
        Vector3 dir = rotZ * (transform.up);

        //해당 플랫폼이 회전이 되었는지 체크하고 회전이 되었다면 해당 각도만큼 보정하기
        Quaternion rotY;
        if (transform.localEulerAngles.y != 0)
        {
            rotY = Quaternion.AngleAxis(transform.localEulerAngles.y, Vector3.up);
            dir = rotY * dir;
        }

        Debug.Log($"{rigid.transform.name} : {dir} 발사");

        //플레이어인 경우 조작키에 따라 rigid.velocity값을 바꿀 수 있기에 속도보정값에 적용
        if(rigid.transform.TryGetComponent(out PlayerController playerController))
        {
            playerController.ExtraDir = new Vector3(dir.x, 0, dir.z) * MoveSpeed;
        }
        else
        {
            rigid.velocity += new Vector3(dir.x, 0, dir.z) * MoveSpeed; // 이동방향 보정
        }
        rigid.AddForce(dir * JumpPower, ForceMode.Impulse); // 점프이기에 y축 벡터만 보정
    }

}
