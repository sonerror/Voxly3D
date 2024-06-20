using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatManager : Singleton<MatManager>
{
    public MaterialAssetData materialNumber;
    public List<MaterialData> listIDBtn = new List<MaterialData>();
    public List<MaterialData> listID = new List<MaterialData>();
    
    public void ChangeMat(VoxelPiece voxelPiece) // doi mau so thanh mau da to
    {
        voxelPiece.mesh.material = listID.Find(m => m.colorID == voxelPiece.ID).material;
    }
    public void ChangeMatNumber(VoxelPiece voxelPiece, int _id) // doi mau trang den thanh so 
    {
        voxelPiece.mesh.material = materialNumber.GetMatWithID(_id).mats;
    }
    public void ChangeMatCurent(VoxelPiece voxelPiece)// gan mau trang den luc tao ra level
    {
        Color cl = listID.Find(m => m.colorID == voxelPiece.ID).material.color;
        ConvertToGrayscale(cl);
        voxelPiece.mesh.material.color = ConvertToGrayscale(cl);
    }
    public void ClearListID()
    {
        if (listIDBtn != null)
        {
            listIDBtn.Clear();
        }
    }
    Color ConvertToGrayscale(Color color)// doi mau trang den
    {
        float gray = color.r * 0.299f + color.g * 0.587f + color.b * 0.114f;
        return new Color(gray, gray, gray, color.a);
    }
}
