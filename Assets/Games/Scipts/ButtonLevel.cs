using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonLevel : MonoBehaviour
{
    public ButtonLevelCellUI buttonLevelCellUI;
    public List<ButtonLevelCellUI> buttonLevelCellUIs;
    public Transform tfButton;
    public RectTransform tfContent;
    private void Start()
    {
        Loadata(LevelManager.Ins.indexLevel);
    }
    public void Loadata(int targetButton)
    {
        if(buttonLevelCellUIs == null)
        {
            buttonLevelCellUIs = new List<ButtonLevelCellUI>();
        }
        for (int i = 0; i < LevelManager.Ins.levelDataTFAssetData.levelDataTFDataModels.Count; i++)
        {
            ButtonLevelCellUI btn = Instantiate(buttonLevelCellUI, tfButton);
            buttonLevelCellUIs.Add(btn);
            btn.LoadData(LevelManager.Ins.levelDataTFAssetData.levelDataTFDataModels[i].id);
        }
        SetSizeDetal(targetButton);
    }
    public void SetSizeDetal(int targetButtonIndex)
    {
        if (tfContent != null && buttonLevelCellUIs.Count > 0)
        {
            RectTransform buttonRectTransform = buttonLevelCellUIs[0].GetComponent<RectTransform>();
            float buttonWidth = buttonRectTransform.rect.height + 100;
            float totalWidth = buttonWidth * buttonLevelCellUIs.Count;

            tfContent.sizeDelta = new Vector2(tfContent.sizeDelta.x, totalWidth);

            if (targetButtonIndex >= 0 && targetButtonIndex < buttonLevelCellUIs.Count)
            {
                RectTransform targetButtonRectTransform = buttonLevelCellUIs[targetButtonIndex].GetComponent<RectTransform>();
                float targetButtonYPos = targetButtonRectTransform.anchoredPosition.y;

                float normalizedPosition = Mathf.Clamp01(1 - (targetButtonYPos / (totalWidth - tfContent.rect.height)));
                ScrollRect scrollRect = tfContent.GetComponentInParent<ScrollRect>();
                scrollRect.verticalNormalizedPosition = normalizedPosition;
            }
        }
    }
   
}
