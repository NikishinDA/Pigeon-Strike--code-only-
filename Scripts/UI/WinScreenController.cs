using System;
using System.Collections;
using System.Collections.Generic;
using BG_UI_Toolkit.LeaderBord;
using UnityEngine;

public class WinScreenController : MonoBehaviour
{
    [SerializeField] private LeaderBoard leaderBoard;
    [SerializeField] private PlayerSizeManager sizeManager;
    private int _playerPlace;
    private int _playerScore;
    private int _playerBestScore;
    private int _playerNewIndex;
    [SerializeField] private int basicScore = 50;
    private bool _playerAdvance;

    private void Awake()
    {
        _playerPlace = PlayerPrefsStrings.GetIntValue(PlayerPrefsStrings.PlayerPlacement);
        _playerBestScore = PlayerPrefsStrings.GetIntValue(PlayerPrefsStrings.PlayerBestScore);
    }

    private void Start()
    {
        _playerScore = (int) (basicScore * sizeManager.GetRelativeScale(3));

        if (_playerPlace > leaderBoard.SlotsNumber)
        {
            leaderBoard.SetRanksFromBottom(_playerPlace);
            int[] scores = new int[leaderBoard.SlotsNumber];
            if (_playerScore > _playerBestScore)
            {
                for (int i = 0; i < scores.Length - 1; i++)
                {
                    scores[i] = _playerScore - (i + 1);
                }

                scores[scores.Length - 1] = _playerScore;
                _playerAdvance = true;
                PlayerPrefs.SetInt(PlayerPrefsStrings.PlayerBestScore.Name, _playerScore);
                PlayerPrefs.SetInt(PlayerPrefsStrings.PlayerPlacement.Name, _playerPlace - leaderBoard.SlotsNumber - 1);
                PlayerPrefs.SetInt(PlayerPrefsStrings.NameRotation.Name,
                    PlayerPrefsStrings.GetIntValue(PlayerPrefsStrings.NameRotation) + 1);
                PlayerPrefs.Save();
            }
            else
            {
                for (int i = 0; i < scores.Length - 1; i++)
                {
                    scores[i] = _playerBestScore + (scores.Length - 1 - i);
                }

                scores[scores.Length - 1] = _playerBestScore;
            }

            leaderBoard.SetScores(scores);
        }
        else
        {
            leaderBoard.SetRanksFromBottom(_playerPlace);
            int[] scores = new int[leaderBoard.SlotsNumber];
            for (int i = 0; i < scores.Length - 1; i++)
            {
                scores[i] = (scores.Length - i) * (_playerScore + (scores.Length - i));
            }

            scores[scores.Length - 1] = _playerScore;
            leaderBoard.SetScores(scores);
        }
    }

    private void OnEnable()
    {
        StartCoroutine(Wait(0.5f));
    }

    private IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
        if (_playerAdvance)
            leaderBoard.SetPlayerPosition(0);
    }
}