using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private PlayerInfo _player1;
    [SerializeField]
    private PlayerInfo _player2;

    [SerializeField]
    private GameObject _activePlayerController;
    [SerializeField]
    private GameObject _inactivePlayerController;

    [SerializeField]
    private Board _board;

    [SerializeField]
    private float _secondsBetweenTurns;

    [SerializeField]
    private OnWin _onWin;
    [SerializeField]
    private UnityEvent _onDraw;
    [SerializeField]
    private ResultLine _resultLine;

    private BoardEvaluator _boardEvaluator;
    private void Awake()
    {
        _onWin ??= new OnWin();
        _onDraw ??= new UnityEvent();
    }
    void Start()
    {
        _boardEvaluator = new BoardEvaluator();
        _board.OnPlay.AddListener(OnPlay);
        _player1.ResetWins();
        _player2.ResetWins();
    }

    public void BeginGame()
    {
        _board.Clear();
        SwapPlayers();
        _resultLine.HideLine();
    }

    public void SwapPlayers()
    {
        var swap = _activePlayerController;
        _activePlayerController = _inactivePlayerController;
        _inactivePlayerController = swap;
        _inactivePlayerController.SetActive(false);
        StartCoroutine(SwapPlayersCoroutine());
    }
    public IEnumerator SwapPlayersCoroutine()
    {
        yield return new WaitForSeconds(_secondsBetweenTurns);
        _activePlayerController.SetActive(true);
    }


    private void OnPlay()
    {
        var result = _boardEvaluator.Evaluate(_board.State);
        _activePlayerController.SetActive(false);

        if (!result.GameFinished) Continue();
        else if (result.IsDraw) Draw();
        else Win(result);
    }

    private void Continue()
    {
        SwapPlayers();
        // trigger swap graphic
        Debug.Log($"swap players");
    }

    private void Win(EvaluationResult result)
    {
        var winner = GetPlayer(result.WinnerId);
        winner.AddWin();
        _resultLine.DrawLine(result.Line);
        _onWin.Invoke(winner);
    }

    private void Draw()
    {
        _onDraw.Invoke();
    }

    private PlayerInfo GetPlayer(int id)
    {
        if(id == _player1.Id) return _player1;
        if(id == _player2.Id) return _player2;
        throw new Exception($"player with id {id} does not exist - players:{ _player1.Id }, { _player2.Id }");
    }
}
[Serializable]
public class OnWin : UnityEvent<PlayerInfo> { }