using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class Enemy : MonoBehaviour
{
    GameManager GM;
    EnemyTurn enemyturn;

    [SerializeField] int maxHP;
    [SerializeField] int HP;
    [SerializeField] int Strength;
    [SerializeField] int Vitality;

    bool[] action = new bool[5];
    public List<ItemName> enemycommand = new List<ItemName>();
    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        enemyturn = GameObject.Find("EnemyTurn").GetComponent<EnemyTurn>();
    }

    // Update is called once per frame
    void Update()
    {
        action = enemyturn.CommandType;

        if (GM.nowTURN == TURN.ENEMY_TURN)
        {
            enemyturn.CommandExecution();

            if (action[(int)ItemName.Sword] ||
                action[(int)ItemName.Hammer] ||
                action[(int)ItemName.Magic])
            {
                //  攻撃してそうなアニメーション
            }
            else if (action[(int)ItemName.Potion])
            {
                //  回復してそうなアニメーション
            }

            enemyturn.commandprehubCLONE[GM.TurnCount].GetComponent<CanvasGroup>().alpha = 0.2f;

            GM.nowTURN = TURN.PLAYER_TURN;
        }
    }
}
