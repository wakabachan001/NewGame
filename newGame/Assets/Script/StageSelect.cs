using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class StageSelect : MonoBehaviour
{
    public int maxStages = 4;

    static public int int_StageNumber = 0;

    private  char char_StageNumber = '0';
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        for (int i = 0; i < maxStages; i++) 
        {
            char_StageNumber = (char)(i + '0');
            if (gameObject.name.StartsWith("Stage" + char_StageNumber))
            {
                int_StageNumber = char_StageNumber - '0';
                break;
            }
        }

        FindObjectOfType<GameManager>().SceneChange(GameScene.COMMANDSELECT);
    }
}
