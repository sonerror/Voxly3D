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
    }
    public int CountVoxelPiecesWithID(int targetID)
    {
        return voxelPieces.Count(voxelPiece => voxelPiece.ID == targetID);
    }
    public int CountVoxel(int targetID)
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

}
