using DG.Tweening;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int currentComboCount { get; set; }
    public int scoreCount { get; set; }

    public TextMeshProUGUI comboCounter;
    public TextMeshProUGUI scoreCounter;

    private void Start()
    {
        //init text 
        scoreCounter.text = scoreCount.ToString();
        comboCounter.text = ComboCountText(currentComboCount);
    }
    
    public void UpdateScore(HitAccuracy accuracy, int difficulty)
    {
        //Update combo count
        int comboCount = accuracy != HitAccuracy.Miss ? currentComboCount + 1 : currentComboCount = 0;
        SetComboCounter(comboCount);

        //Update score
        scoreCount = CalculateScore((int)accuracy,difficulty,1);

    }

    public int CalculateScore(int hitValue, int comboMultiplier, int difficultyMultiplier)
    {
        int Score = hitValue + (hitValue * ((comboMultiplier * difficultyMultiplier) / 25));
        return Score;
    }

    public void SetComboCounter(int count)
    {
        if (count > currentComboCount)
        {
            comboCounter.rectTransform.DOPunchScale(Vector3.one, 0.2f, 10, 1);
        }
        comboCounter.text = ComboCountText(count);
    }
    public string ComboCountText(int count)
    {
        string text = "x" + count;
        return text;
    }
}
