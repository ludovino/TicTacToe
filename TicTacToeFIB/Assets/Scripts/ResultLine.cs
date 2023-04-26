using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultLine : MonoBehaviour
{
    [SerializeField]
    private Transform[] squares = new Transform[9];
    [SerializeField]
    private LineRenderer line;

    public void DrawLine(Vector2Int startEnd)
    {
        var positions = new Vector3[] { squares[startEnd.x].position, squares[startEnd.y].position };
        line.SetPositions(positions);
        line.enabled = true;
    }
}
