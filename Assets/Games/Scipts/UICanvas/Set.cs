using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Set : UICanvas
{
    public TMP_InputField _InputField;
    public void btn_SetVal()
    {
        string input = _InputField.text;
        if (float.TryParse(input, out float newZoomMobileSpeed))
        {
            LevelManager.Ins.levelCurrent.zoomAndMoveLevel.zoomMobileSpeed = newZoomMobileSpeed;
        }
        ExitSet();
    }
    public void ExitSet()
    {
        UIManager.Ins.CloseUI<Set>();
    }
}
