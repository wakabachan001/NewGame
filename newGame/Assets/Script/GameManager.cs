using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public static List<GameObject> CommandList = new List<GameObject>();
    public List<GameObject> a = new List<GameObject>();
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
        a = CommandList;
    }

}
