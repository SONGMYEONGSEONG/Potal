using UnityEngine;

public class JumpPlatform : PuzzleInteractableObject
{
    public float JumpPlatformPower = 200f;

    public void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            player.Controller.Jump(JumpPlatformPower);
        }
    }
}
