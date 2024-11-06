using UnityEngine;

public class PortalController : MonoBehaviour
{
    private PortalPair portals;
    private Crosshair crosshair;

    public PortalPair Portals { get => portals; }
    public Crosshair Crosshair { get => crosshair; } 

    private void Awake()
    {
        portals = GetComponentInChildren<PortalPair>();
        crosshair = GetComponentInChildren<Crosshair>();

        Instantiate(portals);
        Instantiate(crosshair);
    }

}

