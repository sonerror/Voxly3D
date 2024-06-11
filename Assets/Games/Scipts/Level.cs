using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public VoxelPiece[] voxelPieces;
    public int quantity;
    public bool isTutorialClick;
    public bool isTutorialRotate;
    public Transform container;
    void Start()
    {
        Onint();
    }
    public void Onint()
    {
        voxelPieces = GetComponentsInChildren<VoxelPiece>();
        quantity = voxelPieces.Length;
    }
   
}
