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

    //  基礎ステータス
    [SerializeField] int maxHP;
    [SerializeField] float HP;
    [SerializeField] int Strength;
    [SerializeField] int Defence;

    //  攻撃種別　耐性
    //  RES = resistance( 耐性 )
    [SerializeField] int SwordRES;  //  剣　耐性　
    [SerializeField] int HammerRES; //  槌  耐性  
    [SerializeField] int MagicRES;  //  魔  耐性  

    bool[] action = new bool[5];

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

        HP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = HP / maxHP;

        action = playerturn.CommandType;

        if (GM.nowTURN == TURN.PLAYER_TURN && !InAction)
        {
            InAction = true;

            Invoke("Action", 3.0f);
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
            GM.HP0GameOver = true;
        }
    }

    void Action()
    {
        playerturn.CommandExecution();

        if (action[(int)ItemName.Sword])
        {
            PlayerAnimation("attack");
        }
        else if (action[(int)ItemName.Hammer])
        {
            PlayerAnimation("attack");
        }
        else if (action[(int)ItemName.Magic])
        {
            PlayerAnimation("attack");
        }
        else if (action[(int)ItemName.Potion])
        {
            PlayerAnimation("die");
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
        else if (AnimationName == "die")
        {
            animator.SetTrigger("die");
        }
    }

    public void DamageCalc(string string_attackTYPE,int attackPower)
    {
        int int_attackTYPE = 0;

        if (string_attackTYPE == "Sword")
            int_attackTYPE = SwordRES;

        if (string_attackTYPE == "Hammer")
            int_attackTYPE = HammerRES;

        if (string_attackTYPE == "Magic")
            int_attackTYPE = MagicRES;

        HP -= attackPower * (1 - ((float)Defence / 100) + ((float)int_attackTYPE / 100) );
    }
}
