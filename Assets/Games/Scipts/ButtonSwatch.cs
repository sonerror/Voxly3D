using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ButtonSwatch : MonoBehaviour
{
    public int id;
    public TextMeshProUGUI textID;
    public void Start()
    {
        textID.text = id.ToString();
    }
    public void btnID()
    {
        LevelManager.Ins.CheckID(id);
        LevelManager.Ins.ChangematFormID();
        SelectButton();
    }

    public void SelectButton()
    {
        if (LevelManager.Ins.iDSelected != 0)
        {
            if (LevelManager.Ins.iDSelected == id)
            {
                gameObject.transform.localScale *= 1.5f;
                gameObject.transform.localPosition += new Vector3(0, 1, 0);
            }
        }
    }
}