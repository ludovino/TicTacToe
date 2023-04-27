using DG.Tweening;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultLine : MonoBehaviour
{
    [SerializeField]
    private Transform[] squares = new Transform[9];
    [SerializeField]
    private LineRenderer line;
    [SerializeField]
    private Vector2Int _startEnd;
    [SerializeField]
    private float _drawTime;
    private Tween tween;
    private Vector3 _startPos;
    private Vector3 _currentPos;
    private Vector3 _endPos;
    public void DrawLine(Vector2Int startEnd)
    {
        if (tween != null) tween.Kill();
        _startPos = this.transform.InverseTransformPoint(squares[startEnd.x].position);
        _endPos = this.transform.InverseTransformPoint(squares[startEnd.y].position);
        _currentPos = _startPos;
        DOTween.To(() => _currentPos, v => _currentPos = v, _endPos, _drawTime).SetEase(Ease.InCubic).OnUpdate(() =>
        {
            UpdateLine();
        });
        line.enabled = true;
    }

    private void UpdateLine()
    {
        var positions = new Vector3[] { _startPos, _currentPos };
        line.SetPositions(positions);
    }

    [Button("Draw Line")]
    public void DrawLine()
    {
        DrawLine(_startEnd);
    }


    public void HideLine()
    {
        line.enabled = false;
    }

    public void OnDestroy()
    {
        if (tween != null) tween.Kill();
    }
}
