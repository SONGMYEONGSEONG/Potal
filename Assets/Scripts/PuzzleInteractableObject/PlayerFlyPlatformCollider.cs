using UnityEngine;

public class PlayerFlyPlatformCollider : MonoBehaviour
{
    public PlayerFlyPlatform parentObject;

    private void Awake()
    {
        parentObject = GetComponentInParent<PlayerFlyPlatform>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (1 << other.gameObject.layer == parentObject.PlayerLayerMask)
        {
            parentObject.curTimer = 0.0f;
        }
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    if (1 << other.gameObject.layer == parentObject.PlayerLayerMask)
    //    {
    //        parentObject.curTimer += Time.deltaTime;
    //        if (parentObject.RunPlatformTime <= parentObject.curTimer)
    //        {

    //            if (other.TryGetComponent(out PlayerController controller))
    //            {
    //                parentObject.RunPlatform(controller);
    //            }
    //            else
    //            {
    //                Debug.Log($"해당 콜리더의 리지드바디가 존재하지 않음 {other.name}");
    //            }
    //        }
    //    }
    //}
}
