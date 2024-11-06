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

        // ȭ�� �ػ󵵿� �°� RenderTexture�� ����
        tempTexture1 = new RenderTexture(Screen.width, Screen.height, 24, RenderTextureFormat.ARGB32);
        tempTexture2 = new RenderTexture(Screen.width, Screen.height, 24, RenderTextureFormat.ARGB32);
    }

    void Start()
    {
        // �� ��Ż�� Material�� ��� ��Ż�� RenderTexture�� ����
        portals[0].Renderer.material.mainTexture = tempTexture2; // ��Ż A�� ��Ż B�� �ؽ�ó�� ����
        portals[1].Renderer.material.mainTexture = tempTexture1; // ��Ż B�� ��Ż A�� �ؽ�ó�� ����
    }

    void Update()
    {
        // RenderTexture�� �ػ󵵰� ȭ�� ũ�⿡ ���� ������ �缳��
        UpdateRenderTextures();

        // �� ��Ż�� ��ġ�Ǿ� ���� ������ �������� �ǳʶ�
        if (!portals[0].IsPlaced || !portals[1].IsPlaced)
        {
            return;
        }

        // ��Ż A ������ (��Ż A���� ��Ż B�� ���� �� ���)
        if (portals[0].Renderer.isVisible)
        {
            portalCamera.targetTexture = tempTexture1;
            RenderCamera(portals[0], portals[1]);
        }

        // ��Ż B ������ (��Ż B���� ��Ż A�� ���� �� ���)
        if (portals[1].Renderer.isVisible)
        {
            portalCamera.targetTexture = tempTexture2;
            RenderCamera(portals[1], portals[0]);
        }
    }

    private void UpdateRenderTextures()
    {
        // ȭ�� ũ�Ⱑ ����Ǿ����� RenderTexture�� ���� ����
        if (tempTexture1.width != Screen.width || tempTexture1.height != Screen.height)
        {
            tempTexture1.Release();
            tempTexture2.Release();

            tempTexture1 = new RenderTexture(Screen.width, Screen.height, 24, RenderTextureFormat.ARGB32);
            tempTexture2 = new RenderTexture(Screen.width, Screen.height, 24, RenderTextureFormat.ARGB32);

            // �� ��Ż�� Material�� ���ο� RenderTexture�� �ٽ� ����
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

        // ī�޶� �ݺ������� �̵����� ��Ż ������ ȿ�� ����
        for (int i = 0; i < iterations; i++)
        {
            Vector3 relativePos = inTransform.InverseTransformPoint(cameraTransform.position);
            relativePos = Quaternion.Euler(0.0f, 180.0f, 0.0f) * relativePos;
            cameraTransform.position = outTransform.TransformPoint(relativePos);

            Quaternion relativeRot = Quaternion.Inverse(inTransform.rotation) * cameraTransform.rotation;
            relativeRot = Quaternion.Euler(0.0f, 180.0f, 0.0f) * relativeRot;
            cameraTransform.rotation = outTransform.rotation * relativeRot;
        }

        // ī�޶� ������ RenderTexture�� ������
        portalCamera.Render();
    }
}
