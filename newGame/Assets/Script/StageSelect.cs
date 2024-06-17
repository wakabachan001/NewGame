using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelect : MonoBehaviour
{
    static public int StageNumber = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(StageNumber);
    }

    public void OnClick()
    {
        if (this.gameObject.name == "Stage0Button") 
        {
            StageNumber = 0;
        }
        else if(this.gameObject.name == "Stage1Button")
        {
            StageNumber = 1;
        }
        else if (this.gameObject.name == "Stage2Button")
        {
            StageNumber = 2;
        }
        else if (this.gameObject.name == "Stage3Button")
        {
            StageNumber = 3;
        }

        Initiate.Fade("CommandSelect", Color.black, 1.5f);
    }
}
