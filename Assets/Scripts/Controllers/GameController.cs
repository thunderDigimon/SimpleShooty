using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : Singleton<GameController>
{
    private int mCurrentLevel = 1;
    private string mCurrentScene = "defaultLevel";
    private Stage mCurrentStage;
    private Zone mCurrentZone = null;
    private int mCurrentStageEnemiesToBeKilled;

    // called first
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        GameEventManager.Instance.UnregisterEventObserver(GameEvent.ENEMY_KILLED, onEnemyKilled);
        GameEventManager.Instance.RegisterEventObserver(GameEvent.ENEMY_KILLED, onEnemyKilled);
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

        if (mCurrentZone != null)
        {
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
            mCurrentStageEnemiesToBeKilled = 1;
            LoadEnemiesToCurrentStage();
        }
        else
        {
            Debug.Log("Zone Completed");
        }
    }

    private void LoadEnemiesToCurrentStage()
    {
        EnemyFactory.Instance.SpawnEnemy(mCurrentStageEnemiesToBeKilled, mCurrentStage.transform.position);
    }
}
