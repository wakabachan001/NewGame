using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class EnemyTurn : MonoBehaviour
{
    GameManager gamemanager;
    Enemy enemy;
    public GameObject[] EnemyTurnCommand = new GameObject[6];
    public GameObject[] commandprehub = new GameObject[5];
    public GameObject[] commandprehubCLONE = new GameObject[6];

    public bool[] CommandType = new bool[5];

    // Start is called before the first frame update
    void Start()
    {
        gamemanager = GameObject.Find("GameManager").GetComponent<GameManager>();

        Invoke("EnemyInstantiate", 0.5f);
    }

    void EnemyInstantiate()
    {
        enemy = GameObject.FindWithTag("Enemy").GetComponent<Enemy>();

        for (int i = 0; i < 6; i++)
        {
            if (enemy.enemycommand[i] == ItemName.Sword)
            {
                CLONE_ENEMYCOMMAND(1, i);
            }
            else if (enemy.enemycommand[i] == ItemName.Hammer)
            {
                CLONE_ENEMYCOMMAND(2, i);
            }
            else if (enemy.enemycommand[i] == ItemName.Magic)
            {
                CLONE_ENEMYCOMMAND(3, i);
            }
            else if (enemy.enemycommand[i] == ItemName.Potion)
            {
                CLONE_ENEMYCOMMAND(4, i);
            }
        }
    }

    void CLONE_ENEMYCOMMAND(int prehubNUM, int arrayNUM)
    {
        commandprehubCLONE[arrayNUM] = Instantiate(commandprehub[prehubNUM], EnemyTurnCommand[arrayNUM].GetComponent<RectTransform>());
        commandprehubCLONE[arrayNUM].GetComponent<RectTransform>().position = EnemyTurnCommand[arrayNUM].GetComponent<RectTransform>().position;
        commandprehubCLONE[arrayNUM].GetComponent<RectTransform>().localScale = commandprehub[prehubNUM].GetComponent<RectTransform>().localScale * 0.8f;

    }

    public void CommandExecution()
    {
        switch (gamemanager.TurnCount)
        {
            case 0:
                CommandCheck(gamemanager.TurnCount);
                break;
            case 1:
                CommandCheck(gamemanager.TurnCount);
                break;
            case 2:
                CommandCheck(gamemanager.TurnCount);
                break;
            case 3:
                CommandCheck(gamemanager.TurnCount);
                break;
            case 4:
                CommandCheck(gamemanager.TurnCount);
                break;
            case 5:
                CommandCheck(gamemanager.TurnCount);
                break;

            default:
                break;
        }
    }

    public void CommandCheck(int commandNUM)
    {
        for (int i = 0; i < 5; i++)
            CommandType[i] = false;

        if (commandprehubCLONE[commandNUM] != null) 
        {
            if (commandprehubCLONE[commandNUM].name.StartsWith("Sword"))
            {
                CommandType[(int)ItemName.Sword] = true;
            }
            else if (commandprehubCLONE[commandNUM].name.StartsWith("Hammer"))
            {
                CommandType[(int)ItemName.Hammer] = true;
            }
            else if (commandprehubCLONE[commandNUM].name.StartsWith("Magic"))
            {
                CommandType[(int)ItemName.Magic] = true;
            }
            else if (commandprehubCLONE[commandNUM].name.StartsWith("Potion"))
            {
                CommandType[(int)ItemName.Potion] = true;
            }

        }

    }
}
