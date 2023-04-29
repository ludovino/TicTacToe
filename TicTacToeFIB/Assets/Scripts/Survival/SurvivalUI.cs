using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SurvivalUI : MonoBehaviour
{
    [SerializeField]
    private PlayerInfo _playerInfo;

    [SerializeField]
    private GameObject _lives;
    [SerializeField]
    private Image _lifePrefab;
    [SerializeField]
    private TextMeshProUGUI _scoreText;

    [SerializeField]
    private RectTransform _cooldownBar;
    [SerializeField]
    private float maxHeight;
    private List<Image> _lifeIndicators;

    public void Start()
    {
        maxHeight = _cooldownBar.rect.height;
        if (_lives)
        {
            _lifeIndicators = _lives?.GetComponentsInChildren<Image>().ToList();
            foreach (var life in _lifeIndicators)
            {
                life.color = _playerInfo.Color;
            }
        }
        _cooldownBar.GetComponent<Image>().color = _playerInfo.Color;
    }


    public void UpdateCooldown(float proportion)
    {
        var prop = 1 - Mathf.Clamp(proportion, 0f, 1f);
        _cooldownBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, maxHeight * prop);
    }

    public void UpdateLives(int remaining)
    {
        if(remaining == _lifeIndicators.Count) { return; }
        if(remaining > _lifeIndicators.Count)
        {
            var life = Instantiate(_lifePrefab, _lives.transform);
            life.color = _playerInfo.Color;
            return;
        }
        if(remaining < _lifeIndicators.Count)
        {
            var life = _lifeIndicators.Last();
            _lifeIndicators.Remove(life);
            Destroy(life.gameObject);
        }
    }

    public void UpdateScore(int score)
    {
        _scoreText.text = $"Wins: {score}";
    }
}
