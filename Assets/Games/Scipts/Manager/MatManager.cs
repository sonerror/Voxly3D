using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatManager : Singleton<MatManager>
{
    public MaterialAssetData materialAssetData;
    public MaterialAssetData materialNumber;
    public MaterialAssetData materialNumberSelect;
    public List<Material> matCurrent = new List<Material>();
    public void ChangeMat(VoxelPiece voxelPiece,int _id)
    {
        voxelPiece.mesh.material = materialAssetData.GetMatWithID(_id).mats;
    }
    public void ChangeMatNumber(VoxelPiece voxelPiece, int _id)
    {
        voxelPiece.mesh.material = materialNumber.GetMatWithID(_id).mats;
    }
    public void ChangeMatCurent(VoxelPiece voxelPiece, int _id)
    {
        voxelPiece.mesh.material = matCurrent[_id];
    }
    public void ChangeMatNumberSelectCurent(VoxelPiece voxelPiece, int _id)
    {
        voxelPiece.mesh.material = materialNumberSelect.GetMatWithID(_id).mats;
    }
}
