using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class Player : MonoBehaviour
{
    GameManager GM;
    PlayerTurn playerturn;

    //  基礎ステータス
    [SerializeField] int maxHP;
    [SerializeField] int HP;
    [SerializeField] int Strength;
    [SerializeField] int Defence;

    //  攻撃種別　耐性
    //  RES = resistance( 耐性 )
    [SerializeField] int SwordRES;  //  剣　耐性　
    [SerializeField] int HammerRES; //  槌  耐性  
    [SerializeField] int MagicRES;  //  魔  耐性  

    bool[] action = new bool[5];
    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerturn = GameObject.Find("PlayerTurn").GetComponent<PlayerTurn>();

        HP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        action = playerturn.CommandType;

        if(GM.nowTURN == TURN.PLAYER_TURN)
        {
            playerturn.CommandExecution();

            if (action[(int)ItemName.Sword])
            {
                //  攻撃してそうなアニメーション
            }
            else if(action[(int)ItemName.Hammer])
            {
                //  攻撃してそうなアニメーション
            }
            else if (action[(int)ItemName.Magic])
            {
                //  攻撃してそうなアニメーション
            }
            else if( action[(int)ItemName.Potion] )
            {
                //  回復してそうなアニメーション
            }

            if (playerturn.commandprehubCLONE[GM.TurnCount] != null)
                playerturn.commandprehubCLONE[GM.TurnCount].GetComponent<CanvasGroup>().alpha = 0.2f;

            StartCoroutine(FindObjectOfType<GameManager>().WaitTimer(2.5f, true));
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

        HP = attackPower * (1 - (Defence + int_attackTYPE) / 100);
    }
}
