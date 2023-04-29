using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CpuPlayer : MonoBehaviour
{
    [SerializeField]
    private Board _board;

    private BoardEvaluator _boardEvaluator;
    [SerializeField]
    private PlayerInfo _cpuPlayer;
    [SerializeField]
    private PlayerInfo _humanPlayer;

    [SerializeField]
    private int searchDepth;

    [SerializeField]
    private float _thinkTime;

    private void Awake()
    {
        _boardEvaluator = new BoardEvaluator();
    }

    private void OnEnable()
    {
        StartCoroutine(TakeTurn());
    }

    private void ExecuteMove()
    {
        var board = _board.State.ToArray();
        var moves = new List<CandidateMove>();
        for (int i = 0; i < board.Length; i++)
        {
            if (board[i] == 0)
            {
                board[i] = _cpuPlayer.Id;
                var score = MinMax(board, _cpuPlayer.Id, _humanPlayer.Id, searchDepth, false);
                var move = new CandidateMove { Index = i, Score = score };
                moves.Add(move);
                Debug.Log(move);
                board[i] = 0;
            }
        }
        var bestMoves = moves.GroupBy(m => m.Score).OrderByDescending(g => g.Key).FirstOrDefault()?.ToList();
        if (bestMoves == null || !bestMoves.Any())
        {
            _board.Play(0, _cpuPlayer);
            return;
        }
        var chosenmove = bestMoves[Random.Range(0, bestMoves.Count)];
        _board.Play(chosenmove.Index, _cpuPlayer);
    }

    private IEnumerator TakeTurn()
    {
        yield return new WaitForSeconds(_thinkTime);
        ExecuteMove();
    }
    private struct CandidateMove
    {
        public int Index;
        public int Score;
    }

    public int MinMax(int[] board, int playerId, int enemyId, int depth, bool playerTurn)
    {
        var eval = _boardEvaluator.Evaluate(board);
        if (eval.IsDraw) return 0;
        if (eval.WinnerId == playerId) return 10;
        if (eval.WinnerId != playerId && eval.GameFinished) return -10;
        if (depth == 0) return 0;
        var scores = new List<int>();
        
        for (int i = 0;i < board.Length; i++)
        {
            if (board[i] == 0) 
            {
                board[i] = playerTurn ? playerId : enemyId;
                var score = MinMax(board, playerId, enemyId, depth - 1, !playerTurn);
                scores.Add(score);
                board[i] = 0;
            }
        }
        if (playerTurn) return scores.Max();
        else return scores.Min();
    }
}
