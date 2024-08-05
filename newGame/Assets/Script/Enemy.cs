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
    public GameObject StunImmuneBOX;

    [SerializeField] int maxHP;
    [SerializeField] float currentHP;
    [SerializeField] float Strength;
    [SerializeField] float Strength_Buff_Value; //  攻撃力UP効果値
    [SerializeField] float StrengthReduction;   //  攻撃力の減少値（魔法と槌の倍率変更）
    [SerializeField] int Defence;
    [SerializeField] int HealAmount;

    [SerializeField] int SwordRES;
    [SerializeField] int HammerRES;
    [SerializeField] int MagicRES;
    public float EffectRES;

    bool[] action = new bool[5];

    bool Strength_Buff = false;             //  攻撃力UP判定用
    public bool Hammer_Stun = false;        //  スタンを受けたかどうか
    public bool isHammer_StunImmune = false;//  スタンを無効化するかどうか
    bool isAlive = true;
    bool InAction = false;
    public bool hasTakenDamage = false;

    public List<ItemName> enemycommand = new List<ItemName>();
    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        enemyturn = GameObject.Find("EnemyTurn").GetComponent<EnemyTurn>();

        if(GM.string_nowScene == "BATTLE"){
            player = GameObject.FindWithTag("Player").GetComponent<Player>();
            slider = GameObject.Find("EnemyHPBar").GetComponent<Slider>();
        }

        currentHP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        if (GM.string_nowScene == "BATTLE")
        {
            slider.value = currentHP / maxHP;
            action = enemyturn.CommandType;

            if (GM.nowTURN == TURN.ENEMY_TURN && !InAction)
            {
                InAction = true;
                isHammer_StunImmune = false;

                Invoke("Action", GM.turnSpeed / GM.turnSpeedMultiplier);
            }
        }
        else
            enabled = false;

        if (hasTakenDamage)
        {
            EnemyAnimation("hurt");
            hasTakenDamage = false;
        }

        if (isHammer_StunImmune)
        {
            StunImmuneBOX.SetActive(true);
            EnemyAnimation("stun immune");
        }
        else
            StunImmuneBOX.SetActive(false);

        if ((int)currentHP < 1 && isAlive)
        {
            isAlive = false;
            EnemyAnimation("die");

            GM.SceneChange(GameScene.GAMECLEAR);
        }
    }

    void Action()
    {
        enemyturn.CommandExecution();

        if (Hammer_Stun == false)
        {
            if (action[(int)ItemName.Sword])
            {
                EnemyAnimation("attack");
                CheckStrengthBuff("Sword");
                player.hasTakenDamage = true;
            }
            else if (action[(int)ItemName.Hammer])
            {
                EnemyAnimation("attack");
                CheckStrengthBuff("Hammer");
                player.hasTakenDamage = true;

                if (player.isHammer_StunImmune == false)
                    player.Hammer_Stun = (Random.value > (1 - player.EffectRES / 100)); //  ( 値 * 100 )% の確率でtrueを返す
            }
            else if (action[(int)ItemName.Magic])
            {
                EnemyAnimation("attack");
                CheckStrengthBuff("Magic");
                player.hasTakenDamage = true;

                Strength_Buff = true;
            }
            else if (action[(int)ItemName.Potion])
            {
                EnemyAnimation("heal");
                isHammer_StunImmune = true;
            }
        }
        else
            Hammer_Stun = false;

        if (enemyturn.commandprehubCLONE[GM.TurnCount] != null)
            enemyturn.commandprehubCLONE[GM.TurnCount].GetComponent<CanvasGroup>().alpha = 0.2f;

        GM.nowTURN = TURN.PLAYER_TURN;
        InAction = false;
    }

    void EnemyAnimation(string AnimationName)
    {
        if (AnimationName == "attack")
        {
            
        }
        else if (AnimationName == "hurt")
        {
            
        }
        else if (AnimationName == "heal")
        {
            StartCoroutine(HealBox(HealBoxOBJ));
        }
        else if (AnimationName == "stun immune")
        {
            StartCoroutine(StunImmune(0.0f, 0.5f, 0.0f, 0.2f));
        }
        else if (AnimationName == "die")
        {
            
        }
    }

    public void DamageCalc(string string_attackTYPE, float attackPower)
    {
        float float_attackTYPE = 0.0f;
        float RES_Value = 0.0f;
        float DEF_Value = 0.0f;
        float ATK_Value = 0;

        if (string_attackTYPE == "Sword")
            float_attackTYPE = SwordRES;

        if (string_attackTYPE == "Hammer")
            float_attackTYPE = HammerRES;

        if (string_attackTYPE == "Magic")
            float_attackTYPE = MagicRES;

        RES_Value = 1 - ((float)float_attackTYPE / 100);
        DEF_Value = 1 - ((float)Defence / 100);
        ATK_Value = attackPower * RES_Value * DEF_Value;

        currentHP -= (int)ATK_Value;
    }

    void CheckStrengthBuff(string AttackType)   //  攻撃力バフを受けているか調べて、ダメージ計算をする関数
    {
        if (Strength_Buff)
        {
            if (AttackType == "Sword")
                player.DamageCalc(AttackType, Strength + (Strength * (Strength_Buff_Value / 100)));  //  素の攻撃力               + 素の???%
            else
                player.DamageCalc(AttackType, Strength * (StrengthReduction / 100) + (Strength * (Strength_Buff_Value / 100)));  //  素の攻撃力 * 倍率減少値% + 素の???%

            Strength_Buff = false;
        }
        else
        {
            if (AttackType == "Sword")
                player.DamageCalc(AttackType, Strength);
            else
                player.DamageCalc(AttackType, Strength * StrengthReduction / 100);
        }
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

        currentHP += (float)maxHP * ((float)HealAmount / 100);
    }

    IEnumerator StunImmune(float startAlpha, float endAlpha, float elapsedTime, float fadeDuration)
    {
        SpriteRenderer sp;
        sp = StunImmuneBOX.GetComponent<SpriteRenderer>();
        Color color = sp.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration); //  Lerp(補完の開始位置 , 補完の終了位置 , 補完の割合値)
            sp.color = color;
            yield return null;// 次のフレームまで待機
        }
        color.a = endAlpha;
        sp.color = color;
    }
}
