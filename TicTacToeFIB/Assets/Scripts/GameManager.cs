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

    private GameObject _startingPlayer;

    private GameObject _secondPlayer;

    [SerializeField]
    private Board _board;

    [SerializeField]
    private float _secondsBetweenTurns;
    [SerializeField]
    private float _secondsShowingLine;

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
        _startingPlayer = _activePlayerController;
        _secondPlayer = _inactivePlayerController;
        _player1.ResetWins();
        _player2.ResetWins();
    }

    public void BeginGame()
    {
        _board.Clear();
        _resultLine.HideLine();
        SwapStart();
        _activePlayerController = _startingPlayer;
        _inactivePlayerController = _secondPlayer;
        ActivatePlayer();
    }

    public void SwapPlayers()
    {
        var swap = _activePlayerController;
        _activePlayerController = _inactivePlayerController;
        _inactivePlayerController = swap;
        _inactivePlayerController.SetActive(false);
    }

    public void SwapStart()
    {
        var swap = _startingPlayer;
        _startingPlayer = _secondPlayer;
        _secondPlayer = swap;
    }

    public void ActivatePlayer()
    {
        _activePlayerController.SetActive(true);
    }

    public IEnumerator ActivatePlayerCR()
    {
        yield return new WaitForSeconds(_secondsBetweenTurns);
        _activePlayerController.SetActive(true);
    }


    private void OnPlay(PlayerInfo _)
    {
        var result = _boardEvaluator.Evaluate(_board.State);
        _activePlayerController.SetActive(false);

        if (!result.GameFinished) Continue();
        else if (result.IsDraw) StartCoroutine(Draw());
        else StartCoroutine(Win(result));
    }

    private void Continue()
    {
        SwapPlayers();
        StartCoroutine(ActivatePlayerCR());
    }

    private IEnumerator Win(EvaluationResult result)
    {
        var winner = GetPlayer(result.WinnerId);
        _resultLine.DrawLine(result.Line);
        yield return new WaitForSeconds(_secondsShowingLine);
        winner.AddWin();
        _onWin.Invoke(winner);
    }

    private IEnumerator Draw()
    {
        yield return new WaitForSeconds(_secondsShowingLine);
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