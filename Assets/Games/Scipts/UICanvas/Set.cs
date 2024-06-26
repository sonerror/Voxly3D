using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Set : UICanvas
{
    public TMP_InputField _InputFieldZoom;
    public TMP_InputField _InputFieldRotate;
    public TMP_InputField _InputFieldMove;
    public void btn_SetVal()
    {
        string inputZoom = _InputFieldZoom.text;
        string inputRotate = _InputFieldRotate.text;
        string inputMove = _InputFieldMove.text;


        if (float.TryParse(inputZoom, out float newZoomMobileSpeed))
        {
            LevelManager.Ins.levelCurrent.zoomAndMoveLevel.zoomMobileSpeed = newZoomMobileSpeed;
        }
        if (float.TryParse(inputRotate, out float newRotateMobileSpeed))
        {
            LevelManager.Ins.levelCurrent.rotateAround.rotateSpeed = newRotateMobileSpeed;
        }
        if (float.TryParse(inputMove, out float newMoveMobileSpeed))
        {
            LevelManager.Ins.levelCurrent.zoomAndMoveLevel.dragSpeed = newMoveMobileSpeed;
        }
        ExitSet();
    }

    public void SetVal(string _input,float val)
    {

        if (float.TryParse(_input, out float newValu))
        {
          val = newValu;
        }
    }
    public void ExitSet()
    {
        UIManager.Ins.CloseUI<Set>();
    }
}
