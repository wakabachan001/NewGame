using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        //if (this.gameObject.name == "Stage0Button") 
        //{
        //    int_StageNumber = 0;
        //}
        //else if(this.gameObject.name == "Stage1Button")
        //{
        //    int_StageNumber = 1;
        //}
        //else if (this.gameObject.name == "Stage2Button")
        //{
        //    int_StageNumber = 2;
        //}
        //else if (this.gameObject.name == "Stage3Button")
        //{
        //    int_StageNumber = 3;
        //}

        for (int i = 0; i < maxStages; i++) 
        {
            char_StageNumber = (char)(i + '0');
            if (gameObject.name.StartsWith("Stage" + char_StageNumber))
            {
                int_StageNumber = char_StageNumber - '0';
                break;
            }
               
        }

        Initiate.Fade("CommandSelect", Color.black, 1.5f);
    }
}
