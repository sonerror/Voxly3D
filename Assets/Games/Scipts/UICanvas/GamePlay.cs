using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlay : UICanvas
{
    public ButtonSwatchCellUI buttonSwatchCellUI;
    public override void Open()
    {
        base.Open();
        Oninit();
    }
    public override void CloseDirectly()
    {
        base.CloseDirectly();
    }
    public void OnLoadNewScene()
    {
    }
    public void Oninit()
    {

    }
    public void btnID(int idInput)
    {
        LevelManager.Ins.CheckID(idInput);
        LevelManager.Ins.ChangematFormID();
    }
    public void BtnBack()
    {
        SceneController.Ins.ChangeScene("GamePlay", () => {
            UIManager.Ins.CloseAll();
            UIManager.Ins.OpenUI<Home>();
        }, true, true);
        LevelManager.Ins.iDSelected = 0;

    }
}
