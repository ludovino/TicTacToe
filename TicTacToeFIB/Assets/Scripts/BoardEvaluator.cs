using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class BoardEvaluator
    {
        public EvaluationResult Evaluate(IReadOnlyList<int> b)
        {
            if (b.Any(i => i % 2 != 0)) throw new Exception("Player ids should be powers of two");
            // rows
            if((b[0] & b[1] & b[2]) > 0) return new EvaluationResult { GameFinished = true, WinnerId = b[0], Line = new Vector2Int(0, 2) };
            if((b[3] & b[4] & b[5]) > 0) return new EvaluationResult { GameFinished = true, WinnerId = b[3], Line = new Vector2Int(3, 5) };
            if((b[6] & b[7] & b[8]) > 0) return new EvaluationResult { GameFinished = true, WinnerId = b[6], Line = new Vector2Int(6, 8) };
            // columns
            if((b[0] & b[3] & b[6]) > 0) return new EvaluationResult { GameFinished = true, WinnerId = b[0], Line = new Vector2Int(0, 6) };
            if((b[1] & b[4] & b[7]) > 0) return new EvaluationResult { GameFinished = true, WinnerId = b[1], Line = new Vector2Int(1, 7) };
            if((b[2] & b[5] & b[8]) > 0) return new EvaluationResult { GameFinished = true, WinnerId = b[2], Line = new Vector2Int(2, 8) };
            // diagonals
            if((b[0] & b[4] & b[8]) > 0) return new EvaluationResult { GameFinished = true, WinnerId = b[0], Line = new Vector2Int(0, 8) };
            if((b[2] & b[4] & b[6]) > 0) return new EvaluationResult { GameFinished = true, WinnerId = b[2], Line = new Vector2Int(2, 6) };

            if (b.All(i => i > 0)) return new EvaluationResult { GameFinished = true, WinnerId = 0 };

            return new EvaluationResult { GameFinished = false };
        }
    }

    public struct EvaluationResult
    {
        public bool GameFinished { get; set; }
        public int WinnerId { get; set; }
        public bool IsDraw => GameFinished && WinnerId == 0;
        public Vector2Int Line { get; set; }
    }
}
