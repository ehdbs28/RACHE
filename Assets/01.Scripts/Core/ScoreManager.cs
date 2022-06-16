using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    private TextMeshProUGUI _currentScoreTxt;
    private int _currentScore = 0;
    public int CurrentScore
    {
        set => _currentScore = value;
        get => _currentScore;
    }

    private void Start()
    {
        _currentScoreTxt = GameObject.Find("Canvas/Score").GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        _currentScoreTxt.text = $"{_currentScore.ToString("D5")}";
    }
}
