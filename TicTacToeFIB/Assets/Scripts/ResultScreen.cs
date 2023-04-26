using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;

public class ResultScreen : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI _text;
    [SerializeField]
    CanvasGroup _canvas;
    public void Win(PlayerInfo winner)
    {
        _text.text = $"{winner.Name} Wins!";
        _canvas.interactable = true;
        _canvas.blocksRaycasts = true;
    }

    public void Draw()
    {
        _text.text = $"It's a Draw!";
        _canvas.interactable = true;
        _canvas.blocksRaycasts = true;
    }

    public void Continue()
    {
        _canvas.interactable = false;
        _canvas.blocksRaycasts = false;
    }
}
