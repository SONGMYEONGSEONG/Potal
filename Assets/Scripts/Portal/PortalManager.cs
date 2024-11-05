//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class PortalManager : MonoBehaviour
//{
//    public Transform position1, position2;
//    public Transform portalACam;
//    public Transform portalBCam;
//    public Transform playerCam;
//    public Transform playerRoot;

//    public RenderTexture renderTextureA;
//    public RenderTexture renderTextureB;

//    void Start()
//    {
//        renderTextureA.width = Screen.width;
//        renderTextureA.height = Screen.height;
//        renderTextureB.width = Screen.width;
//        renderTextureB.height = Screen.height;
//    }

//    void Update()
//    {
//        Vector3 playerOffset = playerCam.position - position1.position;

//        portalACam.position = position2.position + playerOffset;
//        portalACam.rotation = playerCam.rotation;
//        portalBCam.position = position2.position + playerOffset;
//        portalBCam.rotation = playerCam.rotation ;
//    }
//}
