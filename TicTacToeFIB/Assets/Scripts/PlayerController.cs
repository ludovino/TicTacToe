using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private GameObject _grid;
    [SerializeField]
    private PlayerInfo _playerInfo;

    private Button[] _clickableSquares;

    [SerializeField]
    private Board _board;

    private void Awake()
    {
        _clickableSquares = _grid.GetComponentsInChildren<Button>(true);
    }
    public void OnEnable()
    {
        for (int i = 0; i < _clickableSquares.Length; i++)
        {
            Button button = _clickableSquares[i];
            button.onClick.AddListener(() => Play(Array.IndexOf(_clickableSquares, button)));
            button.enabled = _board.State[i] == 0;
        }
    }

    public void OnDisable()
    {
        foreach (var button in _clickableSquares)
        {
            button.enabled = false;
            button?.onClick.RemoveAllListeners();
        }
    }

    private void Play(int index)
    {
        if (!enabled) return;
        var played = _board.Play(index, _playerInfo);
    }

    private void OnBoardChange()
    {
        for (int i = 0; i < _clickableSquares.Length; i++)
        {
            Button button = _clickableSquares[i];

            button.enabled = _board.State[i] == 0;
        }
    }
}
