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
    public int ID_1 = 0;
 
    public void Onint()
    {
        voxelPieces = new List<VoxelPiece>(GetComponentsInChildren<VoxelPiece>());
        quantity = voxelPieces.Count;
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
