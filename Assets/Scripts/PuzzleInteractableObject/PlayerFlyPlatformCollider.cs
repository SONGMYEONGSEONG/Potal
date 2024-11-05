using UnityEngine;

public class FlyPlatformCollider : MonoBehaviour
{
    public FlyPlatform parentObject;

    private void Awake()
    {
        parentObject = GetComponentInParent<FlyPlatform>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsLayerMatcted(other.gameObject.layer, parentObject.TargetLayerMask))
        {
            parentObject.curTimer = 0.0f;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (IsLayerMatcted(other.gameObject.layer, parentObject.TargetLayerMask))
        {
            parentObject.curTimer += Time.deltaTime;
            if (parentObject.RunPlatformTime <= parentObject.curTimer)
            {

                if (other.TryGetComponent(out Rigidbody rigid))
                {
                    parentObject.RunPlatform(rigid);
                }
                else
                {
                    Debug.Log($"해당 콜리더의 리지드바디가 존재하지 않음 {other.name}");
                }
            }
        }
    }

    private bool IsLayerMatcted(int layer , LayerMask targetLayer)
    {
        if(targetLayer == (1 << layer | targetLayer))
        {
            return true;
        }
        return false;
    }
}
