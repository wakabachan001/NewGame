using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public static List<GameObject> CommandList = new List<GameObject>();
    public List<ItemName> a = new List<ItemName>();
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

    public  enum ItemName
    {
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
        for (int i = 0; i < CommandList.Count-1; i++) 
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
