using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Loading : UICanvas
{
    public Slider slider;

    public override void Open()
    {
        base.Open();
        RunSlider();
    }

    public void RunSlider()
    {
        float duration = 2f;
        DOVirtual.Float(0, 1, duration, (value) =>
        {
            slider.value = value;
        }).SetEase(Ease.InCubic)
        .OnComplete(() =>
        {
            slider.value = 1f;
            DOVirtual.DelayedCall(0.5f, () =>
            {
                SceneController.Ins.ChangeScene("GamePlay", () =>
                {
                    UIManager.Ins.bg.gameObject.SetActive(false);
                    UIManager.Ins.OpenUI<Home>();
                    UIManager.Ins.CloseUI<Loading>();
                }, true, true);
            });
        });
    }
}
