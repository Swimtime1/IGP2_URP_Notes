using UnityEngine;

public class PortalCam: MonoBehaviour
{
    public Camera playerCamera;
    public Transform portal;

    public Camera cameraB;
    public Material cameraMatb;

    public Vector3 offset = new Vector3(0, 1, 0);
    void Start()
    {
        if (cameraB.targetTexture != null)
            cameraB.targetTexture.Release();

        cameraB.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        cameraMatb.mainTexture = cameraB.targetTexture;

    }

    // Update is called once per frame
    void Update()
    {
        cameraB.projectionMatrix = playerCamera.projectionMatrix;

        Vector3 relativePos = transform.InverseTransformPoint(playerCamera.transform.position);
        relativePos = Vector3.Scale(relativePos, new Vector3(-1, 1, -1));
        cameraB.transform.position = portal.TransformPoint(relativePos);

        Vector3 relRotation = portal.InverseTransformDirection(playerCamera.transform.forward);
        relRotation = Vector3.Scale(relRotation, new Vector3(-1, 1, -1));
        cameraB.transform.forward = portal.TransformDirection(relRotation);

        //cameraB.nearClipPlane = Vector3.Distance(transform.position, portal.position); // clip anything between the camera and the portal!
    }

}