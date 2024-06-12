using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VoxelPiece : GameUnit
{
    public int ID { get; private set; }
    public List<TextMeshProUGUI> textIDUI;
    public MeshRenderer mesh;
    public bool isVoxel = false;
    private void Start()
    {
        
    }
    public void Oninit()
    {
        ID = Random.Range(1, 4);
        for (int i = 0; i < textIDUI.Count; i++)
        {
            textIDUI[i].text = ID.ToString();
        }
    }
    public void ChangeMat()
    {

    }
}
