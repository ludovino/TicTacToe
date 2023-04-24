using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private PlayerInfo _player1;
    [SerializeField]
    private PlayerInfo _player2;

    [SerializeField]
    private GameObject _activePlayer;
    [SerializeField]
    private GameObject _inactivePlayer;

    [SerializeField]
    private Board _board;

    [SerializeField]
    private float _secondsBetweenTurns;

    private BoardEvaluator _boardEvaluator;

    void Start()
    {
        _boardEvaluator = new BoardEvaluator();
        _board.OnPlay.AddListener(OnPlay);
    }

    public void BeginGame()
    {
        _board.Clear();
    }

    public void SwapPlayers()
    {
        var swap = _activePlayer;
        _activePlayer = _inactivePlayer;
        _inactivePlayer = swap;
        _inactivePlayer.SetActive(false);
        StartCoroutine(SwapPlayersCoroutine());
    }
    public IEnumerator SwapPlayersCoroutine()
    {
        yield return new WaitForSeconds(_secondsBetweenTurns);
        _activePlayer.SetActive(true);
    }
    private void OnPlay()
    {
        var result = _boardEvaluator.Evaluate(_board.State);
        
        if (!result.GameFinished) {
            SwapPlayers();
            // trigger swap graphic
            Debug.Log($"swap players");
            return; 
        }

        if (result.IsDraw)
        {
            // draw screen
            Debug.Log("Draw");
            return;
        }

        var winner = GetPlayer(result.WinnerId);
        // win effect

        Debug.Log("Win");
        winner.Win();
    }

    private PlayerInfo GetPlayer(int id)
    {
        if(id == _player1.Id) return _player1;
        if(id == _player2.Id) return _player2;
        throw new Exception($"player with id {id} does not exist - players:{ _player1.Id }, { _player2.Id }");
    }
}
