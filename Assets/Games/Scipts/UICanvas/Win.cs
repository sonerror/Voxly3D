using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : UICanvas
{
    public override void Open()
    {
        base.Open();
       
    }
    public void btn_NextLevel()
    {
        UIManager.Ins.CloseAll();
        LevelManager.Ins.NextLevel();
    }
    public void Btn_Home()
    {

    }
}
