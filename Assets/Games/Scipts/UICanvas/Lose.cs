using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lose : UICanvas
{
    public override void Open()
    {
        base.Open();
    }
    public void BtnBack()
    {
        LevelManager.Ins.levelCurrent.StopCountDown();
        SceneController.Ins.ChangeScene("GamePlay", () =>
        {
            UIManager.Ins.CloseAll();
            UIManager.Ins.OpenUI<Home>();
        }, true, true);
    }
}
