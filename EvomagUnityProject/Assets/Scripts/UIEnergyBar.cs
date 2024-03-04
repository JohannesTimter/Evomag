using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIEnergyBar : MonoBehaviour
{
    public static UIEnergyBar instance { get; private set; }

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

    public void SetValue(int currentEnergy, int maxEnergy)
    {
        float fraction = currentEnergy / (float)maxEnergy;
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * fraction);
        energybarText.text = currentEnergy.ToString() + "/" + maxEnergy.ToString();
    }
}
