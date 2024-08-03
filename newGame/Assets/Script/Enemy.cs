using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static GameManager;

public class Enemy : MonoBehaviour
{
    GameManager GM;
    EnemyTurn enemyturn;
    Player player;
    Slider slider;

    public GameObject HealBoxOBJ;

    [SerializeField] int maxHP;
    [SerializeField] float HP;
    [SerializeField] int Strength;
    [SerializeField] int Defence;
    [SerializeField] int HealAmount;

    [SerializeField] int SwordRES;
    [SerializeField] int HammerRES;
    [SerializeField] int MagicRES;

    bool[] action = new bool[5];

    bool canprocess = true;
    bool InAction = false;
    public List<ItemName> enemycommand = new List<ItemName>();
    public bool AttackedByThePlayer = false;
    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        enemyturn = GameObject.Find("EnemyTurn").GetComponent<EnemyTurn>();

        if(GM.string_nowScene == "BATTLE"){
            player = GameObject.FindWithTag("Player").GetComponent<Player>();
            slider = GameObject.Find("EnemyHPBar").GetComponent<Slider>();
        }

        HP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        if (GM.string_nowScene == "BATTLE")
        {
            slider.value = HP / maxHP;
            action = enemyturn.CommandType;

            if (GM.nowTURN == TURN.ENEMY_TURN && !InAction)
            {
                InAction = true;

                Invoke("Action", GM.turnSpeed / GM.turnSpeedMultiplier);
            }
        }
        else
            enabled = false;

        if ((int)HP < 1 && canprocess)
        {
            canprocess = false;
            GM.SceneChange(GameScene.GAMECLEAR);
        }
    }

    void Action()
    {
        enemyturn.CommandExecution();

        if (action[(int)ItemName.Sword])
        {
            //  攻撃してそうなアニメーション
            player.DamageCalc("Sword", Strength);
            player.hasTakenDamage = true;
        }
        else if (action[(int)ItemName.Hammer])
        {
            //  攻撃してそうなアニメーション
            player.DamageCalc("Hammer", Strength);
            player.hasTakenDamage = true;

            if (player.isHammer_StunImmune == false) 
                player.Hammer_Stun = true;
        }
        else if (action[(int)ItemName.Magic])
        {
            //  攻撃してそうなアニメーション
            player.DamageCalc("Magic", Strength);
            player.hasTakenDamage = true;
        }
        else if (action[(int)ItemName.Potion])
        {
            StartCoroutine(HealBox(HealBoxOBJ));
        }

        if (enemyturn.commandprehubCLONE[GM.TurnCount] != null)
            enemyturn.commandprehubCLONE[GM.TurnCount].GetComponent<CanvasGroup>().alpha = 0.2f;

        GM.nowTURN = TURN.PLAYER_TURN;
        InAction = false;
    }

    public void DamageCalc(string string_attackTYPE, float attackPower)
    {
        float int_attackTYPE = 0;
        float RES_Value = 0.0f;
        float DEF_Value = 0.0f;
        float ATK_Value = 0;

        if (string_attackTYPE == "Sword")
            int_attackTYPE = SwordRES;

        if (string_attackTYPE == "Hammer")
            int_attackTYPE = HammerRES;

        if (string_attackTYPE == "Magic")
            int_attackTYPE = MagicRES;

        RES_Value = 1 - ((float)int_attackTYPE / 100);
        DEF_Value = 1 - ((float)Defence / 100);
        ATK_Value = attackPower * RES_Value * DEF_Value;

        HP -= (int)ATK_Value;
    }

    IEnumerator HealBox(GameObject a)
    {
        for (int i = 0; i < 4; i++)
        {
            a.SetActive(true);
            yield return new WaitForSeconds(0.1f);

            a.SetActive(false);
            yield return new WaitForSeconds(0.1f);
        }

        HP += (float)maxHP * ((float)HealAmount / 100);
    }
}
