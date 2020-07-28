using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singleton
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject gameManager = new GameObject("Game Manager");
                _instance = gameManager.AddComponent<GameManager>();
                _instance.LoadInstance();
                DontDestroyOnLoad(gameManager);
            }
            return _instance;
        }
    }

    private void LoadInstance()
    {
        LoadCompletedLevels();
    }
    #endregion

    public int CurrentLevel { get; private set; } = 1;

    private const string CompletedLevelsPath = "CompletedLevels";

    private readonly List<int> levels = new List<int>() { 1, 2 };
    private readonly int numberOfScenesBeforeLevels = 1;

    private List<int> completedLevels;


    public void ChangeLevel(int level)
    {
        SwitchLevel(level);
    }


    public void OnLevelComplete()
    {
        completedLevels.Add(CurrentLevel);

        LoadVictorySceneOnAllLevelsCompleted();
        
        int nextLevel = CurrentLevel + 1;
        SwitchLevel(nextLevel);  
    }


    private void SwitchLevel(int level)
    {
        int levelIndex = LevelToScene(level);

        CurrentLevel = levelIndex;

        CheckPoint.ResetCheckpoint();

        SceneManager.LoadScene(levelIndex);
    }


    private int LevelToScene(int level)
    {
        return (level + numberOfScenesBeforeLevels) - 1;
    }



    private void LoadVictorySceneOnAllLevelsCompleted()
    {
        if (completedLevels.Count != levels.Count) return;

        AudioManager.Instance.OnGameCompleted();
        SceneManager.LoadScene("VictoryScene");
    }


    public int LatestCompletedLevel()
    {
        return completedLevels.Any() ? completedLevels.Last() : 0;
    }


    public bool IsLevelComplete(int level)
    {
        return IsLevelCompleted(level);
    }


    private bool IsLevelCompleted(int level)
    {
        if (completedLevels == null || completedLevels.Count == 0)
        {
            return false;
        }

        return completedLevels.Exists(x => x == level);
    }


    #region Save/Load

    [Serializable]
    private class CompletedLevelsObject
    {
        public CompletedLevelsObject(List<int> completedLevels)
        {
            this.completedLevels = completedLevels;
        }

        public List<int> completedLevels;
    }


    private void SaveCompletedLevels()
    {
        CompletedLevelsObject completedLevelsObject = new CompletedLevelsObject(completedLevels);

        string completedLevelsString = JsonUtility.ToJson(completedLevelsObject);

        PlayerPrefs.SetString(CompletedLevelsPath, completedLevelsString);
    }

    private void LoadCompletedLevels()
    {
        if (!PlayerPrefs.HasKey(CompletedLevelsPath))
        {
            completedLevels = new List<int>();
            SaveCompletedLevels();
        }
        else
        {
            string completedLevelsString = PlayerPrefs.GetString(CompletedLevelsPath);

            CompletedLevelsObject completedLevelsObject = JsonUtility.FromJson<CompletedLevelsObject>(completedLevelsString);
            completedLevels = completedLevelsObject.completedLevels;
        }
    }
    #endregion
}
