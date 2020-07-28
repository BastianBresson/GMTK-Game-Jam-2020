using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EmissiveButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] float fadeTime = default;

    #region Emission Colors
    [Header("State and Transition colors")]
    [ColorUsage(true, true)]
    [SerializeField] Color normalColor = default;

    [ColorUsage(true, true)]
    [SerializeField] Color normalHighlightColor = default;

    [ColorUsage(true, true)]
    [SerializeField] Color selectedColor = default;

    [ColorUsage(true, true)]
    [SerializeField] Color selectedHighlightColor = default;
    #endregion

    Button button;
    bool isSelected;


    private void Awake()
    {
        button = GetComponent<Button>();
        button.image.material = new Material(button.image.material);
        SetButtonColor(normalColor);
    }


    public void SetAsStartButton()
    {
        SetButtonColor(selectedColor);
        isSelected = true;
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        FadeFromIdleToHighlighted();
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        FadeFromHighlightedToIdle();
    }


    private void FadeFromIdleToHighlighted()
    {
        Color startColor = IdleColor();
        Color targetColor = HighligtedColor();

        FadeColor(startColor, targetColor);
    }


    private void FadeFromHighlightedToIdle()
    {
        Color startColor = HighligtedColor();
        Color targetColor = IdleColor();

        FadeColor(startColor, targetColor);
    }


    private Color HighligtedColor()
    {
        Color currentColor = isSelected ? selectedHighlightColor : normalHighlightColor;
        return currentColor;
    }


    private Color IdleColor()
    {
        Color targetColor = isSelected ? selectedColor : normalColor;
        return targetColor;
    }


    public void OnButtonPressed()
    {
        if (isSelected)
        {
            FadeColor(selectedColor, selectedHighlightColor);
            return;
        }

        isSelected = true;

        FadeColor(normalHighlightColor, selectedHighlightColor);
    }


    public void DeselectButton()
    {
        if (!isSelected) return;

        isSelected = false;

        FadeColor(selectedColor, normalColor);
    }


    private void FadeColor(Color fromColor, Color toColor)
    {
        StartCoroutine(FadeColorCoroutine(fromColor, toColor));
    }


    private IEnumerator FadeColorCoroutine(Color currentColor, Color targetColor)
    {
        float elapsedTime = 0;

        while (elapsedTime < fadeTime)
        {
            yield return null;

            elapsedTime += Time.deltaTime;

            Color lerpedColor = Color.Lerp(currentColor, targetColor, elapsedTime / fadeTime);

            SetButtonColor(lerpedColor);
        }
    }


    private void SetButtonColor(Color color)
    {
        button.image.material.SetColor("_EmissionColor", color);
    }
}
