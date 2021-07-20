using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoreBoard : MonoBehaviour
{
    private static ScoreBoard _instance;
    public static ScoreBoard Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("ScoreBoard Failed to load");
            }
            return _instance;
        }
    }

    public List<int> _scores = new List<int>();
    public List<string> _scoreNames = new List<string>();

    public List<TMP_Text> _mainMenuScores, _mainMenuNames, _gameScores, _gameNames;

    private int _highscoreToSet, _tempScore;
    private string _tempName;
    private string _scoreList, _scoreNameList;
    public int checkScore(int score)
    {
        
        LoadScores();
        _highscoreToSet = 99;
        
        for (int l = 0; l < _scores.Count; l++)
        {
            if (score > _scores[l])
            {
                _highscoreToSet = l;
                l = 10;
            }
        }
        return _highscoreToSet;
    }

    public void SetHighScore(int score, string name)
    {
        for (int l = 0; l < _scores.Count; l++)
        {
            
            if (_highscoreToSet <= l)
            {
                
                _tempName = _scoreNames[l];
                _scoreNames[l] = name;
                name = _tempName;

                _tempScore = _scores[l];
                _scores[l] = score;
                score = _tempScore;
                
                _scoreList = "Score" + l;
                _scoreNameList = "ScoreName" + l;
                PlayerPrefs.SetInt(_scoreList, _scores[l]);
                PlayerPrefs.SetString(_scoreNameList, _scoreNames[l].ToUpper());
            }
        }
    }

    public void SetMainMenuScoreBoard()
    {
        LoadScores();
        int loop = 0;
        foreach (TMP_Text score in _mainMenuScores)
        {
            score.text = _scores[loop].ToString();
            loop++;
        }
        loop = 0;
        foreach (TMP_Text name in _mainMenuNames)
        {
            name.text = _scoreNames[loop].ToString();
            loop ++;
        }
        loop = 0;
    }
    public void SetGameScoreBoard()
    {
        LoadScores();
        int loop = 0;
        foreach (TMP_Text score in _gameScores)
        {
            score.text = _scores[loop].ToString();
            loop++;
        }
        loop = 0;
        foreach (TMP_Text name in _gameNames)
        {
            name.text = _scoreNames[loop].ToString();
            loop++;
        }
        loop = 0;
    }
    private void Awake()
    {
        _instance = this;
        LoadScores();
        LoadCheckIfNoScores();
    }

    private string _defaultName = "BIGGYSCORES";
    private int _defaultScores = 11000; 
    private void LoadCheckIfNoScores()
    {
        if (_scores.Count == 0)
        {
            for (int l = 0; l < 10; l++)
            {
                _defaultScores -= (l * 1000);
                _scoreList = "Score" + l;
                _scoreNameList = "ScoreName" + l;
                PlayerPrefs.SetInt(_scoreList, _defaultScores);
                PlayerPrefs.SetString(_scoreNameList, _defaultName.ToUpper());
                
            }
        }
    }
    private void LoadScores()
    {
        _scores.Clear();
        _scoreNames.Clear();
        for (int l = 0; l <10 ; l++)
        {
            _scoreList = "Score" + l;
            _scoreNameList = "ScoreName" + l;
            _scores.Add(PlayerPrefs.GetInt(_scoreList));
            _scoreNames.Add(PlayerPrefs.GetString(_scoreNameList));
        }
    }
}
