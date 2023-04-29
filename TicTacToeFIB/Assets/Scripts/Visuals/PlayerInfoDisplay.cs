using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class PlayerInfoDisplay : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _name;
    [SerializeField]
    private TextMeshProUGUI _wins;
    [SerializeField]
    private Image _mark; 
    [SerializeField]
    private PlayerInfo _playerInfo;

    public void Start()
    {
        _name.text = _playerInfo.Name;
        _mark.sprite = _playerInfo.Mark;
        _mark.color = _playerInfo.Color;
        UpdateScore();
    }

    public void UpdateScore()
    {
        _wins.text = $"Wins: {_playerInfo.Wins}";
    }
}
