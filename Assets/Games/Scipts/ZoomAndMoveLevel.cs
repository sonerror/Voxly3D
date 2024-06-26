using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomAndMoveLevel : MonoBehaviour
{
    public float zoomMobileSpeed = 0.001f;
    public float minOrthoSize = 1f;
    public float maxOrthoSize = 10f;
    public float dragSpeed = 0.1f;
    public float zoomWheelSpeed = 40f;
    private Vector2 firstTouchPrePos, secondTouchPrePos;
    private float touchesPrePosDiff, touchesCurPosDiff;
    private float zoomModifier;
    private Vector2? lastTouchPos = null;
    public Camera cam;
    public bool isZoomHand = false;

    void Start()
    {
        cam = Camera.main;
    }
    public void Onint()
    {
        cam.fieldOfView = maxOrthoSize;
    }
    public void SetTFDef()
    {
        cam.DOFieldOfView(maxOrthoSize  / 1.5f, 0.5f).SetEase(Ease.InOutQuad);
    }
    public void SetTFZoom(bool isZoomIN)
    {
        if (isZoomIN)
        {
            cam.DOFieldOfView(minOrthoSize, 0.5f).SetEase(Ease.InOutQuad);
        }
        else
        {
            cam.DOFieldOfView(maxOrthoSize, 0.5f).SetEase(Ease.InOutQuad);
        }
    }
    void Update()
    {
        if (LevelManager.Ins.isWin == true) return;
        // float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        /* if (LevelManager.Ins != null)
         {
             if (LevelManager.Ins.indexLevel == 0)
             {
                 return;
             }
         }*/
        isZoomHand = false;
        if (Input.touchCount == 2 && !UI_Hover.IsPointerOverUIElement())
        {
            isZoomHand = true;
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
                HandleMove(firstTouch, secondTouch);
            }
            cam.fieldOfView = Mathf.Clamp(cam.fieldOfView, minOrthoSize, maxOrthoSize);
            UpdateLevelManagerZoomState(cam.fieldOfView);
        }
        //float newOrthographicSize = cam.orthographicSize - scrollInput * zoomWheelSpeed * Time.deltaTime;
        //cam.orthographicSize = Mathf.Clamp(newOrthographicSize, minOrthoSize, maxOrthoSize);
        if (isZoomHand == false)
        {
            UpdateLevelManagerZoomState(cam.fieldOfView);
        }
    }

    void HandleZoom(float touchesPrePosDiff, float touchesCurPosDiff, Touch firstTouch, Touch secondTouch)
    {
        float deltaPos = (firstTouch.deltaPosition - secondTouch.deltaPosition).sqrMagnitude;
        deltaPos = Mathf.Clamp(deltaPos, 0, 400f);
        zoomModifier = deltaPos * zoomMobileSpeed * Time.deltaTime;

        if (touchesPrePosDiff < touchesCurPosDiff)
        {
            cam.fieldOfView -= zoomModifier;
        }
        else if (touchesPrePosDiff > touchesCurPosDiff)
        {
            cam.fieldOfView += zoomModifier;
        }
    }
    
    void HandleMove(Touch firstTouch, Touch secondTouch)
    {
        Vector2 firstTouchPrevPos = firstTouch.position - firstTouch.deltaPosition;
        Vector2 secondTouchPrevPos = secondTouch.position - secondTouch.deltaPosition;
        Vector2 prevTouchPosCenter = (firstTouchPrevPos + secondTouchPrevPos) / 2f;
        Vector2 currentTouchPosCenter = (firstTouch.position + secondTouch.position) / 2f;
        if (lastTouchPos.HasValue)
        {
            Vector2 delta = currentTouchPosCenter - prevTouchPosCenter;
            Vector3 moveDirection = new Vector3(delta.x, delta.y, 0f) * dragSpeed * Time.deltaTime;
            Vector3 newPosition = transform.position + moveDirection;
            Vector3 screenPosition = cam.WorldToViewportPoint(newPosition);
            screenPosition.x = Mathf.Clamp(screenPosition.x, 0.1f, 0.9f);
            screenPosition.y = Mathf.Clamp(screenPosition.y, 0.1f, 0.9f);
            newPosition = cam.ViewportToWorldPoint(screenPosition);
            transform.position = newPosition;
        }
        lastTouchPos = currentTouchPosCenter;
    }
    void UpdateLevelManagerZoomState(float orthographicSize)
    {
        if (orthographicSize < maxOrthoSize / 1.4f)
        {
            LevelManager.Ins.SetMatZoomIn();
            LevelManager.Ins.ChangematFormID();
            LevelManager.Ins.isChangeMatSl = true;
            LevelManager.Ins.SetBtnZoomOut();
        }
        else
        {
            LevelManager.Ins.isChangeMatSl = false;
            LevelManager.Ins.SetMatZoomOut();
            LevelManager.Ins.SetBtnZoomIn();
        }
    }
}