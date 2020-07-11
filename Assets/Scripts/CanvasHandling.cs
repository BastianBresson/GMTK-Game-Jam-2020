using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasHandling : MonoBehaviour
{
    [SerializeField] GameObject UI = default;

    [SerializeField] List<GameObject> levelButtons = default;

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

        MarkSelectedLevel();
    }


    private void OnDisable()
    {
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02f;

        UnmarkSelectedLevel();
    }


    private void MarkSelectedLevel()
    {
        if (isGameJustStart) return;

        int currentSelectedLevel = GameManager.Instance.currentLevel;

        RectTransform button = levelButtons[currentSelectedLevel].GetComponent<RectTransform>();
        button.sizeDelta = new Vector2(140, 120);

        markedButton = currentSelectedLevel;
    }

    private void UnmarkSelectedLevel()
    {
        RectTransform button = levelButtons[markedButton].GetComponent<RectTransform>();
        button.sizeDelta = new Vector2(130, 110);

        if (isGameJustStart)
        {
            isGameJustStart = false;
        }
    }

}
