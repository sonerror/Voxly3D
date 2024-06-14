using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClicker : MonoBehaviour
{
    public Vector3 mousePos;
    public VoxelPiece piece;
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !UI_Hover.IsPointerOverUIElement()) // Nhấn chuột
        {
            StartDrawing();
        }

        if (Input.GetMouseButtonUp(0) && !UI_Hover.IsPointerOverUIElement()) // Nhả chuột
        {
            StopDrawing();
        }

        if (Input.GetMouseButton(0) && LevelManager.Ins.areDrawing && !UI_Hover.IsPointerOverUIElement())
        {
            ContinueDrawing();
        }
    }

    void StartDrawing()
    {
        RaycastAndDraw();

    }

    void StopDrawing()
    {
        LevelManager.Ins.areDrawing = false;
    }

    void ContinueDrawing()
    {
        RaycastAndDraw();
    }

    void RaycastAndDraw()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        LayerMask mask = LayerMask.GetMask("Voxel");
        Debug.DrawRay(ray.origin, ray.direction * 100f, Color.green, 0.5f);
        if (Physics.Raycast(ray, out hit, 1000f, mask))
        {
            piece = hit.collider.GetComponentInParent<VoxelPiece>();
            StartCoroutine(IE_Draw());
        }
    }
    IEnumerator IE_Draw()
    {
        yield return new WaitForEndOfFrame();
        if (piece.isVoxel != true && piece.ID == LevelManager.Ins._IDSelected)
        {
            LevelManager.Ins.areDrawing = true;
        }

        if (piece != null && piece.ID == LevelManager.Ins._IDSelected)
        {
            MatManager.Ins.ChangeMat(piece, LevelManager.Ins._IDSelected);
            piece.isVoxel = true;
            LevelManager.Ins.CheckWinLose(piece.ID);
        }
    }
}
