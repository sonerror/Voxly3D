using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlay : UICanvas
{
    public List<Button> buttons = new List<Button>();
    private void OnEnable()
    {
        EventManager.OnLoadNewScene += OnLoadNewScene;
    }

    private void OnDisable()
    {
        EventManager.OnLoadNewScene -= OnLoadNewScene;
    }
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
    }
}
