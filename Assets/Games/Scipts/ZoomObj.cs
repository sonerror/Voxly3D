using UnityEngine;

public class ZoomWithMouseWheel : MonoBehaviour
{
    public enum ZoomState { Position, FOV }
    public ZoomState zoomState = ZoomState.Position;

    public Transform target; 
    public float zoomSpeed = 10f;
    public float minZoom = 5f; 
    public float maxZoom = 50f; 
    public float minFOV = 15f; 
    public float maxFOV = 90f; 

    private Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        HandleZoom();
    }

    void HandleZoom()
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        if (scrollInput != 0)
        {
            switch (zoomState)
            {
                case ZoomState.Position:
                    ZoomByPosition(scrollInput);
                    break;
                case ZoomState.FOV:
                    ZoomByFOV(scrollInput);
                    break;
            }
        }
    }

    void ZoomByPosition(float scrollInput)
    {
        if (target == null) return;

        float distance = Vector3.Distance(cam.transform.position, target.position);
        distance -= scrollInput * zoomSpeed;
        distance = Mathf.Clamp(distance, minZoom, maxZoom);

        Vector3 direction = (cam.transform.position - target.position).normalized;
        cam.transform.position = target.position + direction * distance;
    }

    void ZoomByFOV(float scrollInput)
    {
        cam.fieldOfView -= scrollInput * zoomSpeed;
        cam.fieldOfView = Mathf.Clamp(cam.fieldOfView, minFOV, maxFOV);
        if(cam.fieldOfView < maxFOV/2)
        {
            LevelManager.Ins.SetAcText();
        }
        else
        {
            LevelManager.Ins.SetAcTextAll(false);
        }
    }

  
}
