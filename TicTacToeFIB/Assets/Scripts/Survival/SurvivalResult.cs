using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SurvivalResult : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreText;
    [SerializeField]
    private TextMeshProUGUI roundsText;

    public void Show(int score, int rounds)
    {
        this.scoreText.text = $"{score} wins";
        this.roundsText.text = $"{rounds} rounds";
        this.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        var scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
