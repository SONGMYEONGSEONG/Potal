using UnityEngine;

public class PlayerFlyPlatform : PuzzleInteractableObject
{
    public PlayerFlyPlatformCollider collider;

    public LayerMask PlayerLayerMask;
    public float Angle = 45.0f;
    public float RunPlatformTime = 2.0f;
    public float curTimer = 0.0f;
    public float MoveSpeed = 6;
    public float JumpPower = 50;

    //public void RunPlatform(PlayerController controller)
    //{
    //    //TO DO CODE : 플레이어 발사


    //    Quaternion rotZ = Quaternion.AngleAxis(Angle, Vector3.right);
    //    Vector3 dir = rotZ * (transform.up);
    //    Debug.Log($"{dir} Player 발사");

    //    controller.ExtraDir = new Vector3(dir.x, 0, dir.y) * MoveSpeed;
    //    controller.Jump(dir,JumpPower);

    //}

}
