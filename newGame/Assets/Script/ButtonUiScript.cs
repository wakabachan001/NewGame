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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BattleStart()
    {
        FindObjectOfType<GameManager>().SceneChange(GameScene.BATTLE);
    }
}
