using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSwatch : MonoBehaviour
{
    public int id;
    public TextMeshProUGUI textID;
    public Image imgBG;
    public Button button;
    public void Start()
    {
        textID.text = id.ToString();
    }

    public void btnID()
    {
        LevelManager.Ins.CheckID(id);
        LevelManager.Ins.ChangematFormID();
    }
    public void SetBG(Color color)
    {
        imgBG.color = color;
    }
}