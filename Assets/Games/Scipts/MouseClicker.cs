using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClicker : MonoBehaviour
{
    public VoxelPiece piece;
    public LayerMask layerMask;

    void Update()
    {
        if (LevelManager.Ins.isWin == true) return;
        if (Input.GetMouseButtonDown(0) && !UI_Hover.IsPointerOverUIElement() && Input.touchCount < 2)
        {
            StartDrawing();
        }
        if (Input.GetMouseButtonUp(0) && !UI_Hover.IsPointerOverUIElement() && Input.touchCount < 2)
        {
            StopDrawing();
        }
        if (Input.GetMouseButton(0) && LevelManager.Ins.areDrawing && !UI_Hover.IsPointerOverUIElement() && Input.touchCount < 2)
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
            if (piece != null && !piece.isVoxel && piece.ID == LevelManager.Ins.iDSelected)
            {
                StartCoroutine(IE_Draw(piece));
                if (LevelManager.Ins.isUsingBooster)
                {
                    ShootRaycasts(piece);
                }
            }
            
        }
    }
    IEnumerator IE_Draw(VoxelPiece voxelPiece)
    {
        yield return new WaitForEndOfFrame();

        if (voxelPiece != null && !voxelPiece.isVoxel && voxelPiece.ID == LevelManager.Ins.iDSelected)
        {
            LevelManager.Ins.areDrawing = true;
            MatManager.Ins.ChangeMat(voxelPiece);
            voxelPiece.isVoxel = true;
            LevelManager.Ins.CheckWinLose(voxelPiece.ID);
        }
    }

    IEnumerator IE_DrawByBooster(VoxelPiece voxelPiece)
    {
        yield return new WaitForEndOfFrame();
        LevelManager.Ins.areDrawing = true;
        MatManager.Ins.ChangeMat(voxelPiece);
        voxelPiece.isVoxel = true;
        LevelManager.Ins.CheckWinLose(voxelPiece.ID);

    }
    public void ShootRaycasts(VoxelPiece voxelPiece)
    {
        HashSet<VoxelPiece> visitedPieces = new HashSet<VoxelPiece>();
        Queue<VoxelPiece> piecesToProcess = new Queue<VoxelPiece>();
        piecesToProcess.Enqueue(voxelPiece);
        while (piecesToProcess.Count > 0)
        {
            VoxelPiece currentPiece = piecesToProcess.Dequeue();

            Vector3[] directions = { Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back };
            foreach (var direction in directions)
            {
                RaycastHit hit;
                if (Physics.Raycast(currentPiece.transform.position, direction, out hit, 0.01f))
                {
                    VoxelPiece hitPiece = hit.collider.GetComponentInParent<VoxelPiece>();
                    if (hitPiece != null && !hitPiece.isVoxel && hitPiece.ID == currentPiece.ID && !visitedPieces.Contains(hitPiece))
                    {
                        StartCoroutine(IE_DrawByBooster(hitPiece));
                        StartCoroutine(IE_DrawByBooster(currentPiece));
                        visitedPieces.Add(hitPiece);
                        piecesToProcess.Enqueue(hitPiece);
                    }
                }
            }
        }
    }

}
