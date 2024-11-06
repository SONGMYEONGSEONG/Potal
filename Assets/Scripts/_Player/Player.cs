using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour
{
    private PlayerController controller;
    private PlayerStatus status;
    private InterAction interAction;
    private ObjectGrip objectGrip;
    private PortalPlacement portalPlacement;
    public PlayerController Controller { get { return controller; } }
    public PlayerStatus Status { get { return status; } }
    public InterAction InterAction { get { return interAction; } }
    public ObjectGrip ObjectGrip { get { return objectGrip; } }
    public PortalPlacement PortalPlacement { get { return portalPlacement; } }

    private void Awake()
    {
        //GameManager.Instance.Player = this;
        //cameraType = CameraType.TPS;
        controller = GetComponent<PlayerController>();
        status = GetComponent<PlayerStatus>();
        interAction = GetComponent<InterAction>();
        objectGrip = GetComponent<ObjectGrip>();
        portalPlacement = GetComponent<PortalPlacement>();

        portalPlacement.Portals = GameManager.Instance.PortalController.Portals;
        portalPlacement.Crosshair = GameManager.Instance.PortalController.Crosshair;
    }




}


