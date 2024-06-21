using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GamePlay : UICanvas
{
    public ButtonSwatchCellUI buttonSwatchCellUI;
    public TextMeshProUGUI countDownText;
    public List<ButtonZoom> listB = new List<ButtonZoom>();
    public void ToggleButtons(int indexToDeactivate, int indexToActivate)
    {
        listB[indexToDeactivate].gameObject.SetActive(false);
        listB[indexToActivate].gameObject.SetActive(true);
    }
    private void OnEnable()
    {
        EventManager.OnLoadNewScene += OnLoadNewScene;
    }

    private void OnDisable()
    {
        EventManager.OnLoadNewScene -= OnLoadNewScene;
    }
    public void OnLoadNewScene()
    {
        buttonSwatchCellUI.DestroyButton();
    }
    public override void Open()
    {
        base.Open();
    }
    public void Update()
    {
        UpdateCountDownText();
    }
    public override void CloseDirectly()
    {
        base.CloseDirectly();
    }
    public void BtnBack()
    {
        SceneController.Ins.ChangeScene("GamePlay", () =>
        {
            UIManager.Ins.CloseAll();
            UIManager.Ins.OpenUI<Home>();
        }, true, true);
        LevelManager.Ins.iDSelected = 0;
        MatManager.Ins.ClearListID();
    }
    private void UpdateCountDownText()
    {
        if (LevelManager.Ins.levelCurrent == null) return;
        float currentTime = LevelManager.Ins.levelCurrent.countDownTime;
        int minute = (int)currentTime / 60;
        int second = (int)currentTime % 60;
        string minuteString = minute.ToString();
        string secondString = second.ToString();
        if (minuteString.Length < 2)
        {
            minuteString = "0" + minuteString;
        }
        if (secondString.Length < 2)
        {
            secondString = "0" + secondString;
        }
        countDownText.text = minuteString + ":" + secondString;
    }

    

}
