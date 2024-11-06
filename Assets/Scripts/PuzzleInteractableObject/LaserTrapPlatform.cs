using UnityEngine;
using UnityEngine.UIElements;

public class LaserTrapPlatform : PuzzleInteractableObject
{
    public Transform LaserPivot;
    public Transform Laser;
    public LayerMask PlayerLayerMask;

    private float laserDistance;

    private void Start()
    {
        laserDistance = Laser.lossyScale.y;
    }

    private void Update()
    {
        Ray ray = new Ray(LaserPivot.position, LaserPivot.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, laserDistance, PlayerLayerMask))
        {
            //Debug
            Debug.Log("Player가 레이저에 감지되어 원래 위치로 돌아갑니다.");
            hit.transform.position = StageManager.Instance.PlayerSpawnPos();
        }
    }
}
