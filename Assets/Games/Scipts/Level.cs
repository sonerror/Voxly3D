using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Level : MonoBehaviour
{
    public List<VoxelPiece> voxelPieces;

    public int quantity;
    public bool isTutorialClick;
    public bool isTutorialRotate;
    public Transform container;
    public float countDownTime = 180f;
    public bool isCountDown;
    public Animator animator;
    public ZoomAndMoveLevel zoomAndMoveLevel;
    public void Start()
    {
        zoomAndMoveLevel = GetComponent<ZoomAndMoveLevel>();
        animator.enabled = false;
    }
    private void Update()
    {
        if (!isCountDown || countDownTime < 0) return;
        countDownTime -= Time.deltaTime;
        if (countDownTime < 0)
        {
            LevelManager.Ins.CheckWinloseTimer();
            isCountDown = false;
        }
    }

    public void Onint()
    {
        voxelPieces = new List<VoxelPiece>(GetComponentsInChildren<VoxelPiece>());
        quantity = voxelPieces.Count;
        isCountDown = true;
        animator.enabled = false;
    }
    public int CountVoxelPiecesWithID(int targetID)
    {
        return voxelPieces.Count(voxelPiece => voxelPiece.ID == targetID);
    }
    public int CountVoxel(int targetID)
    {
        return voxelPieces.Count(voxelPiece => voxelPiece.ID == targetID && !voxelPiece.isVoxel);
    }
    public float FCountVoxel(int targetID)
    {
        return voxelPieces.Count(voxelPiece => voxelPiece.ID == targetID && !voxelPiece.isVoxel);
    }
    public int CountSumVoxel()
    {
        return voxelPieces.Count(voxelPiece => !voxelPiece.isVoxel);
    }
    public void StopCountDown()
    {
        isCountDown = false;
    }
    public void ChangeAnim()
    {
        zoomAndMoveLevel.SetTFDef();
        this.transform.DORotate(Vector3.zero, 0.8f).SetEase(Ease.InOutQuad);
        DOVirtual.DelayedCall(1f, () =>
        {
            animator.enabled = true;
            animator.Play("Win");
        });
    }
    public void ZoomInLevel()
    {
        zoomAndMoveLevel.SetTFZoom(true);
    }
    public void ZoomOutLevel()
    {
        zoomAndMoveLevel.SetTFZoom(false);

    }
    public void BoosterPaintObject(VoxelPiece voxelPiece)
    {

    }
    public void ShootRaycasts(VoxelPiece voxelPiece)
    {
        Vector3[] directions = { Vector3.up, Vector3.down, Vector3.left, Vector3.right };
        foreach (var direction in directions)
        {
            RaycastHit hit;
            if (Physics.Raycast(voxelPiece.transform.position, direction, out hit))
            {
                Debug.LogError("hit");
            }
        }
    }

}
