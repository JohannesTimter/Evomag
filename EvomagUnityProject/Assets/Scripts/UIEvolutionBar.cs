using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIEvolutionBar : MonoBehaviour
{
    public static UIEvolutionBar instance { get; private set; }

    public Image mask;
    public TextMeshProUGUI energybarText;
    float originalSize;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        originalSize = mask.rectTransform.rect.width;
    }

    public void SetValue(int currentEvopoints, int maxEvopoints)
    {
        float fraction = currentEvopoints / (float)maxEvopoints;
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * fraction);
        energybarText.text = currentEvopoints.ToString() + "/" + maxEvopoints.ToString();
    }
}
