using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Home : UICanvas
{
    public override void Open()
    {
        base.Open();
    }

    private void Update()
    {
    }
    public void BtnPlay()
    {
        SceneController.Ins.ChangeScene("GamePlay", () =>
        {
            UIManager.Ins.CloseUI<Home>();
            UIManager.Ins.OpenUI<GamePlay>();
            UIManager.Ins.bg.gameObject.SetActive(false);
        }, true, true);
    }

}
