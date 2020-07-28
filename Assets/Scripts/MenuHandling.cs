using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuHandling : MonoBehaviour
{
    [SerializeField] GameObject UI = default;

    [SerializeField] List<GameObject> levelButtons = default;

    public Color completedColor = default;

    private int markedButton;


    public void OnButtonPressed(int buttonNr)
    {
        GameManager.Instance.ChangeLevel(buttonNr);

        UI.SetActive(false);
    }


    public void OnExitButtonPressed()
    {
        Debug.Log("Exited Game");
        Application.Quit();
    }


    private void OnEnable()
    {
        Time.timeScale = 0.2f;
        Time.fixedDeltaTime = 0.2f * 0.02f;

        CheckAndMarkCompletedLevel();

        EnableAndDisableLevelButtons();

        MarkSelectedLevel();
    }


    private void OnDisable()
    {
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02f;

        UnmarkSelectedLevel();
    }


    private void CheckAndMarkCompletedLevel()
    {
        for (int i = 0; i < levelButtons.Count; i++)
        {
            if (GameManager.Instance.IsLevelComplete(i+1))
            {
                levelButtons[i].GetComponent<Image>().color = completedColor;
            }
        }
    }


    private void EnableAndDisableLevelButtons()
    {
        for (int i = 0; i < levelButtons.Count; i++)
        {
            bool isLevelComplete = GameManager.Instance.IsLevelComplete(i+1);
            bool isNewestLevel = i+1 == GameManager.Instance.LatestCompletedLevel() + 1;

            Button button = levelButtons[i].GetComponentInChildren<Button>();

            if (!isLevelComplete && !isNewestLevel)
            {
                button.interactable = false;
            }
            else
            {
                button.interactable = true;
            }
        }
    }


    private void MarkSelectedLevel()
    {
        int currentSelectedLevel = GameManager.Instance.CurrentLevel - 1;

        ResizeButton(levelButtons[currentSelectedLevel], 140, 120);

        markedButton = currentSelectedLevel;
    }


    private void UnmarkSelectedLevel()
    {
        if (levelButtons.Count == 0) return;

        ResizeButton(levelButtons[markedButton], 130, 110);
    }

    
    private void ResizeButton(GameObject button, int width, int height)
    {
        RectTransform buttonRect = button.GetComponent<RectTransform>();
        buttonRect.sizeDelta = new Vector2(width, height);
    }
}
