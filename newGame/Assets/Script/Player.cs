using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using static GameManager;

public class Player : MonoBehaviour
{
    GameManager GM;
    PlayerTurn  playerturn;
    Animator    animator;
    Slider      slider;
    Enemy       enemy;

    public GameObject HealBoxOBJ;
    public GameObject StunImmuneBOX;

    //  基礎ステータス
    [SerializeField] int   maxHP;               //  最大HP
    [SerializeField] float currentHP;           //  現在のHP
    [SerializeField] float Strength;            //  攻撃力
    [SerializeField] float Strength_Buff_Value; //  攻撃力UP効果値
    [SerializeField] float StrengthReduction;   //  攻撃力の減少値（魔法と槌の倍率変更）
    [SerializeField] int   Defense;             //  防御力
    [SerializeField] int   HealAmount;          //  回復量

    //  攻撃種別　耐性
    //  RES = resistance( 耐性 )
    [SerializeField] int SwordRES;  //  剣の耐性
    [SerializeField] int HammerRES; //  槌の耐性
    [SerializeField] int MagicRES;  //  魔の耐性
    public float EffectRES;

    bool[] action = new bool[5];

    bool Strength_Buff = false;             //  攻撃力UP判定用
    public bool Hammer_Stun = false;        //  スタンを受けたかどうか
    public bool isHammer_StunImmune = false;//  スタンを無効化するかどうか
    bool isAlive = true;                    //  生きてるかどうか
    bool InAction = false;                  //  行動中かどうか
    public  bool hasTakenDamage = false;    //  攻撃を受けたかどうか

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerturn = GameObject.Find("PlayerTurn").GetComponent<PlayerTurn>();
        slider = GameObject.Find("PlayerHPBar").GetComponent<Slider>();

        currentHP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHP > maxHP)
            currentHP = maxHP;

        slider.value = currentHP / maxHP;

        action = playerturn.CommandType;

        if (GM.nowTURN == TURN.PLAYER_TURN && !InAction)    //  プレイヤーのターンの時　＆＆　行動中じゃない時
        {
            if (enemy == null) 
                enemy = GameObject.FindWithTag("Enemy").GetComponent<Enemy>();

            InAction = true;
            isHammer_StunImmune = false;

            Invoke("Action", GM.turnSpeed / GM.turnSpeedMultiplier); //  ターン進行速度を変化させる
        }

        if(hasTakenDamage)
        {
            PlayerAnimation("hurt");
            hasTakenDamage = false;
        }

        if (isHammer_StunImmune)
        {
            StunImmuneBOX.SetActive(true);
            PlayerAnimation("stun immune");
        }
        else
            StunImmuneBOX.SetActive(false);

        if ((int)currentHP < 1 && isAlive)
        {
            isAlive = false;
            PlayerAnimation("die");

            GM.SceneChange(GameScene.GAMEOVER1);
        }
    }

    void Action()
    {
        playerturn.CommandExecution();

        if (Hammer_Stun == false)
        {
            if (action[(int)ItemName.Sword])
            {
                PlayerAnimation("attack");
                CheckStrengthBuff("Sword");
                enemy.hasTakenDamage = true;
            }
            else if (action[(int)ItemName.Hammer])
            {
                PlayerAnimation("attack");
                CheckStrengthBuff("Hammer");
                enemy.hasTakenDamage = true;

                if (enemy.isHammer_StunImmune == false)
                    enemy.Hammer_Stun = (Random.value > (1 - enemy.EffectRES / 100)); //  ( 値 * 100 )% の確率でtrueを返す
            }
            else if (action[(int)ItemName.Magic])
            {
                PlayerAnimation("attack");
                CheckStrengthBuff("Magic");
                enemy.hasTakenDamage = true;

                Strength_Buff = true;
            }
            else if (action[(int)ItemName.Potion])
            {
                PlayerAnimation("heal");
                isHammer_StunImmune = true;
            }
        }
        else
            Hammer_Stun = false;

        if (playerturn.commandprehubCLONE[GM.TurnCount] != null)
            playerturn.commandprehubCLONE[GM.TurnCount].GetComponent<CanvasGroup>().alpha = 0.2f;   //  現在のターンのコマンドカードを暗くする（ターンが終了した事を視覚化）

        GM.nowTURN = TURN.ENEMY_TURN;
        InAction = false;
        GM.TurnCount++; //  プレイやーは後攻なので、プレイヤーのターンが終了したときに次のターンに進行する
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
        else if (AnimationName == "stun immune")
        {
            StartCoroutine(StunImmune(0.0f, 0.5f, 0.0f, 0.2f));
        }
        else if (AnimationName == "die")
        {
            animator.SetTrigger("die");
        }
    }

    void CheckStrengthBuff(string AttackType)   //  攻撃力バフを受けているか調べて、ダメージ計算をする関数
    {
        if(Strength_Buff)
        {
            if (AttackType == "Sword")
                enemy.DamageCalc(AttackType, Strength                             + (Strength * (Strength_Buff_Value / 100)));  //  素の攻撃力               + 素の???%
            else
                enemy.DamageCalc(AttackType, Strength * (StrengthReduction / 100) + (Strength * (Strength_Buff_Value / 100)));  //  素の攻撃力 * 倍率減少値% + 素の???%

            Strength_Buff = false;
        }
        else
        {
            if (AttackType == "Sword")
                enemy.DamageCalc(AttackType, Strength);
            else
                enemy.DamageCalc(AttackType, Strength * StrengthReduction / 100);
        }
    }

    public void DamageCalc(string string_attackTYPE,float attackPower)
    {
        float float_attackTYPE = 0.0f;   //  攻撃種類によって値（耐性値）を変更させる用
        float RES_Value = 0.0f;     //  最終耐性値
        float DEF_Value = 0.0f;     //  最終防御力
        float ATK_Value = 0;        //  最終攻撃値

        if (string_attackTYPE == "Sword")
            float_attackTYPE = SwordRES;

        if (string_attackTYPE == "Hammer")
            float_attackTYPE = HammerRES;

        if (string_attackTYPE == "Magic")
            float_attackTYPE = MagicRES;

        RES_Value = 1 - ((float)float_attackTYPE / 100);
        DEF_Value = 1 - ((float)Defense / 100);
        ATK_Value = attackPower * RES_Value * DEF_Value;

        currentHP -= (int)ATK_Value;
    }

    IEnumerator HealBox(GameObject healbox)
    {
        for (int i = 0; i < 4; i++) 
        {
            healbox.SetActive(true);
            yield return new WaitForSeconds(0.1f);

            healbox.SetActive(false);
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
