using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Home : UICanvas
{
    public override void Open()
    {
        base.Open();
    }
    public void OnLoadNewScene()
    {
        UIManager.Ins.CloseUI<Home>();
        UIManager.Ins.OpenUI<GamePlay>();
        if (SceneController.Ins.currentSceneName.Equals("GamePlay") && !UIManager.Ins.IsOpened<Home>())
        {
            LevelManager.Ins.LoadLevel();
        }
    }
}
