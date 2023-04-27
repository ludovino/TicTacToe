using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SurvivalUI : MonoBehaviour
{
    [SerializeField]
    private GameObject Lives;

    [SerializeField]
    private TextMeshProUGUI scoreText;

    [SerializeField]
    private RectTransform playerCooldown;
    private float playerCooldownMaxHeight;
    [SerializeField]
    private RectTransform cpuCooldown;
    private float cpuCooldownMaxHeight;

    public void Start()
    {
        
    }

    public void UpdateCooldown(float proportion)
    {

    }
}
