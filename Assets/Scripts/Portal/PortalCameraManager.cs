using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCameraManager : MonoBehaviour
{
    [SerializeField]
    private Portal[] portals = new Portal[2];

    [SerializeField]
    private Camera portalCamera;

    [SerializeField]
    private int iterations = 7;

    private RenderTexture tempTexture1;
    private RenderTexture tempTexture2;

    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = GetComponent<Camera>();

        // 화면 해상도에 맞게 RenderTexture를 생성
        tempTexture1 = new RenderTexture(Screen.width, Screen.height, 24, RenderTextureFormat.ARGB32);
        tempTexture2 = new RenderTexture(Screen.width, Screen.height, 24, RenderTextureFormat.ARGB32);
    }

    void Start()
    {
        // 각 포탈의 Material에 상대 포탈의 RenderTexture를 설정
        portals[0].Renderer.material.mainTexture = tempTexture2; // 포탈 A에 포탈 B의 텍스처를 설정
        portals[1].Renderer.material.mainTexture = tempTexture1; // 포탈 B에 포탈 A의 텍스처를 설정
    }

    void Update()
    {
        // RenderTexture의 해상도가 화면 크기에 맞지 않으면 재설정
        UpdateRenderTextures();

        // 두 포탈이 배치되어 있지 않으면 렌더링을 건너뜀
        if (!portals[0].IsPlaced || !portals[1].IsPlaced)
        {
            return;
        }

        // 포탈 A 렌더링 (포탈 A에서 포탈 B를 통해 본 장면)
        if (portals[0].Renderer.isVisible)
        {
            portalCamera.targetTexture = tempTexture1;
            RenderCamera(portals[0], portals[1]);
        }

        // 포탈 B 렌더링 (포탈 B에서 포탈 A를 통해 본 장면)
        if (portals[1].Renderer.isVisible)
        {
            portalCamera.targetTexture = tempTexture2;
            RenderCamera(portals[1], portals[0]);
        }
    }

    private void UpdateRenderTextures()
    {
        // 화면 크기가 변경되었으면 RenderTexture를 새로 생성
        if (tempTexture1.width != Screen.width || tempTexture1.height != Screen.height)
        {
            tempTexture1.Release();
            tempTexture2.Release();

            tempTexture1 = new RenderTexture(Screen.width, Screen.height, 24, RenderTextureFormat.ARGB32);
            tempTexture2 = new RenderTexture(Screen.width, Screen.height, 24, RenderTextureFormat.ARGB32);

            // 각 포탈의 Material에 새로운 RenderTexture를 다시 설정
            portals[0].Renderer.material.mainTexture = tempTexture2;
            portals[1].Renderer.material.mainTexture = tempTexture1;
        }
    }

    private void RenderCamera(Portal inPortal, Portal outPortal)
    {
        portalCamera.clearFlags = CameraClearFlags.SolidColor;
        portalCamera.backgroundColor = Color.white;

        Transform inTransform = inPortal.transform;
        Transform outTransform = outPortal.transform;

        Transform cameraTransform = portalCamera.transform;
        cameraTransform.position = transform.position;
        cameraTransform.rotation = transform.rotation;

        // 카메라를 반복적으로 이동시켜 포탈 렌더링 효과 적용
        for (int i = 0; i < iterations; i++)
        {
            Vector3 relativePos = inTransform.InverseTransformPoint(cameraTransform.position);
            relativePos = Quaternion.Euler(0.0f, 180.0f, 0.0f) * relativePos;
            cameraTransform.position = outTransform.TransformPoint(relativePos);

            Quaternion relativeRot = Quaternion.Inverse(inTransform.rotation) * cameraTransform.rotation;
            relativeRot = Quaternion.Euler(0.0f, 180.0f, 0.0f) * relativeRot;
            cameraTransform.rotation = outTransform.rotation * relativeRot;
        }

        // 카메라에 설정된 RenderTexture로 렌더링
        portalCamera.Render();
    }
}
