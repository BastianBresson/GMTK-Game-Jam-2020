﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasHandling : MonoBehaviour
{
    [SerializeField] GameObject UI = default;

    [SerializeField] List<GameObject> levelButtons = default;

    public Color completedColor = default;

    private int markedButton;

    private bool isGameJustStart = true;

    public void OnButtonPressed(int buttonNr)
    {
        Debug.Log($"Button {buttonNr} Presesd!");
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
        if (isGameJustStart || levelButtons.Count == 0) return;
        for (int i = 0; i < levelButtons.Count; i++)
        {
            if (GameManager.Instance.IsLevelComplete(i))
            {
                levelButtons[i].GetComponent<Image>().color = completedColor;
            }
        }
    }


    private void MarkSelectedLevel()
    {
        if (isGameJustStart || levelButtons.Count == 0) return;

        int currentSelectedLevel = GameManager.Instance.currentLevel;

        RectTransform button = levelButtons[currentSelectedLevel].GetComponent<RectTransform>();
        button.sizeDelta = new Vector2(140, 120);

        markedButton = currentSelectedLevel;
    }

    private void UnmarkSelectedLevel()
    {
        if (levelButtons.Count == 0) return;
        RectTransform button = levelButtons[markedButton].GetComponent<RectTransform>();
        button.sizeDelta = new Vector2(130, 110);

        if (isGameJustStart)
        {
            isGameJustStart = false;
        }
    }
}
