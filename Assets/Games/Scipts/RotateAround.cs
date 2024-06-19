using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour
{
    public float rotateSpeed = 1f;
    private Vector2? lastTouchPos;
    private bool isDragging = false;
    void Update()
    {
        HandleMouseInput();
        HandleTouchInput();
    }
    void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0) && !UI_Hover.IsPointerOverUIElement())
        {
            isDragging = true;
            lastTouchPos = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0) && !UI_Hover.IsPointerOverUIElement())
        {
            isDragging = false;
            lastTouchPos = null;
        }
        if (isDragging && LevelManager.Ins.areDrawing == false)
        {
            Vector2 currentTouchPos = (Vector2)Input.mousePosition;
            if (lastTouchPos.HasValue)
            {
                Vector2 delta = currentTouchPos - lastTouchPos.Value;

                float rotationX = delta.y * rotateSpeed * Time.deltaTime;
                float rotationY = -delta.x * rotateSpeed * Time.deltaTime;

                transform.Rotate(Vector3.up, rotationY, Space.World);
                transform.Rotate(Vector3.right, rotationX, Space.World);
            }
            lastTouchPos = currentTouchPos;
        }
    }
    void HandleTouchInput()
    {
        if (Input.touchCount > 0 && !UI_Hover.IsPointerOverUIElement() && Input.touchCount < 2)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    lastTouchPos = touch.position;
                    break;

                case TouchPhase.Moved:
                    if (lastTouchPos != null)
                    {
                        Vector2 currentTouchPos = touch.position;
                        Vector2 delta = currentTouchPos - lastTouchPos.Value;
                        float rotationX = delta.y * rotateSpeed * Time.deltaTime;
                        float rotationY = -delta.x * rotateSpeed * Time.deltaTime;
                        transform.Rotate(Vector3.up, rotationY, Space.World);
                        transform.Rotate(Vector3.right, rotationX, Space.World);
                        lastTouchPos = currentTouchPos;
                    }
                    break;

                case TouchPhase.Ended:
                    lastTouchPos = null;
                    break;
            }
        }
    }

}
