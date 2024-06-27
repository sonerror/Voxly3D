using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour
{
    public float rotateSpeed = 1f;
    private Vector2? lastTouchPos;
    private bool isDragging = false;
    public float smoothTime = 0.1f;
    private Quaternion targetRotation;
    void Start()
    {
        targetRotation = transform.rotation;
    }

    void Update()
    {
        if (LevelManager.Ins.isWin == true) return;
        HandleMouseInput();
        HandleTouchInput();
        SmoothRotate();
    }

    void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0) && !UI_Hover.IsPointerOverUIElement() && Input.touchCount < 2)
        {
            isDragging = true;
            lastTouchPos = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            lastTouchPos = null;
        }
        if (isDragging && !LevelManager.Ins.areDrawing && Input.touchCount < 2)
        {
            Vector2 currentTouchPos = (Vector2)Input.mousePosition;
            if (lastTouchPos.HasValue)
            {
                Vector2 delta = currentTouchPos - lastTouchPos.Value;
                float rotationX = delta.y * rotateSpeed * Time.deltaTime;
                float rotationY = -delta.x * rotateSpeed * Time.deltaTime;
                Quaternion rotation = Quaternion.Euler(rotationX, rotationY, 0);
                targetRotation = rotation * targetRotation;
            }
            lastTouchPos = currentTouchPos;
        }
    }

    void SmoothRotate()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime / smoothTime);
    }
    void HandleTouchInput()
    {
        if (Input.touchCount == 1 && !UI_Hover.IsPointerOverUIElement() && Input.touchCount < 2)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began && Input.touchCount < 2)
            {
                isDragging = true;
                lastTouchPos = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                isDragging = false;
                lastTouchPos = null;
            }
            if (isDragging && !LevelManager.Ins.areDrawing)
            {
                Vector2 currentTouchPos = touch.position;
                if (lastTouchPos.HasValue)
                {
                    Vector2 delta = currentTouchPos - lastTouchPos.Value;
                    float rotationX = delta.y * rotateSpeed * Time.deltaTime;
                    float rotationY = -delta.x * rotateSpeed * Time.deltaTime;
                    Quaternion rotation = Quaternion.Euler(rotationX, rotationY, 0);
                    targetRotation = rotation * targetRotation;
                }
                lastTouchPos = currentTouchPos;
            }
        }
    }
}
