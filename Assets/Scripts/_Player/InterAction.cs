using System;
using UnityEngine;


public class InterAction : MonoBehaviour
{
    public event Action<string, string> OnEventUIInterActionPopUp;

    public LayerMask layerMask;
    public float RayDistance = 10.0f;

    private RaycastHit hit;
    private string targetName;
    private string targetDescription;


    private void Update()
    {
        Ray ray;
        ray = new Ray(GameManager.Instance.Player.transform.position, GameManager.Instance.Player.transform.forward);

        //if (Physics.Raycast(ray, out hit, RayDistance, layerMask))
        //{
        //    targetName = hit.transform.GetComponent<InterActableObject>().ObjectSO.Name;
        //    targetDescription = hit.transform.GetComponent<InterActableObject>().ObjectSO.Description;

        //    OnEventUIInterActionPopUp?.Invoke(targetName, targetDescription);
        //}
        //else
        //{
        //    UIInterAction UiWindow = UIManager.Instance.UIDict[UIKey.InterAction] as UIInterAction;
        //    UiWindow.PopDownWindow();
        //}
    }
}
