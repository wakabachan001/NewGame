using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class Enemy : MonoBehaviour
{
    GameManager GM;
    EnemyTurn enemyturn;

    [SerializeField] int maxHP;
    [SerializeField] public int HP;
    [SerializeField] int Strength;
    [SerializeField] int Defence;

    [SerializeField] int SwordRES;
    [SerializeField] int HammerRES;
    [SerializeField] int MagicRES;

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
        if (GM.string_nowScene == "BATTLE") 
        {
            action = enemyturn.CommandType;

            if (GM.nowTURN == TURN.ENEMY_TURN)
            {
                enemyturn.CommandExecution();

                if (action[(int)ItemName.Sword])
                {
                    //  攻撃してそうなアニメーション
                }
                else if (action[(int)ItemName.Hammer])
                {
                    //  攻撃してそうなアニメーション
                }
                else if (action[(int)ItemName.Magic])
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

    public void DamageCalc(string string_attackTYPE, int attackPower)
    {
        int int_attackTYPE = 0;

        if (string_attackTYPE == "Sword")
            int_attackTYPE = SwordRES;

        if (string_attackTYPE == "Hammer")
            int_attackTYPE = HammerRES;

        if (string_attackTYPE == "Magic")
            int_attackTYPE = MagicRES;

        HP = attackPower * (1 - (Defence + int_attackTYPE) / 100);
    }
}
