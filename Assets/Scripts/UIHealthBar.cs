using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    public static UIHealthBar instance { get; private set; }

    public Image mask;
    public TextMeshProUGUI healthbarText;
    float originalSize;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        originalSize = mask.rectTransform.rect.width;
    }

    public void SetValue(int currentHealth, int maxHealth)
    {
        float fraction = currentHealth / (float)maxHealth;
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * fraction);
        healthbarText.text = currentHealth.ToString() + "/" + maxHealth.ToString();
    }

}
