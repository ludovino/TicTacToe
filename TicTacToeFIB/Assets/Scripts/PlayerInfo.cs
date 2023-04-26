using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerInfo")]
public class PlayerInfo : ScriptableObject
{
    [SerializeField]
    private string _name;
    [SerializeField]
    private int _wins;
    [SerializeField]
    private Sprite _mark;
    [SerializeField]
    private Color _color;
    [SerializeField]
    [Range(1,2)]
    private byte _id;

    public string Name { get => _name; set => _name = value; }
    public int Wins { get => _wins; }
    public void AddWin() => _wins++;
    public Sprite Mark { get => _mark; }
    public int Id { get => 1 << _id; } // allow bitwise eval
    public Color Color { get => _color;  }

    public void ResetWins()
    {
        _wins= 0;
    }
}

