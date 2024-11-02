using UnityEngine;

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
            Debug.Log("Player�� ���̾ ���� �Ǿ����ϴ�!");
        }
    }
}
