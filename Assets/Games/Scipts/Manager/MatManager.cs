using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatManager : Singleton<MatManager>
{
    public MaterialAssetData materialAssetData;


    public void ChangeMat(VoxelPiece voxelPiece,int _id)
    {
        voxelPiece.mesh.material = materialAssetData.GetMatWithID(_id).mats;
    }
}
