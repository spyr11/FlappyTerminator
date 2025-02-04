﻿using TMPro;
using UnityEngine;

public class ScoreView : MonoBehaviour
{
    [SerializeField] private ScoreCounter _scoreCounter;
    [SerializeField] private TextMeshProUGUI _score;

    private void OnEnable() => _scoreCounter.Changed += OnChanged;

    private void OnDisable() => _scoreCounter.Changed -= OnChanged;

    private void OnChanged(int score) => _score.text = score.ToString();
}