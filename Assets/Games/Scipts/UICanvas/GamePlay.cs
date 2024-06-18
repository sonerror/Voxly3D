using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GamePlay : UICanvas
{
    public ButtonSwatchCellUI buttonSwatchCellUI;
    public override void Open()
    {
        base.Open();
    }
    public override void CloseDirectly()
    {
        base.CloseDirectly();
    }
    public void btnID(int idInput)
    {
        LevelManager.Ins.CheckID(idInput);
        LevelManager.Ins.ChangematFormID();
    }
    public void BtnBack()
    {
        LevelManager.Ins.DestroyCurrentLevel();
        SceneController.Ins.ChangeScene("GamePlay", () =>
        {
           
            buttonSwatchCellUI.DestroyButton();
            UIManager.Ins.CloseAll();
            UIManager.Ins.OpenUI<Home>();
        }, true, true);
        LevelManager.Ins.iDSelected = 0;
        MatManager.Ins.ClearListID();
    }
}
