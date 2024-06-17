using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VoxelPiece : GameUnit
{
    public int ID;
    public MeshRenderer mesh;
    public Material material;
    public bool isVoxel = false;
    public void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        material = mesh.material;
    }
    public void Oninit()
    {
        LevelManager.Ins.ChangeMatCurrent(this);
    }
}