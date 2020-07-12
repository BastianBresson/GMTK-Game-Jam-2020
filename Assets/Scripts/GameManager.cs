using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Simple Singleton
    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion

    [SerializeField] private List<GameObject> levels = default;
    public int currentLevel { get; private set; } = 0;

    Dictionary<int, bool> completedLevels = new Dictionary<int, bool>();

    [SerializeField] private GameObject playerGO = default;
    private Player player;
    private CheckPoint currentCheckPoint;
    private Vector3 checkpointPosition;
    private Vector3 playerStartPosition;

    [SerializeField] List<GameObject> probes = default;

    public void ChangeLevel(int level)
    {
        StartCoroutine(RefreshProbes());

        int lvl = level - 1;
        if (lvl == currentLevel) return;

        SwitchLevel(lvl);
    }

    public void OnLevelComplete()
    {
        completedLevels[currentLevel] = true;

        CheckAllLevelsComplete();

        if (currentLevel == 8) // Last Level
        {
            ResetPlayerPositionToStart();
        }
        else
        {
            int lvl = currentLevel + 1;
            ResetPlayerPositionToStart();
            SwitchLevel(lvl);
            StartCoroutine(RefreshProbes());
            ResetCheckPoint();
        }
    }


    private void SwitchLevel(int lvl)
    {
        levels[currentLevel].SetActive(false);
        currentLevel = lvl;
        levels[lvl].SetActive(true);
    }


    private void CheckAllLevelsComplete()
    {
        for (int i = 0; i < completedLevels.Count; i++)
        {
            if (!completedLevels[i])
            {
                return;
            }
        }

        AudioManager.Instance.OnGameCompleted();
        SceneManager.LoadScene("VictoryScene");
    }


    public bool IsLevelComplete(int level)
    {
        return completedLevels[level];
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
        playerStartPosition = playerGO.transform.position;
        playerStartPosition.y = 2;
        checkpointPosition = playerStartPosition;
        player = playerGO.GetComponent<Player>();

        SetCompletedLevels();
    }


    private void ResetPlayerPositionToStart()
    {
        player.ResetPosition(playerStartPosition);
    }


    private void ResetPlayerPositionToCheckpoint()
    {
        player.ResetPosition(checkpointPosition);
    }


    private void SetCompletedLevels()
    {
        for (int i = 0; i < 9; i++)
        {
            completedLevels.Add(i, false);
        }
    }


    IEnumerator RefreshProbes()
    {
        yield return new WaitForFixedUpdate();

        foreach (GameObject probe in probes)
        {
            probe.GetComponent<ReflectionProbe>().RenderProbe(null);
        }
    }

}
