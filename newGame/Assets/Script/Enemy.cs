using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class Enemy : MonoBehaviour
{
    GameManager GM;
    EnemyTurn enemyturn;
    Player player;

    [SerializeField] int maxHP;
    [SerializeField] public int HP;
    [SerializeField] int Strength;
    [SerializeField] int Defence;

    [SerializeField] int SwordRES;
    [SerializeField] int HammerRES;
    [SerializeField] int MagicRES;

    bool[] action = new bool[5];

    bool InAction = false;
    public List<ItemName> enemycommand = new List<ItemName>();
    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        enemyturn = GameObject.Find("EnemyTurn").GetComponent<EnemyTurn>();
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GM.string_nowScene == "BATTLE")
        {
            action = enemyturn.CommandType;

            if (GM.nowTURN == TURN.ENEMY_TURN && !InAction)
            {
                InAction = true;

                Invoke("Action", 3.0f);
            }
        }
        else
            enabled = false;
    }

    void Action()
    {
        //  Battleシーンに移動した瞬間に処理が作動しないように遅延をかける
        //if (GM.TurnCount == 0)
        //    StartCoroutine(GM.WaitTimer(3.0f, false));

        enemyturn.CommandExecution();

        if (action[(int)ItemName.Sword])
        {
            //  攻撃してそうなアニメーション
            player.DamageCalc("Sword", Strength);
            player.AttackedByTheEnemy = true;
        }
        else if (action[(int)ItemName.Hammer])
        {
            //  攻撃してそうなアニメーション
            player.DamageCalc("Hammer", Strength);
            player.AttackedByTheEnemy = true;
        }
        else if (action[(int)ItemName.Magic])
        {
            //  攻撃してそうなアニメーション
            player.DamageCalc("Magic", Strength);
            player.AttackedByTheEnemy = true;
        }
        else if (action[(int)ItemName.Potion])
        {
            //  回復してそうなアニメーション
        }

        if (enemyturn.commandprehubCLONE[GM.TurnCount] != null)
            enemyturn.commandprehubCLONE[GM.TurnCount].GetComponent<CanvasGroup>().alpha = 0.2f;

        //StartCoroutine(GM.WaitTimer(3.0f, true));
        GM.nowTURN = TURN.PLAYER_TURN;
        InAction = false;
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

        HP -= attackPower * (1 - (Defence + int_attackTYPE) / 100);
    }
}
