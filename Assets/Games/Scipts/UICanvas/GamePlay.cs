using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlay : UICanvas
{
    public Transform tfButton;
    public override void Open()
    {
        base.Open();
    }
    public void Oninit()
    {

    }


    public void btnID(int idInput)
    {
        LevelManager.Ins.CheckID(idInput);
        LevelManager.Ins.ChangematFormID();
    }
}
