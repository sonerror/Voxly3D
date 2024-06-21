using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonZoom : MonoBehaviour
{
    public GamePlay gamePlay;
   
    public void Start()
    {
        gamePlay = GetComponentInParent<GamePlay>();
    }
    public void ZoomIN()
    {
        gamePlay.ToggleButtons(0, 1);
        LevelManager.Ins.levelCurrent.ZoomInLevel();
    }
    public void ZoomOut()
    {
        gamePlay.ToggleButtons(1, 0);
        LevelManager.Ins.levelCurrent.ZoomOutLevel();
    }
}
