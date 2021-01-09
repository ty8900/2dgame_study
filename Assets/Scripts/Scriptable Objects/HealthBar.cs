using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public HitPoints hitPoints;

    [HideInInspector]
    public Player character;

    public Image meterImage;
    public Text hpText;

    float maxHitpoints;

    void Start()
    {
        if (character != null)
        maxHitpoints = character.maxHitPoints;
    }

    void Update()
    {
        if (character != null)
        {

            meterImage.fillAmount = hitPoints.value / maxHitpoints;

            hpText.text = "HP: " + (meterImage.fillAmount * 100);
        }
    }
}
