using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using RenderPipeline = UnityEngine.Rendering.RenderPipelineManager;

public class PortalCamera : MonoBehaviour
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

        tempTexture1 = new RenderTexture(Screen.width, Screen.height, 24, RenderTextureFormat.ARGB32);
        tempTexture2 = new RenderTexture(Screen.width, Screen.height, 24, RenderTextureFormat.ARGB32);
    }

    void Start()
    {
        portals[0].Renderer.material.mainTexture = tempTexture1;
        portals[1].Renderer.material.mainTexture = tempTexture2;
    }

    private void OnEnable()
    {
        RenderPipeline.beginCameraRendering += UpdateCamera;
    }

    private void OnDisable()
    {
        RenderPipeline.beginCameraRendering -= UpdateCamera;

        if (tempTexture1 != null)
        {
            tempTexture1.Release();
            tempTexture1 = null;
        }
        if (tempTexture2 != null)
        {
            tempTexture2.Release();
            tempTexture2 = null;
        }
    }

    void UpdateCamera(ScriptableRenderContext SRC, Camera camera)
    {
        UpdateRenderTextures();

        if (!portals[0].IsPlaced || !portals[1].IsPlaced)
        {
            return;
        }

        if (portals[0].Renderer.isVisible)
        {
            portalCamera.targetTexture = tempTexture1;
            for (int i = iterations - 1; i >= 0; --i)
            {
                RenderCamera(portals[0], portals[1], i, SRC);
            }
        }

        if (portals[1].Renderer.isVisible)
        {
            portalCamera.targetTexture = tempTexture2;
            for (int i = iterations - 1; i >= 0; --i)
            {
                RenderCamera(portals[1], portals[0], i, SRC);
            }
        }
    }

    private void UpdateRenderTextures()
    {
        if (tempTexture1.width != Screen.width || tempTexture1.height != Screen.height)
        {
            tempTexture1.Release();
            tempTexture2.Release();

            tempTexture1 = new RenderTexture(Screen.width, Screen.height, 24, RenderTextureFormat.ARGB32);
            tempTexture2 = new RenderTexture(Screen.width, Screen.height, 24, RenderTextureFormat.ARGB32);

            portals[0].Renderer.material.mainTexture = tempTexture1;
            portals[1].Renderer.material.mainTexture = tempTexture2;
        }
    }


    private void RenderCamera(Portal inPortal, Portal outPortal, int iterationID, ScriptableRenderContext SRC)
    {
        portalCamera.clearFlags = CameraClearFlags.SolidColor;
        portalCamera.backgroundColor = Color.white;

        Transform inTransform = inPortal.transform;
        Transform outTransform = outPortal.transform;

        Transform cameraTransform = portalCamera.transform;
        cameraTransform.position = transform.position;
        cameraTransform.rotation = transform.rotation;

        for (int i = 0; i <= iterationID; ++i)
        {
            // Position the camera behind the other portal.
            Vector3 relativePos = inTransform.InverseTransformPoint(cameraTransform.position);
            relativePos = Quaternion.Euler(0.0f, 180.0f, 0.0f) * relativePos;
            cameraTransform.position = outTransform.TransformPoint(relativePos);

            // Rotate the camera to look through the other portal.
            Quaternion relativeRot = Quaternion.Inverse(inTransform.rotation) * cameraTransform.rotation;
            relativeRot = Quaternion.Euler(0.0f, 180.0f, 0.0f) * relativeRot;
            cameraTransform.rotation = outTransform.rotation * relativeRot;
        }

        // Set the camera's oblique view frustum.
        Plane p = new Plane(-outTransform.forward, outTransform.position);
        Vector4 clipPlaneWorldSpace = new Vector4(p.normal.x, p.normal.y, p.normal.z, p.distance);
        Vector4 clipPlaneCameraSpace =
            Matrix4x4.Transpose(Matrix4x4.Inverse(portalCamera.worldToCameraMatrix)) * clipPlaneWorldSpace;

        var newMatrix = mainCamera.CalculateObliqueMatrix(clipPlaneCameraSpace);
        portalCamera.projectionMatrix = newMatrix;

        // Render the camera to its render target.
        //UniversalRenderPipeline.RenderSingleCamera(SRC, portalCamera);

        // Render the camera using ScriptableRenderContext
        portalCamera.Render(); // 커스텀 렌더링을 위해 Render() 메서드 호출

        // Submit the context after rendering
        SRC.Submit(); // 반드시 제출하여 커스텀 렌더링이 화면에 반영되도록 함
    }
}
