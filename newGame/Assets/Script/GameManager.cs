using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public  List<GameObject> CommandList = new List<GameObject>(6);
    public  List<ItemName> playercommand = new List<ItemName>();
    public  List<GameObject> EnemyList = new List<GameObject>(4);

    public  int TurnCount = 1;

    private bool canPprocess = true;

    //public string TURN = "\0";
    public enum GameScene
    {
        TITLE,
        STAGESELECT,
        COMMANDSELECT,
        BATTLE,
        GAMECLEAR,
        GAMEOVER1,
        GAMEOVER2,
    }
    public GameScene nowScene= GameScene.STAGESELECT;
    public string string_nowScene;

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
                    playercommand[i] = ItemName.Sword;

               else if (CommandList[i].name.StartsWith("Hammer"))
                    playercommand[i] = ItemName.Hammer;

               else if (CommandList[i].name.StartsWith("Magic"))
                    playercommand[i] = ItemName.Magic;

               else if (CommandList[i].name.StartsWith("Potion"))
                    playercommand[i] = ItemName.Potion;
            }
        }

    }

    public void SceneChange(GameScene scene)
    {
        switch (scene)
        {
            case GameScene.TITLE:
                TitleScene();
                break;

            case GameScene.STAGESELECT:
                StageSelectScene();
                break;

            case GameScene.COMMANDSELECT:
                CommandSelectScene();
                break;

            case GameScene.BATTLE:
                BattleScene();
                break;

            case GameScene.GAMECLEAR:
                GameClearScene();
                break;

            case GameScene.GAMEOVER1:
                GameOver1Scene();
                break;

            case GameScene.GAMEOVER2:
                GameOver2Scene();
                break;
        }
    }

    void TitleScene()
    {
        string_nowScene = "TITLE";
        Initiate.Fade("Title", Color.black, 1.5f);
    }

    void StageSelectScene()
    {
        string_nowScene = "STAGESELECT";
        Initiate.Fade("StageSelect", Color.black, 1.5f);
    }

    void CommandSelectScene()
    {
        string_nowScene = "COMMANDSELECT";
        Initiate.Fade("CommandSelect", Color.black, 1.5f);

        GameObject EnemyParent = GameObject.Find("EnemyParent");
        Transform  EnemyParentTransform = EnemyParent.GetComponent<Transform>();
        GameObject EnemyObject;

        EnemyObject = Instantiate(EnemyList[StageSelect.int_StageNumber], EnemyParentTransform);
        EnemyObject.transform.localScale *= 0.7f;
    }

    void BattleScene()
    {
        string_nowScene = "BATTLE";
        Initiate.Fade("Battle", Color.black, 2.0f);
    }

    void GameClearScene()
    {
        string_nowScene = "GAMECLEAR";
        Initiate.Fade("GameClear", Color.black, 1.5f);
    }

    void GameOver1Scene()
    {
        string_nowScene = "GAMEOVER1";
        Initiate.Fade("GameOver1", Color.black, 1.5f);
    }

    void GameOver2Scene()
    {
        string_nowScene = "GAMEOVER2";
        Initiate.Fade("GameOver2", Color.black, 1.5f);
    }
}
