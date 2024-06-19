using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomAndMoveLevel : MonoBehaviour
{
    public float touchesPrePosDiff, touchesCurPosDiff, zoomModifier;
    public Vector2 firstTouchPrePos, secondTouchPrePos;
    public float zoomWheelSpeed;
    public float minOrthoSize;
    public float maxOrthoSize;
    public float zoomMobileSpeed;
    public float minFOV = 15f;
    public float maxFOV = 90f;
    private Camera cam;

    void Start()
    {
        cam = Camera.main;
        Camera.main.orthographicSize = maxOrthoSize;
    }
    void Update()
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");

        if (Input.touchCount == 2 && !UI_Hover.IsPointerOverUIElement())
        {
            Touch firstTouch = Input.GetTouch(0);
            Touch secondTouch = Input.GetTouch(1);

            firstTouchPrePos = firstTouch.position - firstTouch.deltaPosition;
            secondTouchPrePos = secondTouch.position - secondTouch.deltaPosition;
            touchesPrePosDiff = (firstTouchPrePos - secondTouchPrePos).sqrMagnitude;
            touchesCurPosDiff = (firstTouch.position - secondTouch.position).sqrMagnitude;

            Vector2 firstDelta = firstTouch.deltaPosition;
            Vector2 secondDelta = secondTouch.deltaPosition;

            bool isZoomIn = Vector2.Dot(firstDelta.normalized, secondDelta.normalized) < 0;

            float deltaPos = (firstTouch.deltaPosition - secondTouch.deltaPosition).sqrMagnitude;
            deltaPos = Mathf.Clamp(deltaPos, 0, 400f);
            zoomModifier = deltaPos * zoomMobileSpeed * Time.deltaTime;

            if (isZoomIn)
            {
                if (touchesPrePosDiff < touchesCurPosDiff)
                {
                    Camera.main.orthographicSize -= zoomModifier;
                }
            }
            else
            {
                if (touchesPrePosDiff > touchesCurPosDiff)
                {
                    Camera.main.orthographicSize += zoomModifier;
                }
                else if (touchesPrePosDiff < touchesCurPosDiff)
                {
                    if ((Camera.main.orthographicSize - zoomModifier) < minOrthoSize)
                    {
                        Camera.main.orthographicSize = minOrthoSize;
                    }
                    else
                    {
                        Camera.main.orthographicSize -= zoomModifier;
                    }
                }
            }

            Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, minOrthoSize, maxOrthoSize);
        }

        float newOrthographicSize = Camera.main.orthographicSize - scrollInput * zoomWheelSpeed * Time.deltaTime;
        Camera.main.orthographicSize = Mathf.Clamp(newOrthographicSize, minOrthoSize, maxOrthoSize);
        if (Camera.main.orthographicSize < maxOrthoSize / 1.4)
        {
            LevelManager.Ins.SetMatZoomIn();
            LevelManager.Ins.ChangematFormID();
            LevelManager.Ins.isChangeMatSl = true;
        }
        else
        {
            LevelManager.Ins.isChangeMatSl = false;
            LevelManager.Ins.SetMatZoomOut();
        }
    }
}
