using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClicker : MonoBehaviour
{
    public Vector3 mousePos;
    public VoxelPiece piece;
    public LayerMask layerMask;
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
       
        Debug.DrawRay(ray.origin, ray.direction * 100f, Color.green, 0.5f);
        if (Physics.Raycast(ray, out hit, 1000f, layerMask))
        {
            piece = hit.collider.GetComponentInParent<VoxelPiece>();
            StartCoroutine(IE_Draw(piece));
        }
    }
    IEnumerator IE_Draw(VoxelPiece voxelPiece)
    {
        yield return new WaitForEndOfFrame();
        if (voxelPiece != null)
        {
            if (voxelPiece.isVoxel != true && voxelPiece.ID == LevelManager.Ins.iDSelected)
            {
                LevelManager.Ins.areDrawing = true;
            }
            if (voxelPiece != null && voxelPiece.ID == LevelManager.Ins.iDSelected)
            {
                MatManager.Ins.ChangeMat(voxelPiece);
                voxelPiece.isVoxel = true;
                LevelManager.Ins.CheckWinLose(voxelPiece.ID);
            }
        }
    }
}
