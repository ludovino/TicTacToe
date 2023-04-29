using Assets.Scripts;
using DG.Tweening;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SurvivalGameManager : MonoBehaviour
{
    [SerializeField]
    private Board _board;

    private BoardEvaluator _boardEvaluator;
    [SerializeField]
    private float cooldown;
    [SerializeField]
    private float minCooldown;
    [SerializeField]
    private float speedIncreasePercent;
    
    [SerializeField]
    [Range(0f, 1f)]
    private float playerAdvantage;

    [SerializeField]
    private float transitionTime;
    [SerializeField]
    private ResultLine _resultLine;

    [SerializeField]
    private SurvivalResult _survivalResult;

    [Serializable]
    public class Player
    {
        public PlayerInfo Info;
        public GameObject playerController;
        public SurvivalUI ui;
        public float cooldown;
        public void UpdateCooldown()
        {
            if (cooldown > 0)
            {
                if (playerController.activeInHierarchy) playerController.SetActive(false);
                cooldown -= Time.deltaTime;
            }
            else if (!playerController.activeInHierarchy) playerController.SetActive(true);
        }
    }

    [SerializeField]
    private int _startingLives;
    private int _lives;
    public int Lives => _lives;

    private int _score;
    private int _rounds;
    public int score => _score;

    [SerializeField]
    private Player _humanPlayer;
    [SerializeField]
    private Player _cpuPlayer;

    private Dictionary<int, Player> _players;
    private bool timeOn;

    private void Awake()
    {
        _boardEvaluator = new BoardEvaluator();
        timeOn = true;
        _lives = _startingLives;
        _players = new Dictionary<int, Player> {
            { _humanPlayer.Info.Id, _humanPlayer },
            { _cpuPlayer.Info.Id, _cpuPlayer }
        };
    }

    private void Start()
    {
        _board.OnPlay.AddListener(OnPlay);
        _board.OnPlayFail.AddListener(OnFail);
    }

    private void Update()
    {
        if (!timeOn) return;
        _humanPlayer.UpdateCooldown();
        _humanPlayer.ui.UpdateCooldown(Mathf.Clamp(_humanPlayer.cooldown / cooldown, 0f, 1f));
        _cpuPlayer.UpdateCooldown();
        _cpuPlayer.ui.UpdateCooldown(Mathf.Clamp(_cpuPlayer.cooldown / (cooldown * (1f + playerAdvantage)), 0f, 1f));
    }

    private void OnPlay(PlayerInfo playerInfo)
    {
        var player = _players[playerInfo.Id];
        var result = _boardEvaluator.Evaluate(_board.State);
        Cooldown(player);
        if (!result.GameFinished) return;
        else if (result.IsDraw)
        {
            Draw();
            return;
        }
        else if (result.WinnerId == _cpuPlayer.Info.Id) Lose();
        else Win(result);
        _resultLine.DrawLine(result.Line);
    }

    private void OnFail(PlayerInfo playerInfo)
    {
        var player = _players[playerInfo.Id];
        Cooldown(player);
    }

    public void Cooldown(Player player)
    {
        var time = cooldown;
        if(player == _cpuPlayer) time = cooldown * (1f + playerAdvantage);
        player.cooldown = time;
        player.UpdateCooldown();
    }

    private void Win(EvaluationResult result)
    {
        _rounds++;
        _score++;
        _humanPlayer.ui.UpdateScore(_score);
        SpeedUp();
        ResetBoard();
    }

    private void SpeedUp()
    {
        var speedFactor = speedIncreasePercent / 100f;
        cooldown = cooldown * (1 - speedFactor);
        cooldown = Mathf.Max(cooldown, minCooldown);
    }

    private void Lose()
    {
        _rounds++;
        _lives--;
        if (_lives == 0)
        {
            timeOn = false;
            _survivalResult.Show(_score, _rounds);
            return;
        }
        _humanPlayer.ui.UpdateLives(_lives);
        ResetBoard();
    }
    private void ResetBoard()
    {
        if (_humanPlayer.cooldown == 0) _humanPlayer.cooldown = 0.1f;
        if (_cpuPlayer.cooldown == 0) _cpuPlayer.cooldown = 0.1f;
        StartCoroutine(ResetBoardCR());
    }
    private IEnumerator ResetBoardCR()
    {
        timeOn = false;
        _board.enabled = false;
        var pos = _board.gameObject.transform.position; 
        yield return new WaitForSeconds(transitionTime);
        yield return _board.gameObject.transform.DOMove(pos + new Vector3(0, 10f, 0), transitionTime).WaitForCompletion();
        _board.Clear();
        _resultLine.HideLine();
        _board.gameObject.transform.position = pos - new Vector3(0, 10f, 0);
        yield return _board.gameObject.transform.DOMove(pos, transitionTime).WaitForCompletion();
        _board.enabled = true;
        timeOn = true;
    }

    private void Draw()
    {
        _rounds++;
        SpeedUp();
        ResetBoard();
    }
}
