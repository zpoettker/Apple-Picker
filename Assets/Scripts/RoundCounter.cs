using UnityEngine;
using TMPro;

public class RoundCounter : MonoBehaviour
{
    [Header("Inscribed")]
    public int pointsPerRound = 1000;
    public int maxRound = 4;

    [Header("Dynamic")]
    public int round = 1;

    private TextMeshProUGUI uiText;
    private ScoreCounter scoreCounter;

    void Start()
    {
        uiText = GetComponent<TextMeshProUGUI>();
        scoreCounter = FindFirstObjectByType<ScoreCounter>();
    }

    void Update()
    {
        // 0–999 = Round 1, 1000–1999 = Round 2, 2000–2999 = Round 3, 3000+ = Round 4
        round = Mathf.Min(scoreCounter.score / pointsPerRound + 1, maxRound);
        uiText.text = "Round " + round;
    }
}