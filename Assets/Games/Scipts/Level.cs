using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Level : MonoBehaviour
{
    public VoxelPiece[] voxelPieces;
    public int quantity;
    public bool isTutorialClick;
    public bool isTutorialRotate;
    public Transform container;
    public int ID_1 = 0;
    void Start()
    {
        Onint();
    }
    public void Onint()
    {
        voxelPieces = GetComponentsInChildren<VoxelPiece>();
        quantity = voxelPieces.Length;
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
}
