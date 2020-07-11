using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasHandling : MonoBehaviour
{
    [SerializeField] GameObject UI = default;

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
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02f;
    }
}
