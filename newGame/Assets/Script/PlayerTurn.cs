using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerTurn : MonoBehaviour
{
    GameManager gamemanager;
    public GameObject[] PlayerTurnCommand = new GameObject[6];
    public GameObject[] commandprehub = new GameObject[5];
    public GameObject[] commandprehubCLONE = new GameObject[6];

    // Start is called before the first frame update
    void Start()
    {
        gamemanager = GameObject.Find("GameManager").GetComponent<GameManager>();

        for (int i = 0; i < 6; i++) 
        {
            if (gamemanager.a[i] == GameManager.ItemName.Sword)
            {
                CLONE_PLAYERCOMMAND(1, i);
            }
           else if (gamemanager.a[i] == GameManager.ItemName.Hammer)
            {
                CLONE_PLAYERCOMMAND(2, i);
            }
            else if (gamemanager.a[i] == GameManager.ItemName.Magic)
            {
                CLONE_PLAYERCOMMAND(3, i);
            }
            else if (gamemanager.a[i] == GameManager.ItemName.Potion)
            {
                CLONE_PLAYERCOMMAND(4, i);
            }
        }
    }

    void CLONE_PLAYERCOMMAND(int prehubNUM,int arrayNUM)
    {
        commandprehubCLONE[arrayNUM] = Instantiate(commandprehub[prehubNUM], PlayerTurnCommand[arrayNUM].GetComponent<RectTransform>());
        commandprehubCLONE[arrayNUM].GetComponent<RectTransform>().position = PlayerTurnCommand[arrayNUM].GetComponent<RectTransform>().position;
        commandprehubCLONE[arrayNUM].GetComponent<RectTransform>().localScale = commandprehub[prehubNUM].GetComponent<RectTransform>().localScale * 0.8f;

    }
}
