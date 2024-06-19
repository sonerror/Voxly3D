using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSwatchCellUI : MonoBehaviour
{
    public ButtonSwatch buttonSwatch;
    public Transform tf;
    public List<ButtonSwatch> buttonSwatches;
    public RectTransform tfContent;
    public void LoadData()
    {
        DestroyButton();
        for (int i = 0; i < MatManager.Ins.listIDBtn.Count; i++)
        {
            CreateAndConfigureButton(MatManager.Ins.listIDBtn[i]);
        }
        StartCoroutine(IE_DelayTime());
    }
    IEnumerator IE_DelayTime()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        SetPosScroll(LevelManager.Ins.iDSelected);
        yield return new WaitForEndOfFrame();
        SelectButton();
    }
    void CreateAndConfigureButton(MaterialData materialData)
    {
        ButtonSwatch btn = Instantiate(buttonSwatch, tf);
        btn.id = materialData.colorID;

        Color newColor = materialData.material.color;
        newColor.a -= 0.3f;

        btn.imgBG.color = newColor;
        btn.color = newColor;
        btn.Onint(this);

        buttonSwatches.Add(btn);
    }
    public void DestroyButton()
    {
        if (buttonSwatches.Count > 0)
        {
            foreach (var buttonSwatch in buttonSwatches)
            {
                Destroy(buttonSwatch.gameObject);
            }
            buttonSwatches.Clear();
        }
    }

    public void ResetUI(int id)
    {
        SetPosScroll(id);
    }
    public void SetPosScroll(int targetButtonIndex)
    {
        if (tfContent != null && buttonSwatches.Count > 0)
        {
            RectTransform buttonRectTransform = buttonSwatches[0].GetComponent<RectTransform>();
            float buttonWidth = buttonRectTransform.rect.width;
            float totalWidth = buttonWidth * buttonSwatches.Count;
            tfContent.sizeDelta = new Vector2(totalWidth, tfContent.sizeDelta.y);
            if (targetButtonIndex > 0 && targetButtonIndex < buttonSwatches.Count)
            {
                RectTransform targetButtonRectTransform = buttonSwatches.Find(id =>id.id == targetButtonIndex).GetComponent<RectTransform>();
                float targetButtonXPos = targetButtonRectTransform.anchoredPosition.x + 100;
                float normalizedPosition = Mathf.Clamp01(1 - (targetButtonXPos / (totalWidth - tfContent.rect.width)));
                ScrollRect scrollRect = tfContent.GetComponentInParent<ScrollRect>();
                scrollRect.horizontalNormalizedPosition = normalizedPosition;
            }
        }
    }
    public void SelectButton()
    {
        if (LevelManager.Ins.iDSelected != 0)
        {
            for (int i = 0; i < buttonSwatches.Count; i++)
            {
                if (buttonSwatches[i].id == LevelManager.Ins.iDSelected)
                {
                    buttonSwatches[i].MoveUp();
                }
                else
                {
                    buttonSwatches[i].MoveDown();
                }
            }
        }
    }
}
