using CodeStage.AntiCheat.ObscuredTypes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "ScriptsTableObject/LevelData", order = 3)]

public class LevelData : ScriptableObject
{
    public int levelID;
    public List<TransformData> tfData;
    public List<MaterialData> materials;
}
[Serializable]
public class TransformData
{
    public Vector3 position;
    public int realColorID;
    public int defaultColorID;
    public TransformData(Vector3 position, int realColorID, int defaultColorID)
    {
        this.position = position;
        this.realColorID = realColorID;
        this.defaultColorID = defaultColorID;
    }
}
[System.Serializable]
public class MaterialData
{
    public Material material;
    public int colorID;
    public MaterialData(Material material, int colorId)
    {
        this.material = material;
        this.colorID = colorId;
    }
}