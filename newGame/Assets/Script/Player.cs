using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using static GameManager;

public class Player : MonoBehaviour
{
    GameManager GM;
    PlayerTurn playerturn;
    Animator animator;
    Slider slider;
    Enemy enemy;

    public GameObject HealBoxOBJ;

    //  基礎ステータス
    [SerializeField] int maxHP;
    [SerializeField] float HP;
    [SerializeField] float Strength;
    [SerializeField] float Strength_Buff_Value;
    [SerializeField] int Defence;
    [SerializeField] int HealAmount;

    //  攻撃種別　耐性
    //  RES = resistance( 耐性 )
    [SerializeField] int SwordRES;  //  剣　耐性　
    [SerializeField] int HammerRES; //  槌  耐性  
    [SerializeField] int MagicRES;  //  魔  耐性  

    bool[] action = new bool[5];

    bool Strength_Buff = false;
    bool Hammer_Stun = false;
    bool canprocess = true;
    bool InAction = false;
    public  bool AttackedByTheEnemy = false;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerturn = GameObject.Find("PlayerTurn").GetComponent<PlayerTurn>();
        slider = GameObject.Find("PlayerHPBar").GetComponent<Slider>();
        //spriteRenderer = HealBoxOBJ.GetComponent<SpriteRenderer>();

        HP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = HP / maxHP;

        action = playerturn.CommandType;

        if (GM.nowTURN == TURN.PLAYER_TURN && !InAction)
        {
            if (enemy == null) 
                enemy = GameObject.FindWithTag("Enemy").GetComponent<Enemy>();
            InAction = true;

            Invoke("Action", 1.0f);
        }

        if(AttackedByTheEnemy)
        {
            PlayerAnimation("hurt");
            AttackedByTheEnemy = false;
        }

        if ((int)HP < 1 && canprocess)
        {
            canprocess = false;
            PlayerAnimation("die");

            GM.SceneChange(GameScene.GAMEOVER1);
        }
    }

    void Action()
    {
        playerturn.CommandExecution();

        if (action[(int)ItemName.Sword])
        {
            PlayerAnimation("attack");
            CheckStrengthBuff("Sword");
            //if (Strength_Buff){
            //    enemy.DamageCalc("Sword", Strength + (Strength * (Strength_Buff_Value / 100)));
            //}
            //else
            //    enemy.DamageCalc("Sword", Strength);
        }
        else if (action[(int)ItemName.Hammer])
        {
            PlayerAnimation("attack");
            CheckStrengthBuff("Hammer");
            //if (Strength_Buff) { 
            //    enemy.DamageCalc("Hammer", Strength * 0.4f + (Strength * (Strength_Buff_Value / 100)));
            //}
            //else
            //    enemy.DamageCalc("Hammer", Strength * 0.4f);
        }
        else if (action[(int)ItemName.Magic])
        {
            PlayerAnimation("attack");
            CheckStrengthBuff("Magic");
            //if (Strength_Buff){
            //    enemy.DamageCalc("Magic", Strength * 0.4f);
            //}
            //else
                
            Strength_Buff = true;
        }
        else if (action[(int)ItemName.Potion])
        {
            PlayerAnimation("heal");
        }

        if (playerturn.commandprehubCLONE[GM.TurnCount] != null)
            playerturn.commandprehubCLONE[GM.TurnCount].GetComponent<CanvasGroup>().alpha = 0.2f;

        //StartCoroutine(FindObjectOfType<GameManager>().WaitTimer(2.5f, true));
        GM.nowTURN = TURN.ENEMY_TURN;
        InAction = false;
        GM.TurnCount++;
    }

    void PlayerAnimation(string AnimationName)
    {
        if (AnimationName == "attack")
        {
            animator.SetTrigger("attack");
        }
        else if (AnimationName == "hurt")
        {
            animator.SetTrigger("hurt");
        }
        else if(AnimationName == "heal")
        {
            StartCoroutine(HealBox(HealBoxOBJ));
        }
        else if (AnimationName == "die")
        {
            animator.SetTrigger("die");
        }
    }

    void CheckStrengthBuff(string AttackType)
    {
        if(Strength_Buff)
        {
            if(AttackType=="Sword")
                enemy.DamageCalc(AttackType, Strength + (Strength * (Strength_Buff_Value / 100)));
            else
                enemy.DamageCalc(AttackType, Strength * 0.4f + (Strength * (Strength_Buff_Value / 100)));

            Strength_Buff = false;
        }
        else
        {
            if (AttackType == "Sword")
                enemy.DamageCalc(AttackType, Strength);
            else
                enemy.DamageCalc("Magic", Strength * 0.4f);
        }
    }
    public void DamageCalc(string string_attackTYPE,float attackPower)
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
