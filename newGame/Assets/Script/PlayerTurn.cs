using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerTurn : CommandDropArea
{
    public GameObject[] pCommand = new GameObject[6];
    private GameObject commandOBJ;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < CommandList.Count; i++) 
        {
            commandOBJ = Instantiate(CommandList[i], pCommand[i].GetComponent<RectTransform>());
            commandOBJ.GetComponent<RectTransform>().position = pCommand[i].GetComponent<RectTransform>().position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
