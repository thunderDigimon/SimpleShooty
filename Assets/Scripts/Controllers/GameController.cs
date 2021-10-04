using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;
public class GameController : Singleton<GameController>
{
    [SerializeField]
    private GameObject m_PlayerPrefab;

    private int mCurrentLevel = 1;
    private string mCurrentScene = "defaultLevel";
    private Stage mCurrentStage;
    private StageConfig mCurrentStageConfig;
    private LevelData mCurrentLevelConfig;
    private GameObject m_Player;

    private Zone mCurrentZone = null;
    private int mCurrentStageEnemiesToBeKilled;
    LevelConfig m_LevelConfig;

    void Awake()
    {
        LoadLevelConfig();
    }

    // called first
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        GameEventManager.Instance.UnregisterEventObserver(GameEvent.ENEMY_KILLED, onEnemyKilled);
        GameEventManager.Instance.RegisterEventObserver(GameEvent.ENEMY_KILLED, onEnemyKilled);

        GameEventManager.Instance.UnregisterEventObserver(GameEvent.PLAYER_KILLED, OnPlayerKilled);
        GameEventManager.Instance.RegisterEventObserver(GameEvent.PLAYER_KILLED, OnPlayerKilled);
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        GameEventManager.Instance.UnregisterEventObserver(GameEvent.ENEMY_KILLED, onEnemyKilled);
        GameEventManager.Instance.UnregisterEventObserver(GameEvent.PLAYER_KILLED, OnPlayerKilled);
    }

    void LoadLevelConfig()
    {
        string json = Resources.Load<TextAsset>(Constants.kLevelConfigFile).text;
        m_LevelConfig = JsonConvert.DeserializeObject<LevelConfig>(json);
    }

    private LevelData GetLevelData(int inLevel)
    {
        LevelData retVal = null;
        if (m_LevelConfig != null && m_LevelConfig.Levels != null)
        {
            retVal = inLevel < m_LevelConfig.Levels.Length ? m_LevelConfig.Levels[inLevel - 1] : m_LevelConfig.Levels[m_LevelConfig.Levels.Length - 1];
        }
        return retVal;
    }

    private void OnPlayerKilled(object obj)
    {
        Debug.Log("Player killed");
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (string.Compare(scene.name, mCurrentScene) == 0)
        {
            onCurrentLevelLoaded();
        }
    }

    private void onCurrentLevelLoaded()
    {
        //spawn the player 
        mCurrentZone = FindObjectOfType<Zone>();
        mCurrentLevelConfig = GetLevelData(mCurrentLevel);
        if (mCurrentZone != null)
        {
            m_Player = Instantiate(m_PlayerPrefab, new Vector3(0, 0.357f, -5), Quaternion.identity);
            m_Player.SetActive(true);
            loadCurrentStageSetup();
        }
    }

    private void onEnemyKilled(object obj)
    {
        if (mCurrentStage != null)
        {
            mCurrentStageEnemiesToBeKilled--;

            if (mCurrentStageEnemiesToBeKilled <= 0)
            {
                mCurrentStage.OnStageCompleted();
                mCurrentZone.MoveToNextStage();
                loadCurrentStageSetup();
            }
        }
    }

    void loadCurrentStageSetup()
    {
        mCurrentStage = mCurrentZone.CurrentStage;

        if (mCurrentStage != null)
        {
            mCurrentStageConfig = mCurrentLevelConfig != null && mCurrentLevelConfig.Stages != null && mCurrentZone.CurrentStageIndex < mCurrentLevelConfig.Stages.Length ? mCurrentLevelConfig.Stages[mCurrentZone.CurrentStageIndex] : mCurrentLevelConfig.Stages[mCurrentLevelConfig.Stages.Length - 1];
            mCurrentStageEnemiesToBeKilled = mCurrentStageConfig != null ? mCurrentStageConfig.EnemyCount : 1;
            LoadEnemiesToCurrentStage();
        }
        else
        {
            Debug.Log("Zone Completed");
            LoadNextLevel();
        }
    }

    private void LoadNextLevel()
    {
        mCurrentLevel++;
        if (mCurrentLevel <= m_LevelConfig.Levels.Length)
        {
            mCurrentLevelConfig = GetLevelData(mCurrentLevel);
            mCurrentScene = mCurrentLevelConfig.ZoneScene;
        }
        else
        {
            Debug.Log("All Level Completed");
        }
    }

    public void loadNextGame()
    {
        ResetGameScene();
        SceneManager.LoadScene(mCurrentScene);
    }


    void ResetGameScene()
    {
        Destroy(PoolManager.Instance.gameObject);
        Destroy(GameEventManager.Instance.gameObject);
        Destroy(m_Player);
    }

    private void LoadEnemiesToCurrentStage()
    {
        EnemyFactory.Instance.SpawnEnemy(mCurrentStageEnemiesToBeKilled, mCurrentStage.transform.position);
    }
}
