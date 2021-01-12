﻿using System.Collections;
using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    private const float survivalBonusDelay = 3.0f;

    private int _score;
    private int _survivalBonus;
    private int _collisionBonus;

    private GameController _gameController;
    private TMP_Text _textScore;
    private TMP_Text _textSurvivalBonus;
    private TMP_Text _textCollisionBonus;

    private void Start()
    {
        _gameController = GameObject.FindGameObjectWithTag("GameController")
            .GetComponent<GameController>();

        _textScore = GameObject.FindGameObjectWithTag("TextScore")
            .GetComponent<TMP_Text>();

        _textSurvivalBonus = GameObject.FindGameObjectWithTag("TextSurvivalBonus")
            .GetComponent<TMP_Text>();

        _textCollisionBonus = GameObject.FindGameObjectWithTag("TextCollisionBonus")
            .GetComponent<TMP_Text>();

        _textSurvivalBonus.color = Color.red;
        _textCollisionBonus.color = Color.magenta;
    }

    public IEnumerator GiveSurvivalBonus()
    {
        while (_gameController.IsRunning)
        {
            yield return new WaitForSeconds(survivalBonusDelay);

            GiveSurvivalBonus(GameObject.FindGameObjectsWithTag("Cube").Length
                + GameObject.FindGameObjectsWithTag("Sphere").Length);
        }
    }

    public void GiveCollisionBonus()
    {
        if (_gameController.IsRunning)
        {
            AddBonusToScoreText(1);

            _collisionBonus++;
            _textCollisionBonus.text = "++" + _collisionBonus;
        }
    }

    private void GiveSurvivalBonus(int bonus)
    {
        if (_gameController.IsRunning)
        {
            AddBonusToScoreText(bonus);

            _survivalBonus += bonus;
            _textSurvivalBonus.text = "+" + _survivalBonus;
        }
    }

    private void AddBonusToScoreText(int bonus)
    {
        _score += bonus;
        _textScore.text = "Score: " + _score;   
    }
}
