using UnityEngine;

public class ZoomObj : MonoBehaviour
{
    public enum ZoomState { Position }
    public ZoomState zoomState = ZoomState.Position;
    public Transform target;
    public float zoomSpeed = 20f;
    public float minZoom = 0f;
    public float maxZoom = 10f;
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
        if (distance < maxZoom / 2)
        {
            LevelManager.Ins.SetAcText();
        }
        else
        {
            LevelManager.Ins.SetAcTextAll(false);
        }
    }
}
