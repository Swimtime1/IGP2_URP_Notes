using System;
using UnityEngine;
using UnityEngine.Rendering;

[ExecuteInEditMode]
public class Portal : MonoBehaviour
{
    public Camera playerCamera;
    public Transform portal;

    public Camera cameraB;
    public Material cameraMatb;

    public Vector3 offset = new Vector3(0, 1, 0);

    private void OnEnable()
    {
        
        if (cameraB.targetTexture != null)
            cameraB.targetTexture.Release();

        cameraB.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        cameraMatb.mainTexture = cameraB.targetTexture;
        
        
        RenderPipelineManager.beginCameraRendering += UpdateCamera;
    }
    private void OnDisable()
    {
        RenderPipelineManager.beginCameraRendering -= UpdateCamera;
    }

    private void UpdateCamera(ScriptableRenderContext context, Camera camera)
    {
        if ((camera.cameraType == CameraType.Game || camera.cameraType == CameraType.SceneView) &&
            camera.tag != "Portal Camera") {

            cameraB.projectionMatrix = camera.projectionMatrix;

            Vector3 relativePos = transform.InverseTransformPoint(camera.transform.position);
            relativePos = Vector3.Scale(relativePos, new Vector3(-1, 1, -1));
            cameraB.transform.position = portal.TransformPoint(relativePos);

            Vector3 relRotation = transform.InverseTransformDirection(camera.transform.forward);
            relRotation = Vector3.Scale(relRotation, new Vector3(-1, 1, -1));
            cameraB.transform.forward = portal.TransformDirection(relRotation);

            if (camera.cameraType != CameraType.SceneView)
                cameraB.nearClipPlane = Vector3.Distance(cameraB.transform.position, portal.position)-0.5f;
        }
    }
}
