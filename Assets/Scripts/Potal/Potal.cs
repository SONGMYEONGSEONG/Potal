using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Potal : MonoBehaviour
{
    public Camera potalCamera;//���� ī�޶� ���߸�� 
    public Potal EndPotal;
    
    private void OnTriggerEnter(Collider other)
    {
        other.transform.position = EndPotal.transform.position + (EndPotal.transform.forward * 3);
    }




}
