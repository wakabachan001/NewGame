using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerTurn : MonoBehaviour
{
    public GameObject[] pCommand = new GameObject[6];
    private GameObject[] commandOBJ = new GameObject[0];

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < GameManager.CommandList.Count; i++) 
        {
            commandOBJ[i] = Instantiate(GameManager.CommandList[i], pCommand[i].GetComponent<RectTransform>());
            commandOBJ[i].GetComponent<RectTransform>().position = pCommand[i].GetComponent<RectTransform>().position;
        }
    }
}
