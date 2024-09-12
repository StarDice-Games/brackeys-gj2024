using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Text scoreText;
    public Text timerText; 

    private int score;
    private float timer;

    void Start()
    {
        score = 0;
        timer = 0f;
        UpdateStats();
    }

    public void UpdateScore(int amount)
    {
        score += amount;
        UpdateStats();
    }

    void Update()
    {
        timer += Time.deltaTime;
        UpdateStats();
    }

    private void UpdateStats()
    {
        scoreText.text = "Punteggio: " + score;
        timerText.text = "Tempo: " + Mathf.FloorToInt(timer).ToString();
    }
}
