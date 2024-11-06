using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPortal : MonoBehaviour
{
    public PlayerControllerPortal controller;

    private void Awake()
    {
        CharacterManager.Instance.PlayerPortal = this;
        controller = GetComponent<PlayerControllerPortal>();
    }
}
