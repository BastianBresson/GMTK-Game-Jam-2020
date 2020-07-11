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

    int currentLevel = 0;

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
            ResetPlayerPosition();
        }
        else
        {
            int lvl = currentLevel + 1;
            ResetPlayerPosition();
            SwitchLevel(lvl);
        }
    }


    private void SwitchLevel(int lvl)
    {
        levels[currentLevel].SetActive(false);
        currentLevel = lvl;
        levels[lvl].SetActive(true);
    }


    private void ResetPlayerPosition()
    {

    }

}
