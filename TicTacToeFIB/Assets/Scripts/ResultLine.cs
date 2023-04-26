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
    public void DrawLine(Vector2Int startEnd)
    {
        var positions = new Vector3[] { 
            this.transform.InverseTransformPoint(squares[startEnd.x].position),
            this.transform.InverseTransformPoint(squares[startEnd.y].position) };
        
        line.SetPositions(positions);
        line.enabled = true;
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
}
