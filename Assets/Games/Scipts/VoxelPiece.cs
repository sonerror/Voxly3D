using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VoxelPiece : GameUnit
{
    public int ID;
    public List<TextMeshProUGUI> textIDUI;
    public MeshRenderer mesh;
    public bool isVoxel = false;
    public void Oninit()
    {
        LevelManager.Ins.ChangeMatCurrent(this);
    }
    
}
