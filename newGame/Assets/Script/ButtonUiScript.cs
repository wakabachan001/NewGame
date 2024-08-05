using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class ButtonUiScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    public void goTitle()
    {
        FindObjectOfType<GameManager>().SceneChange(GameScene.TITLE);
    }
    public void goStageSelect()
    {
        FindObjectOfType<GameManager>().SceneChange(GameScene.STAGESELECT);
    }
    public void goCommandSelect()
    {
        FindObjectOfType<GameManager>().SceneChange(GameScene.COMMANDSELECT);
    }
    public void goBattle()
    {
        FindObjectOfType<GameManager>().SceneChange(GameScene.BATTLE);
    }
    public void endGame()
    {
        
    }
}
