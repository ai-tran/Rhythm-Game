using DG.Tweening;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int comboCount { get; set; }
    public int scoreCount { get; set; }

    public TextMeshProUGUI comboCounter;
    public TextMeshProUGUI scoreCounter;

    private void Start()
    {
        //init text 
        scoreCounter.text = scoreCount.ToString();
        comboCounter.text = ComboCountText(comboCount);
    }
    
    public void UpdateScore(HitAccuracy accuracy, int difficulty)
    {
        comboCount = accuracy == HitAccuracy.Perfect ? comboCount++ : comboCount = 0;

        if (comboCounter != null)
        {
            comboCounter.text = comboCount.ToString();
        }
        else
        {
            Debug.LogWarning("Missing Combo Counter UI");
        }
    }

    public int CalculateScore(int hitValue, int comboMultiplier, int difficultyMultiplier)
    {
        int Score = hitValue + (hitValue * ((comboMultiplier * difficultyMultiplier) / 25));
        return Score;
    }

    public void SetComboCounter(int count)
    {
        if (count > comboCount)
        {
            comboCounter.rectTransform.DOPunchScale(Vector3.one, 0.2f, 10, 1);
        }
        comboCounter.text = ComboCountText(count);
        comboCount = count;
    }
    public string ComboCountText(int count)
    {
        string text = "x" + count;
        return text;
    }
}
