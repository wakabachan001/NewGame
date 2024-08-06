using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Audio
{
    public float AudioVolume;
    public bool AudioLoop;
}

public class GameManager : MonoBehaviour
{
    AudioSource m_AudioSource;

    public AudioClip m_Title;
    public AudioClip m_StageSelect;
    public AudioClip m_CommandSelect;
    public AudioClip m_Battle;
    public AudioClip m_GameOver1;
    public AudioClip m_GameOver2;
    public AudioClip m_GameClear;

    Player player;
    public static GameManager Instance { get; private set; }
    public  List<GameObject> CommandList = new List<GameObject>(6);
    public  List<ItemName> playercommand = new List<ItemName>();
    public  List<GameObject> EnemyList = new List<GameObject>(4);

    public  int TurnCount = 0;
    public float turnSpeed = 0.1f;
    public float turnSpeedMultiplier = 1.0f;
    public bool HP0GameOver = false;
    public bool AllTurnEnd = false;
    public bool GameClear = false;

    bool wait = false;

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

    private void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
        m_AudioSource.PlayOneShot(m_Title, 0.5f);
    }

    private void Update()
    {
        if (string_nowScene == "STAGESELECT")
        {
            TurnCount = 0;

            for (int i = 0; i < playercommand.Count; i++)
                playercommand[i] = ItemName.EMPTY;
        }

        Invoke("a", 5f);

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

    void a()
    {
        if (TurnCount == 6)
        {
            TurnCount++;
            SceneChange(GameScene.GAMEOVER2);
        }
    }

    void TitleScene()
    {
        string_nowScene = "TITLE";
        Initiate.Fade("Title", Color.black, 1.5f);

        AudioPlayer(m_Title, true, 0.5f, 1.0f);
    }

    void StageSelectScene()
    {
        string_nowScene = "STAGESELECT";
        Initiate.Fade("StageSelect", Color.black, 1.5f);

        AudioPlayer(m_StageSelect, true, 0.5f, 1.0f);
    }

    void CommandSelectScene()
    {
        string_nowScene = "COMMANDSELECT";
        Initiate.Fade("CommandSelect", Color.black, 1.5f);

        Invoke("EnemyInstantiate", 1.0f);

        AudioPlayer(m_CommandSelect, true, 0.5f, 1.0f);
    }

    void BattleScene()
    {
        Initiate.Fade("Battle", Color.black, 1.5f);
        string_nowScene = "BATTLE";
        Invoke("EnemyInstantiate", 1.0f);

        AudioPlayer(m_Battle, true, 0.5f, 1.0f);
    }

    void GameClearScene()
    {
        string_nowScene = "GAMECLEAR";
        Initiate.Fade("GameClear", Color.black, 1.5f);

        AudioPlayer(m_GameClear, true, 0.5f, 1.0f);
    }

    void GameOver1Scene()
    {
        string_nowScene = "GAMEOVER1";
        Initiate.Fade("GameOver1", Color.black, 1.5f);

        AudioPlayer(m_GameOver1, true, 0.5f, 1.0f);
    }

    void GameOver2Scene()
    {
        string_nowScene = "GAMEOVER2";
        Initiate.Fade("GameOver2", Color.black, 1.5f);

        AudioPlayer(m_GameOver2, true, 0.5f, 1.0f);
    }

    void EnemyInstantiate()
    {
        GameObject GameOBJ_EnemyParent = GameObject.Find("EnemyParent");
        EnemyParent Script_EnemyParent = GameOBJ_EnemyParent.GetComponent<EnemyParent>();
        Transform EnemyParentTransform = GameOBJ_EnemyParent.GetComponent<Transform>();

        Script_EnemyParent.EnemyArray[StageSelect.int_StageNumber].SetActive(true);
    }

    void AudioPlayer(AudioClip clip,bool loop,float volume,float delayTime)
    {
        m_AudioSource.clip = clip;
        m_AudioSource.loop = loop;
        m_AudioSource.volume = volume;
        m_AudioSource.PlayDelayed(delayTime);
    }
}
