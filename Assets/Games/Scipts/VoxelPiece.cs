using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VoxelPiece : GameUnit
{
    public int ID;
    public MeshRenderer mesh;
    public bool isVoxel = false;
   
    public void Oninit()
    {
        isVoxel = false;
        LevelManager.Ins.ChangeMatCurrent(this);
    }
    
}