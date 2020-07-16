using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] float fadeTime = default;

    [ColorUsage(true, true)]
    [SerializeField] Color normalColor = default;

    [ColorUsage(true, true)]
    [SerializeField] Color normalHighlightColor = default;

    [ColorUsage(true, true)]
    [SerializeField] Color selectedColor = default;

    [ColorUsage(true, true)]
    [SerializeField] Color selectedHighlightColor = default;

    Button button;
    bool isSelected;

    private void Start()
    {
        button = GetComponent<Button>();
        button.image.material = new Material(button.image.material);
        SetButtonColor(normalColor);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Color startColor = isSelected ? selectedColor : normalColor;
        Color targetColor = isSelected ? selectedHighlightColor : normalHighlightColor;

        StartCoroutine(FadeColorCoroutine(startColor, targetColor));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Color startColor = isSelected ? selectedHighlightColor : normalHighlightColor; 
        Color targetColor = isSelected ? selectedColor : normalColor;

        StartCoroutine(FadeColorCoroutine(startColor, targetColor));
    }

    public void OnButtonPressed()
    {
        if (isSelected)
        {
            StartCoroutine(FadeColorCoroutine(selectedColor, selectedHighlightColor));
            return;
        }

        isSelected = true;

        StartCoroutine(FadeColorCoroutine(normalHighlightColor, selectedHighlightColor));
    }

    public void DeselectButton()
    {
        isSelected = true;

        StartCoroutine(FadeColorCoroutine(selectedColor, normalColor));
    }

    IEnumerator FadeColorCoroutine(Color currentColor, Color targetColor)
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
