using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomAndMoveLevel : MonoBehaviour
{
    public float rotateSpeed = 0.2f;
    public float zoomMobileSpeed = 0.001f;
    public float minOrthoSize = 1f;
    public float maxOrthoSize = 10f;
    public float dragSpeed = 0.1f;
    public float zoomWheelSpeed = 40f;
    private Vector2 firstTouchPrePos, secondTouchPrePos;
    private float touchesPrePosDiff, touchesCurPosDiff;
    private float zoomModifier;
    private Vector2? lastTouchPos = null;
    private Camera cam;
    // private RectTransform canvasRectTransform;

    void Start()
    {
        cam = Camera.main;
        cam.orthographicSize = maxOrthoSize;
        //canvasRectTransform = GameObject.Find("Canvas").GetComponent<RectTransform>();
    }
    public void SetTFDef()
    {
        cam.DOOrthoSize(maxOrthoSize / 3, 0.5f).SetEase(Ease.InOutQuad);
    }

    void Update()
    {
        if (LevelManager.Ins.isWin == true) return;
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
            bool isZooming = Vector2.Dot(firstDelta.normalized, secondDelta.normalized) < 0;

            if (isZooming)
            {
                HandleZoom(touchesPrePosDiff, touchesCurPosDiff, firstTouch, secondTouch);
            }
            else
            {
                // HandleMove(firstTouch, secondTouch);
            }

            cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, minOrthoSize, maxOrthoSize);
            UpdateLevelManagerZoomState(cam.orthographicSize);
        }

        float newOrthographicSize = cam.orthographicSize - scrollInput * zoomWheelSpeed * Time.deltaTime;
        cam.orthographicSize = Mathf.Clamp(newOrthographicSize, minOrthoSize, maxOrthoSize);
        UpdateLevelManagerZoomState(cam.orthographicSize);
    }

    void HandleZoom(float touchesPrePosDiff, float touchesCurPosDiff, Touch firstTouch, Touch secondTouch)
    {
        float deltaPos = (firstTouch.deltaPosition - secondTouch.deltaPosition).sqrMagnitude;
        deltaPos = Mathf.Clamp(deltaPos, 0, 400f);
        zoomModifier = deltaPos * zoomMobileSpeed;

        if (touchesPrePosDiff < touchesCurPosDiff)
        {
            cam.orthographicSize -= zoomModifier;
            //cam.DOOrthoSize(cam.orthographicSize + zoomModifier, 0.1f).SetEase(Ease.OutQuad);

        }
        else if (touchesPrePosDiff > touchesCurPosDiff)
        {
            cam.orthographicSize += zoomModifier;
            //cam.DOOrthoSize(cam.orthographicSize - zoomModifier, 0.1f).SetEase(Ease.OutQuad);
        }
    }

    /*    void HandleMove(Touch firstTouch, Touch secondTouch)
        {
            Vector2 currentTouchPos = (firstTouch.position + secondTouch.position) / 2f;
            if (lastTouchPos.HasValue)
            {
                Vector2 delta = currentTouchPos - lastTouchPos.Value;
                Vector3 moveDirection = new Vector3(delta.x, delta.y, 0f) * dragSpeed;
                Vector3 screenPosition = cam.WorldToScreenPoint(transform.position);
                screenPosition.x = Mathf.Clamp(screenPosition.x, 0, Screen.width);
                screenPosition.y = Mathf.Clamp(screenPosition.y, 0, Screen.height);
                Vector3 worldPosition = cam.ScreenToWorldPoint(screenPosition + moveDirection);
                Vector3 localPos = canvasRectTransform.InverseTransformPoint(worldPosition);
                localPos.x = Mathf.Clamp(localPos.x, -canvasRectTransform.rect.width / 2, canvasRectTransform.rect.width / 2);
                localPos.y = Mathf.Clamp(localPos.y, -canvasRectTransform.rect.height / 2, canvasRectTransform.rect.height / 2);
                worldPosition = canvasRectTransform.TransformPoint(localPos);
                transform.position = worldPosition;
            }
            lastTouchPos = currentTouchPos;
        }*/

    void UpdateLevelManagerZoomState(float orthographicSize)
    {
        if (orthographicSize < maxOrthoSize / 1.4f)
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
