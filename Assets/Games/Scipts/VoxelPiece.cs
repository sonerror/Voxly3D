using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VoxelPiece : GameUnit
{
    public int ID { get; private set; }
    public List<TextMeshProUGUI> textIDUI;
    public Material material;
    private void Start()
    {
        ID = Random.Range(1, 4);
        for (int i = 0; i < textIDUI.Count; i++)
        {
            textIDUI[i].text = ID.ToString();
        }
    }
}
