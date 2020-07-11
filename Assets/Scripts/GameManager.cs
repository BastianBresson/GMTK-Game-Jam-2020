using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    [SerializeField] private GameObject playerGO = default;
    private Player player;
    private Vector3 checkpointPosition;
    private Vector3 playerStartPosition;

    public void ChangeLevel(int level)
    {
        int lvl = level - 1;
        if (lvl == currentLevel) return;

        SwitchLevel(lvl);
    }

    public void OnLevelComplete()
    {
        if (currentLevel == 8) // Last Level
        {
            ResetPlayerPositionToStart();
        }
        else
        {
            int lvl = currentLevel + 1;
            ResetPlayerPositionToStart();
            SwitchLevel(lvl);
        }
    }

    private void SwitchLevel(int lvl)
    {
        levels[currentLevel].SetActive(false);
        currentLevel = lvl;
        levels[lvl].SetActive(true);
    }


    public void OnCheckpointReached(Vector3 checkpointPosition)
    {
        this.checkpointPosition = checkpointPosition;
        this.checkpointPosition.y = 2;
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
    }


    private void ResetPlayerPositionToStart()
    {
        player.ResetPosition(playerStartPosition);
    }

    private void ResetPlayerPositionToCheckpoint()
    {
        player.ResetPosition(checkpointPosition);
    }
}
