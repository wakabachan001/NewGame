using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public  List<GameObject> CommandList = new List<GameObject>(6);
    public  List<ItemName> a = new List<ItemName>();
    public enum GameState
    {
        TITLE,
        STAGESELECT,
        COMMANDSELECT,
        BATTLE,
        GAMECLEAR,
        GAMEOVER1,
        GAMEOVER2,
    }

    public enum TURN
    {
        PLAYER_TURN,
        ENEMY_TURN
    }

    public TURN nowTURN = TURN.ENEMY_TURN;

    public  enum ItemName
    {
        EMPTY,
        Sword,
        Hammer,
        Magic,
        Potion
    }

    // Start is called before the first frame update
    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        for (int i = 0; i < CommandList.Count; i++) 
        {
            if (CommandList[i] != null) 
            {
                if (CommandList[i].name.StartsWith("Sword"))
                    a[i] = ItemName.Sword;
               else if (CommandList[i].name.StartsWith("Hammer"))
                    a[i] = ItemName.Hammer;
               else if (CommandList[i].name.StartsWith("Magic"))
                    a[i] = ItemName.Magic;
               else if (CommandList[i].name.StartsWith("Potion"))
                    a[i] = ItemName.Potion;
            }
        }
    }

}
