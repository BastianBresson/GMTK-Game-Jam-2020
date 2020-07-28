using System;
using System.Collections;
using System.Collections.Generic;
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
                DontDestroyOnLoad(gameManager);
            }
            return _instance;
        }
    }
    #endregion

    private const string CompletedLevelsPath = "CompletedLevels";

    private List<int> levels = new List<int>() { 1, 2 };
    private int numberOfScenesBeforeLevels = 1;
    public int currentLevel { get; private set; }

    List<int> completedLevels;

    [SerializeField] private GameObject playerGO = default;
    private Player player;
    private CheckPoint currentCheckPoint;
    private Vector3 checkpointPosition;
    private Vector3 playerStartPosition;

    public void ChangeLevel(int level)
    {
        if (level == currentLevel) return;

        SwitchLevel(LevelToScene(level));
    }

    public void OnLevelComplete()
    {
        completedLevels.Add(currentLevel);

        CheckAllLevelsComplete();

        if (currentLevel == 8) // Last Level
        {
            ResetPlayerPositionToStart();
        }
        else
        {
            int nextLevel = currentLevel + 1;
            ResetPlayerPositionToStart();
            SwitchLevel(nextLevel);
            ResetCheckPoint();
        }
    }


    private void SwitchLevel(int level)
    {
        currentLevel = level;
        SceneManager.LoadScene(level);
    }


    private void CheckAllLevelsComplete()
    {
        for (int i = 0; i < completedLevels.Count; i++)
        {
            if (completedLevels.Count != levels.Count) return;
        }

        AudioManager.Instance.OnGameCompleted();
        SceneManager.LoadScene("VictoryScene");
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


    public void OnCheckpointReached(CheckPoint checkPoint)
    {
        currentCheckPoint = checkPoint;
        this.checkpointPosition = checkPoint.transform.position;
        this.checkpointPosition.y = 2;
    }


    private void ResetCheckPoint()
    {
        checkpointPosition = playerStartPosition;
        if (currentCheckPoint != null)
        {
            currentCheckPoint.TurnOff();
        }
    }


    public void OnPlayerFall()
    {
        ResetPlayerPositionToCheckpoint();
    }


    private void Start()
    {
        playerGO = GameObject.FindWithTag("Player");

        playerStartPosition = playerGO.transform.position;
        playerStartPosition.y = 2;
        checkpointPosition = playerStartPosition;
        player = playerGO.GetComponent<Player>();

        LoadCompletedLevels();
    }


    private void ResetPlayerPositionToStart()
    {
        player.ResetPosition(playerStartPosition);
    }


    private void ResetPlayerPositionToCheckpoint()
    {
        player.ResetPosition(checkpointPosition);
    }


    private int LevelToScene(int level)
    {
        return (level + numberOfScenesBeforeLevels) - 1;
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
