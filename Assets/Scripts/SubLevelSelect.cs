using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubLevelSelect : MonoBehaviour
{
    [SerializeField] private Button[] buttons = default;

    [SerializeField] private GameObject[] subLevels = default;

    private int currentSubLevel = 0;


    private void Start()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            // using i directly made all buttons call SwitchSubLevel(i) with i = length. Clousure problem.
            int copy = i; 

            buttons[i].onClick.AddListener(() => SwitchSubLevel(copy));
        }
    }


    private void SwitchSubLevel(int subLevel)
    {
        if (subLevel == currentSubLevel) return;

        subLevels[currentSubLevel].SetActive(false);
        subLevels[subLevel].SetActive(true);


        buttons[currentSubLevel].GetComponent<ButtonHover>().DeselectButton();

        currentSubLevel = subLevel;
    }
}
