using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Board : MonoBehaviour
{
    [SerializeField]
    private int[] _board = new int[9];
    private int _currentTurn;

    [SerializeField]
    private Image[] _markImages = new Image[9];

    public IReadOnlyList<int> State => Array.AsReadOnly(_board);

    [SerializeField]
    private OnPlay _onPlay;
    public OnPlay OnPlay => _onPlay;
    [SerializeField]
    private OnPlay _onPlayFail;
    public OnPlay OnPlayFail => _onPlayFail;

    private void Awake()
    {
        _onPlay ??= new OnPlay();
        _onPlayFail ??= new OnPlay();
    }

    [Button("Clear")]
    public void Clear()
    {
        for(int i = 0; i < _board.Length; i++) 
        {
            _board[i] = 0;
            _markImages[i].enabled = false;
        }
    }

    public bool Playable(int index)
    {
        return this.enabled && _board[index] == 0;
    }

    public bool Play(int index, PlayerInfo player)
    {
        if (!Playable(index))
        {
            _onPlayFail.Invoke(player);
            return false;
        }
        _board[index] = player.Id;

        var toMark = _markImages[index];
        
        toMark.enabled = true;
        toMark.sprite = player.Mark;
        toMark.color = player.Color;

        _onPlay.Invoke(player);

        return true;
    }
}
[Serializable]
public class OnPlay : UnityEvent<PlayerInfo> { }
