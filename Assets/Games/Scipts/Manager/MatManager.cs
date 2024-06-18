using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatManager : Singleton<MatManager>
{
    public MaterialAssetData materialAssetData;
    public MaterialAssetData materialNumber;
    public List<Material> matCurrent = new List<Material>();
    public List<MaterialData> listID = new List<MaterialData>();
    public void ChangeMat(VoxelPiece voxelPiece)
    {
        voxelPiece.mesh.material = listID.Find(m => m.colorID == voxelPiece.ID).material;
    }
    public void ChangeMatNumber(VoxelPiece voxelPiece, int _id)
    {
        voxelPiece.mesh.material = materialNumber.GetMatWithID(_id).mats;
    }
    public void ChangeMatCurent(VoxelPiece voxelPiece, int _id)
    {
        voxelPiece.mesh.material = matCurrent[_id];
    }
    public void ClearListID()
    {
        if (listID != null)
        {
            listID.Clear();
        }
    }
}
