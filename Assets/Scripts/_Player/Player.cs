using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour
{
    //public CameraType cameraType;

    //public CharacterStatusSO statusSO;
    private PlayerController controller;
    private PlayerStatus status;
    private InterAction interAction;
    public PlayerController Controller { get { return controller; } }
    public PlayerStatus Status { get { return status; } }
    public InterAction InterAction { get { return interAction; } }


    private void Awake()
    {
        GameManager.Instance.Player = this;
        //cameraType = CameraType.TPS;
        controller = gameObject.GetComponent<PlayerController>();
        status = gameObject.GetComponent<PlayerStatus>();
        interAction = gameObject.GetComponent<InterAction>();
    }




}


